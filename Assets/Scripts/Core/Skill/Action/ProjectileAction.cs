using System;
using UnityEngine;

namespace ProjectReLink.Core.Skill
{
    [Serializable]
    public class ProjectileAction : SkillAction
    {
        public string PrefabID;
        public float Speed;

        public override void Play(SkillContext context, Action onComplete)
        {
            Debug.Log($"Firing projectile {PrefabID} from {context.Caster.name} to {context.Target.name}");
            // 실제 로직: 투사체 생성 -> 이동 완료 시 onComplete 호출
            onComplete?.Invoke();
        }
    }
}