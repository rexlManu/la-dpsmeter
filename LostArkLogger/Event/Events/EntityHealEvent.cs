namespace LostArkLogger.Event.Events;

public class EntityHealEvent:Event
{
    public string Id, Name;
    public long HealAmount;

    public EntityHealEvent(string id, string name, long healAmount)
    {
        Id = id;
        Name = name;
        HealAmount = healAmount;
    }
}