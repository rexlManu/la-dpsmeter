namespace LostArkLogger.Event.Events;

public class NewEntityEvent : Event
{
    public State.Entity Entity;
    public bool IsUnknown;

    public NewEntityEvent(State.Entity entity)
    {
        Entity = entity;
        IsUnknown = false;
    }

    public NewEntityEvent(State.Entity entity, bool isUnknown)
    {
        Entity = entity;
        IsUnknown = isUnknown;
    }
}