namespace LostArkLogger.State;

public class DamageStatistics
{
    public long TotalDamageDealt { get; set; }
    public long TopDamageDealt { get; set; }
    public long TotalDamageTaken { get; set; }
    public long TopDamageTaken { get; set; }
    public long TotalHealingDone { get; set; }
    public long TopHealingDone { get; set; }
    public long TotalShieldDone { get; set; }
    public long TopShieldDone { get; set; }

    public DamageStatistics(long totalDamageDealt, long topDamageDealt, long totalDamageTaken, long topDamageTaken,
        long totalHealingDone, long topHealingDone, long totalShieldDone, long topShieldDone)
    {
        TotalDamageDealt = totalDamageDealt;
        TopDamageDealt = topDamageDealt;
        TotalDamageTaken = totalDamageTaken;
        TopDamageTaken = topDamageTaken;
        TotalHealingDone = totalHealingDone;
        TopHealingDone = topHealingDone;
        TotalShieldDone = totalShieldDone;
        TopShieldDone = topShieldDone;
    }
}

public class Breakdown
{
    public long Timestamp { get; set; }
    public long Damage { get; set; }
    public string TargetEntity { get; set; }
    public bool IsCrit { get; set; }
    public bool IsBackAttack { get; set; }
    public bool IsFrontAttack { get; set; }

    public Breakdown(long timestamp, long damage, string targetEntity, bool isCrit, bool isBackAttack,
        bool isFrontAttack)
    {
        Timestamp = timestamp;
        Damage = damage;
        TargetEntity = targetEntity;
        IsCrit = isCrit;
        IsBackAttack = isBackAttack;
        IsFrontAttack = isFrontAttack;
    }
}

public class Hits
{
    public int Casts { get; set; }
    public int Total { get; set; }
    public int Crit { get; set; }
    public int BackAttack { get; set; }
    public int FrontAttack { get; set; }
    public int Counter { get; set; }

    public Hits(int casts, int total, int crit, int backAttack, int frontAttack, int counter)
    {
        Casts = casts;
        Total = total;
        Crit = crit;
        BackAttack = backAttack;
        FrontAttack = frontAttack;
        Counter = counter;
    }
}

public class EntitySkills
{
    public static EntitySkills Create()
    {
        return new EntitySkills(
            0,
            "",
            0,
            0,
            new Hits(
                0,
                0,
                0,
                0,
                0,
                0),
            new List<Breakdown>());
    }

    public uint Id { get; set; }
    public string Name { get; set; }
    public long TotalDamage { get; set; }
    public long MaxDamage { get; set; }
    public Hits Hits { get; set; }
    public List<Breakdown> Breakdown { get; set; }

    public EntitySkills(uint id, string name, long totalDamage, long maxDamage, Hits hits, List<Breakdown> breakdown)
    {
        Id = id;
        Name = name;
        TotalDamage = totalDamage;
        MaxDamage = maxDamage;
        Hits = hits;
        Breakdown = breakdown;
    }

    public EntitySkills Modify(Func<EntitySkills, EntitySkills> action)
    {
        return action.Invoke(this);
    }
}

public class Entity
{
    public static Entity CreateEntity()
    {
        return new Entity(
            0,
            "",
            0,
            "",
            "",
            0,
            false,
            false,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            new Dictionary<string, EntitySkills>(),
            new Hits(
                0,
                0,
                0,
                0,
                0,
                0
            )
        );
    }

    public long LastUpdate { get; set; }
    public string Id { get; set; }
    public uint NpcId { get; set; }
    public string Name { get; set; }
    public string Class { get; set; }
    public int ClassId { get; set; }
    public bool IsPlayer { get; set; }
    public bool IsDead { get; set; }
    public int Deaths { get; set; }
    public long DeathTime { get; set; }
    public float GearScore { get; set; }
    public long CurrentHp { get; set; }
    public long MaxHp { get; set; }
    public long DamageDealt { get; set; }
    public long HealingDone { get; set; }
    public long ShieldDone { get; set; }
    public long DamageTaken { get; set; }
    public Dictionary<string, EntitySkills> Skills { get; set; }
    public Hits Hits { get; set; }

    public int Level { get; set; } = 0;

    public Entity(long lastUpdate, string id, uint npcId, string name, string @class, int classId, bool isPlayer,
        bool isDead, int deaths, long deathTime, float gearScore, long currentHp, long maxHp, long damageDealt,
        long healingDone, long shieldDone, long damageTaken, Dictionary<string, EntitySkills> skills, Hits hits)
    {
        LastUpdate = lastUpdate;
        Id = id;
        NpcId = npcId;
        Name = name;
        Class = @class;
        ClassId = classId;
        IsPlayer = isPlayer;
        IsDead = isDead;
        Deaths = deaths;
        DeathTime = deathTime;
        GearScore = gearScore;
        CurrentHp = currentHp;
        MaxHp = maxHp;
        DamageDealt = damageDealt;
        HealingDone = healingDone;
        ShieldDone = shieldDone;
        DamageTaken = damageTaken;
        Skills = skills;
        Hits = hits;
    }

    public Entity Modify(Func<Entity, Entity> action)
    {
        return action.Invoke(this);
    }

    public Entity Update()
    {
        LastUpdate = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        return this;
    }
}

public class HealSource
{
    public string Source { get; set; }
    public long Expires { get; set; }

    public HealSource(string source, long expires)
    {
        Source = source;
        Expires = expires;
    }
}

public class Game
{
    public long StartedOn { get; set; }
    public long LastCombatPacket { get; set; }
    public long FightStartedOn { get; set; }
    public Dictionary<string, Entity> Entities { get; set; }
    public DamageStatistics DamageStatistics { get; set; }

    public Game(long startedOn, long lastCombatPacket, long fightStartedOn, Dictionary<string, Entity> entities,
        DamageStatistics damageStatistics)
    {
        StartedOn = startedOn;
        LastCombatPacket = lastCombatPacket;
        FightStartedOn = fightStartedOn;
        Entities = entities;
        DamageStatistics = damageStatistics;
    }
}

public class HealingSkillDetails
{
    public int Duration { get; set; }

    public HealingSkillDetails(int duration)
    {
        Duration = duration;
    }
}

public class HealingSkills
{
    public Dictionary<string, HealingSkillDetails> HealingSkillsMap { get; set; }
}