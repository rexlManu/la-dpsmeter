namespace LostArkLogger.Event.Events;

public class NewEntityEvent : Event
{
    public State.Entity Entity;

    public NewEntityEvent(State.Entity entity)
    {
        Entity = entity;
    }
}