using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectReLink.Core.Battle
{
    /// <summary>
    /// 전투 이벤트를 중계하는 경량 이벤트 버스입니다.
    /// </summary>
    public static class BattleEventBus
    {
        private static readonly Dictionary<TriggerPoint, Action<BattleEventContext>> _subscribers = new();

        public static void Subscribe(TriggerPoint point, Action<BattleEventContext> action)
        {
            if (!_subscribers.ContainsKey(point))
                _subscribers[point] = null;
            _subscribers[point] += action;
        }

        public static void Unsubscribe(TriggerPoint point, Action<BattleEventContext> action)
        {
            if (_subscribers.ContainsKey(point))
                _subscribers[point] -= action;
        }

        public static void Publish(TriggerPoint point, BattleEventContext context)
        {
            if (_subscribers.TryGetValue(point, out var action))
            {
                action?.Invoke(context);
            }
        }
    }

    /// <summary>
    /// 이벤트 발생 시 전달되는 정보 묶음입니다.
    /// </summary>
    public struct BattleEventContext
    {
        public GameObject Actor;    // 행위자
        public GameObject Target;   // 대상
        public float Value;         // 대미지 혹은 수치
        public string SourceID;     // 원인이 된 스킬/효과 ID
    }

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

    public struct DamageInput
    {
        public float AttackerATK;
        public float SkillRatio;
        public float AffinityMultiplier;
        public float CritMultiplier;
        public float DefenderDEF;
        public float PenetrationMultiplier;
    }
}