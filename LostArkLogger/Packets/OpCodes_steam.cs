using System;
namespace LostArkLogger
{
    public enum OpCodes_Steam : UInt16
    {
		PKTAuthTokenResult = 16412,
		PKTCounterAttackNotify = 50809,
		PKTDeathNotify = 38154,
		PKTInitEnv = 59989,
		PKTInitPC = 36666,
		PKTNewNpc = 13966,
		PKTNewNpcSummon = 988,
		PKTNewPC = 33113,
		PKTNewProjectile = 8723,
		PKTRaidBossKillNotify = 29235,
		PKTRaidResult = 936,
		PKTRemoveObject = 5965,
		PKTSkillDamageAbnormalMoveNotify = 21397,
		PKTSkillDamageNotify = 41338,
		PKTSkillStageNotify = 53133,
		PKTSkillStartNotify = 50035,
		PKTStatChangeOriginNotify = 13018,
		PKTStatusEffectAddNotify = 17061,
		PKTTriggerBossBattleStatus = 30432
	}
}
