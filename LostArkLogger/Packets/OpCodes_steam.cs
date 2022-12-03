using System;
namespace LostArkLogger
{
    public enum OpCodes_Steam : UInt16
    {
        PKTAuthTokenResult = 31261,
        PKTCounterAttackNotify = 9781,
        PKTDeathNotify = 31947,
        PKTInitEnv = 9838,
        PKTInitPC = 14691,
        PKTNewNpc = 12653,
        PKTNewNpcSummon = 438,
        PKTNewPC = 3004,
        PKTNewProjectile = 46254,
        PKTPartyStatusEffectAddNotify = 24622,
        PKTPartyStatusEffectRemoveNotify = 33744,
        PKTRaidBossKillNotify = 41595,
        PKTRaidResult = 38299,
        PKTRemoveObject = 41408,
        PKTSkillDamageAbnormalMoveNotify = 46255,
        PKTSkillDamageNotify = 43263,
        PKTSkillStageNotify = 15303,
        PKTSkillStartNotify = 31471,
        PKTStatChangeOriginNotify = 4335,
        PKTStatusEffectAddNotify = 28922,
        PKTStatusEffectRemoveNotify = 49696,
        PKTTriggerBossBattleStatus = 8986,
        PKTTriggerStartNotify = 14699,
    }
}
