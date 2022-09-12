namespace LostArkLogger.Event.Events;

public class EntityDeathEvent : Event
{
    public string Id, Name, KillerId, KillerName;

    public EntityDeathEvent(string id, string name, string killerId, string killerName)
    {
        Id = id;
        Name = name;
        KillerId = killerId;
        KillerName = killerName;
    }
}