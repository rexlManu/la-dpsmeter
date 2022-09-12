namespace LostArkLogger.Event.Events;

public class PhaseTransitionEvent : Event
{
    public static PhaseCode GetPhaseCode(string phaseCode)
    {
        switch (phaseCode)
        {
            case "0":
                return PhaseCode.RaidResult;
            case "1":
                return PhaseCode.RaidBossKillNotify;
            case "2":
                return PhaseCode.TriggerBossBattleStatus;
            default:
                return PhaseCode.RaidResult;
        }
    }
    
    public PhaseCode PhaseCode;

    public PhaseTransitionEvent(PhaseCode phaseCode)
    {
        PhaseCode = phaseCode;
    }
}

public enum PhaseCode
{
    RaidResult,
    RaidBossKillNotify,
    TriggerBossBattleStatus
}