using LostArkLogger.Event;
using LostArkLogger.Event.Events;
using LostArkLogger.State.Socket;

namespace LostArkLogger.State;

public class StateManager
{
    private static readonly Dictionary<string, int> healingSkills = new Dictionary<string, int>()
    {
        {"Serenade of Salvation", 3},
        {"Holy Aura", 16 * 1000},
        {"Holy Protection", 7 * 1000},
        {"Demonize", 1500}
    };

    private List<StateSocketHandler> _stateSocketHandlers = new List<StateSocketHandler>();

    private bool PhaseTransitionResetRequest;
    private long PhaseTransitionResetTime;
    private Game? _Game;
    private List<HealSource> _HealSources;

    private List<Game> _Encounters;

    public StateManager()
    {
        this._Encounters = new List<Game>();

        this.ResetState();

        this.RegisterListeners();
    }

    private void RegisterListeners()
    {
        EventManager manager = LostArkLogger.Instance.EventManager;

        manager.Subscribe<EnterNewZoneEvent>(_ => { SoftReset(); });
        manager.Subscribe<NewEntityEvent>(e =>
        {
            // Console.WriteLine($"New entity: {e.Entity.Name}, Class: {e.Entity.Class}, ID: {e.Entity.Id}");
            UpdateEntity(e.Entity.Id, entity =>
            {
                entity.Id = e.Entity.Id;
                entity.Name = e.Entity.Name;
                entity.IsPlayer = e.Entity.IsPlayer;
                // We don't want to overwrite the entity's metadata with the default values
                if (!e.IsUnknown || entity.GearScore == 0)
                {
                    entity.Class = e.Entity.Class;
                    entity.ClassId = e.Entity.ClassId;
                    entity.GearScore = e.Entity.GearScore;
                    entity.MaxHp = e.Entity.MaxHp;
                    entity.NpcId = e.Entity.NpcId;
                }

                return entity;
            });
        });
        manager.Subscribe<EntityDeathEvent>(e =>
        {
            this._Game.Entities.TryGetValue(e.Id, out Entity? entity);

            var deaths = 0;
            if (entity == null) deaths = 1;
            else if (entity.IsDead) deaths = entity.Deaths;
            else deaths = entity.Deaths + 1;

            this.UpdateEntity(e.Id, newEntity =>
            {
                newEntity.Name = e.Name;
                newEntity.IsDead = true;
                newEntity.DeathTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                newEntity.Deaths = deaths;
                return newEntity;
            });
        });
        manager.Subscribe<SkillStartEvent>(e =>
        {
            if (healingSkills.ContainsKey(e.SkillName))
            {
                _HealSources.Add(new HealSource(
                    e.Name,
                    DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() + healingSkills[e.SkillName]
                ));
            }

            if (e.Name == null) return;

            UpdateEntity(e.Id, entity =>
            {
                entity.Name = e.Name;
                entity.Id = e.Id;
                entity.IsDead = false;
                return entity;
            });

            _Game.Entities.TryGetValue(e.Name, out Entity? entity);

            if (entity == null) return;

            entity.Hits.Casts += 1;

            if (!entity.Skills.ContainsKey(e.SkillName))
            {
                entity.Skills[e.SkillName] = EntitySkills.Create().Modify(skills =>
                {
                    skills.Id = e.SkillId;
                    skills.Name = e.SkillName;
                    return skills;
                });

                entity.Skills[e.SkillName].Hits.Casts += 1;
            }
        });

        manager.Subscribe<EntityDamageEvent>(e =>
        {
            if (PhaseTransitionResetRequest && PhaseTransitionResetTime > 0 &&
                PhaseTransitionResetTime < DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - 1500)
            {
                SoftReset();
            }

            UpdateEntity(e.Id, entity =>
            {
                entity.Id = e.Id;
                entity.Name = e.Name;
                return entity;
            });

            UpdateEntity(e.TargetId, entity =>
            {
                entity.Id = e.TargetId;
                entity.Name = e.TargetName;
                entity.CurrentHp = e.CurrentHp;
                entity.MaxHp = e.MaxHp;
                return entity;
            });

            var damageOwner = _Game.Entities[e.Id];
            _Game.Entities.TryGetValue(e?.TargetName ?? "", out Entity? damageTarget);

            if (
                damageTarget is {IsPlayer: false} &&
                e.CurrentHp < 0
            )
            {
                e.Damage = e.Damage + e.CurrentHp;
            }

            if (e.SkillId == 0 && e.SkillEffectId != 0)
            {
                e.SkillId = e.SkillEffectId;
                e.SkillName = e.SkillEffect;
            }

            if (!damageOwner.Skills.ContainsKey(e.SkillName))
            {
                damageOwner.Skills[e.SkillName] = EntitySkills.Create().Modify(skills =>
                {
                    skills.Id = e.SkillId;
                    skills.Name = e.SkillName;
                    return skills;
                });
            }

            var hitFlag = (HitFlag) (e.DamageModifier & 0xf);
            var hitOption = (HitOption) (((e.DamageModifier >> 4) & 0x7) - 1);

            // TODO: Keeping for now; Not sure if referring to damage share on Valtan G1 or something else
            // TODO: Not sure if this is fixed in the logger
            if (e.SkillName == "Bleed" && e.Damage > 10000000) return;

            // Remove 'sync' bleeds on G1 Valtan
            if (e.SkillName == "Bleed" && hitFlag == HitFlag.HIT_FLAG_DAMAGE_SHARE)
            {
                return;
            }

            var isCrit = hitFlag == HitFlag.HIT_FLAG_CRITICAL || hitFlag == HitFlag.HIT_FLAG_DOT_CRITICAL;
            var isBackAttack = hitOption == HitOption.HIT_OPTION_BACK_ATTACK;
            var isFrontAttack = hitOption == HitOption.HIT_OPTION_FRONTAL_ATTACK;
            var critCount = isCrit ? 1 : 0;
            var backAttackCount = isBackAttack ? 1 : 0;
            var frontAttackCount = isFrontAttack ? 1 : 0;

            damageOwner.Skills[e.SkillName].TotalDamage += e.Damage;
            if (e.Damage > damageOwner.Skills[e.SkillName].MaxDamage)
                damageOwner.Skills[e.SkillName].MaxDamage = e.Damage;

            damageOwner.DamageDealt += e.Damage;
            if (damageTarget != null)
                damageTarget.DamageTaken += e.Damage;

            if (e.SkillName != "Bleed")
            {
                damageOwner.Hits.Total += 1;
                damageOwner.Hits.Crit += critCount;
                damageOwner.Hits.BackAttack += backAttackCount;
                damageOwner.Hits.FrontAttack += frontAttackCount;

                damageOwner.Skills[e.SkillName].Hits.Total += 1;
                damageOwner.Skills[e.SkillName].Hits.Crit += critCount;
                damageOwner.Skills[e.SkillName].Hits.BackAttack += backAttackCount;
                damageOwner.Skills[e.SkillName].Hits.FrontAttack += frontAttackCount;
            }

            if (damageOwner.IsPlayer)
            {
                this._Game.DamageStatistics.TotalDamageDealt += e.Damage;
                this._Game.DamageStatistics.TopDamageDealt = Math.Max(
                    this._Game.DamageStatistics.TopDamageDealt, damageOwner.DamageDealt);

                var breakdown = new Breakdown(
                    DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
                    e.Damage,
                    damageTarget?.Id ?? "",
                    isCrit,
                    isBackAttack,
                    isFrontAttack
                );
                damageOwner.Skills[e.SkillName].Breakdown.Add(breakdown);
            }

            if (damageTarget is {IsPlayer: true})
            {
                this._Game.DamageStatistics.TotalDamageTaken += e.Damage;
                this._Game.DamageStatistics.TopDamageTaken = Math.Max(
                    this._Game.DamageStatistics.TopDamageTaken, damageTarget.DamageTaken);
            }

            if (this._Game.FightStartedOn == 0)
            {
                this._Game.FightStartedOn = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            }

            this._Game.LastCombatPacket = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        });
        manager.Subscribe<EntityHealEvent>(e =>
        {
            var sourceName = null as string;
            for (var i = 0; i < _HealSources.Count; i++)
            {
                HealSource source = _HealSources[i];
                if (source.Expires >= DateTimeOffset.UtcNow.ToUnixTimeMilliseconds())
                {
                    sourceName = source.Source;
                }
            }

            if (sourceName == null) return;

            foreach (var (key, value) in _Game.Entities)
            {
                if (value.Name == sourceName)
                {
                    value.HealingDone += e.HealAmount;

                    if (value.IsPlayer)
                    {
                        this._Game.DamageStatistics.TotalHealingDone += e.HealAmount;
                        this._Game.DamageStatistics.TopHealingDone = Math.Max(
                            this._Game.DamageStatistics.TopHealingDone, value.HealingDone);
                    }

                    break;
                }
            }
        });
        manager.Subscribe<EntityBuffEvent>(e =>
        {
            if (e.IsNew)
            {
                UpdateEntity(e.Id, entity =>
                {
                    entity.Name = e.Name;
                    entity.Id = e.Id;
                    return entity;
                });

                var gameEntity = this._Game.Entities[e.Id];
                gameEntity.ShieldDone += e.ShieldAmount;

                if (gameEntity.IsPlayer)
                {
                    this._Game.DamageStatistics.TotalShieldDone += e.ShieldAmount;
                    this._Game.DamageStatistics.TopShieldDone = Math.Max(
                        this._Game.DamageStatistics.TopShieldDone, gameEntity.ShieldDone);
                }
            }
        });
        manager.Subscribe<EntityCounterAttackEvent>(e =>
        {
            UpdateEntity(e.Id, entity =>
            {
                entity.Name = e.Name;
                entity.Id = e.Id;
                return entity;
            });

            this._Game.Entities[e.Id].Hits.Counter += 1;
            if (this._Game.Entities[e.Id].Skills.ContainsKey(e.SkillName))
            {
                this._Game.Entities[e.Id].Skills[e.SkillName].Hits.Counter += 1;
            }
        });
        manager.Subscribe<PhaseTransitionEvent>(e =>
        {
            PhaseTransitionResetRequest = true;
            PhaseTransitionResetTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

            RemoveDeadEntities();
        });
    }

