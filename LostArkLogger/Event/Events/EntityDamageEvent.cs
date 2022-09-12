namespace LostArkLogger.Event.Events;

public class EntityDamageEvent: Event
{
    public string Id, Name, SkillName, SkillEffect, TargetId, TargetName;
    public uint SkillId, SkillEffectId;
    public long Damage, DamageModifier, CurrentHp, MaxHp;

    public EntityDamageEvent(string id, string name, string skillName, string skillEffect, string targetId, string targetName, uint skillId, uint skillEffectId, long damage, long damageModifier, long currentHp, long maxHp)
    {
        Id = id;
        Name = name;
        SkillName = skillName;
        SkillEffect = skillEffect;
        TargetId = targetId;
        TargetName = targetName;
        SkillId = skillId;
        SkillEffectId = skillEffectId;
        Damage = damage;
        DamageModifier = damageModifier;
        CurrentHp = currentHp;
        MaxHp = maxHp;
    }
}