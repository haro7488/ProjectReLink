using System;
using ProjectReLink.Core.Battle;
using UnityEngine;

namespace ProjectReLink.Core.Skill
{
    [Serializable]
    public class DamageEffect : SkillEffect
    {
        public float Ratio = 1.0f;
        public string DamageType = "Normal";

        public override void Execute(SkillContext context)
        {
            // 1. 입력값 취합 (실제로는 캐릭터 스탯 컴포넌트에서 가져옴)
            var input = new DamageInput
            {
                AttackerATK = 100f, // 예시 수치
                SkillRatio = this.Ratio,
                AffinityMultiplier = 1.0f,
                CritMultiplier = 1.0f,
                DefenderDEF = 50f,
                PenetrationMultiplier = 1.0f
            };

            // 2. 엔진을 통한 연산
            float finalDamage = BattleProcessor.CalculateDamage(input);

            // 3. 이벤트 발행 (OnDamaged 트리거 등 전파)
            BattleEventBus.Publish(TriggerPoint.OnDamaged, new BattleEventContext
            {
                Actor = context.Caster,
                Target = context.Target,
                Value = finalDamage
            });

            Debug.Log($"[Battle] {context.Caster.name} dealt {finalDamage} damage to {context.Target.name}");
        }
    }
}