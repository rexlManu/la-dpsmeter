namespace LostArkLogger.Event.Events;

public class SkillStageEvent:Event
{
    public string Id, Name, SkillName;
    public uint SkillId, SkillStage;

    public SkillStageEvent(string id, string name, string skillName, uint skillId, uint skillStage)
    {
        Id = id;
        Name = name;
        SkillName = skillName;
        SkillId = skillId;
        SkillStage = skillStage;
    }
}