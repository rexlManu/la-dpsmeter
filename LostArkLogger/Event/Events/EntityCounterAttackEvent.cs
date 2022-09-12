namespace LostArkLogger.Event.Events;

public class EntityCounterAttackEvent : Event
{
    public string Id, Name, TargetId, TargetName, SkillId, SkillName;

    public EntityCounterAttackEvent(string id, string name, string targetId, string targetName, string skillId, string skillName)
    {
        Id = id;
        Name = name;
        TargetId = targetId;
        TargetName = targetName;
        SkillId = skillId;
        SkillName = skillName;
    }
}