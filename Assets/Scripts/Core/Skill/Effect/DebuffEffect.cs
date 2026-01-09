using System;
using UnityEngine;

namespace ProjectReLink.Core.Skill
{
    [Serializable]
    public class DebuffEffect : SkillEffect
    {
        public string StatusEffectId;
        public int Duration;

        public override void Execute(SkillContext context)
        {
            Debug.Log($"{context.Target.name} affected by {StatusEffectId} for {Duration} turns");
        }
    }
}