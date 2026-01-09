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