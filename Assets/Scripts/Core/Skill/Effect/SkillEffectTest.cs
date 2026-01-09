using UnityEngine;

namespace ProjectReLink.Core.Skill
{
    public class SkillEffectTest : SkillEffect
    {
        public override void Execute(SkillContext context)
        {
            Debug.Log("SkillEffectTest executed.");
        }
    }
}