    private void UpdateEntity(string? id, Func<Entity, Entity> modify)
    {
        lock (_Game.Entities)
        {
            if (id == null) id = "";
            this._Game.Entities.TryGetValue(id, out Entity? entity);

            if (entity == null)
            {
                entity = Entity.CreateEntity();
            }

            this._Game.Entities[id] = entity.Modify(modify).Update();
        }
    }
    
    private void SoftReset()
    {
        lock (_Game.Entities)
        {
            var gameEntities = _Game.Entities;
            ResetState();

            foreach (var entity in gameEntities)
            {
                if (DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - entity.Value.LastUpdate > 10 * 60 * 1000)
                {
                    continue;
                }

                UpdateEntity(entity.Key, newEntity =>
                {
                    newEntity.Name = entity.Value.Name;
                    newEntity.NpcId = entity.Value.NpcId;
                    newEntity.Class = entity.Value.Class;
                    newEntity.ClassId = entity.Value.ClassId;
                    newEntity.IsPlayer = entity.Value.IsPlayer;
                    newEntity.GearScore = entity.Value.GearScore;
                    newEntity.MaxHp = entity.Value.MaxHp;
                    newEntity.CurrentHp = entity.Value.CurrentHp;

                    return newEntity;
                });
            }

            RemoveDeadEntities();
        }
    }

