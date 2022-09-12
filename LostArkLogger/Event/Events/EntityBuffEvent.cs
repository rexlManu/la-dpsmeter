namespace LostArkLogger.Event.Events;

public class EntityBuffEvent:Event
{
    public string Id, Name, BuffId, BuffName, SourceId, SourceName;
    public long ShieldAmount;
    public bool IsNew;

    public EntityBuffEvent(string id, string name, string buffId, string buffName, string sourceId, string sourceName, long shieldAmount)
    {
        Id = id;
        Name = name;
        BuffId = buffId;
        BuffName = buffName;
        SourceId = sourceId;
        SourceName = sourceName;
        ShieldAmount = shieldAmount;
        IsNew = SourceId == "1";
    }
}