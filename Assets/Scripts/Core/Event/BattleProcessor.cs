using System;
using UnityEngine;

namespace ProjectReLink.Core.Battle
{
    /// <summary>
    /// 모든 수식에 유연하게 대처할 수 있는 전투 연산 엔진입니다.
    /// </summary>
    public static class BattleProcessor
    {
        // 대미지 공식을 델리게이트로 분리하여 기획자 요구에 따라 런타임 교체 가능
        public static Func<DamageInput, float> DamageFormula = DefaultDamageFormula;

        private static float DefaultDamageFormula(DamageInput input)
        {
            // 최종 대미지 = (공격력 * 스킬배율 * 상성계수 * 크리티컬보정) - (방어력 * 관통보정)
            var attackPart = input.AttackerATK * input.SkillRatio * input.AffinityMultiplier * input.CritMultiplier;
            var defensePart = input.DefenderDEF * input.PenetrationMultiplier;
            
            var finalDamage = Mathf.Max(1, attackPart - defensePart); // 최소 대미지 1 보정
            return finalDamage;
        }

        public static float CalculateDamage(DamageInput input) => DamageFormula(input);
    }
}