    // Remove entities that are dead and not players
    private void RemoveDeadEntities()
    {
        var gameEntities = _Game.Entities;
        foreach (var entity in gameEntities)
        {
            if (entity.Value.IsPlayer) continue;
            if (entity.Value.CurrentHp > 0) continue;
            if (!entity.Value.IsDead) continue;

            gameEntities.Remove(entity.Key);
        }
    }

    public void AddHandler(StateSocketHandler handler)
    {
        this._stateSocketHandlers.Add(handler);
    }

    public void RemoveHandler(StateSocketHandler stateSocketHandler)
    {
        this._stateSocketHandlers.Remove(stateSocketHandler);
    }

    public void ResetState()
    {
        long startAt = DateTime.Now.Ticks;
        _Game = new Game(startAt,
            startAt,
            0,
            new Dictionary<string, Entity>(),
            new DamageStatistics(0,
                0,
                0,
                0,
                0,
                0,
                0,
                0)
        );
        PhaseTransitionResetTime = 0;
        PhaseTransitionResetRequest = false;
        _HealSources = new List<HealSource>();

        foreach (var handler in _stateSocketHandlers)
        {
            try
            {
                handler.Context.WebSocket.Send("stateReseted");
            }
            catch (Exception ignored)
            {
            }
        }
    }

    public Game GetState()
    {
        return _Game;
    }
}