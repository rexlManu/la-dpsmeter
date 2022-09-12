namespace LostArkLogger.Event.Events;

public class SkillStartEvent:Event
{
    public string? Id, Name, SkillName;
    public uint SkillId;

    public SkillStartEvent(string id, string name, string skillName, uint skillId)
    {
        Id = id;
        Name = name;
        SkillName = skillName;
        SkillId = skillId;
    }
}