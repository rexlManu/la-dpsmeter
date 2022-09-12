namespace LostArkLogger.Event.Events;

public class EnterNewZoneEvent : Event
{
    public String PlayerId;

    public EnterNewZoneEvent(string playerId)
    {
        PlayerId = playerId;
    }
}