namespace ProjectReLink.Core.Battle
{
    /// <summary>
    /// 전투 중 발생하는 모든 주요 시점을 정의합니다.
    /// </summary>
    public enum TriggerPoint
    {
        None,
        OnBattleStart,      // 전투 진입 시
        OnTurnStart,        // 턴 시작 시
        OnTurnEnd,          // 턴 종료 시
        OnAttack,           // 공격 성공 시
        OnDamaged,          // 피격 시
        OnKill,             // 적 처치 시
        OnDeath             // 사망 시
    }
}