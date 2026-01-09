using System;
using UnityEngine;

namespace ProjectReLink.Core.Skill
{
    /// <summary>
    /// 트리거 기반 패시브/버프 효과 예시
    /// </summary>
    [Serializable]
    public class TriggeredBuffEffect : SkillEffect
    {
        string BuffId;
        public float Chance = 1.0f;

        public override void Execute(SkillContext context)
        {
            // 특정 트리거 시점에 실행되도록 이벤트 버스에 예약하거나 즉시 실행
            if (UnityEngine.Random.value <= Chance)
            {
                Debug.Log($"[Trigger] {BuffId} activated on {Trigger}");
            }
        }
    }
}