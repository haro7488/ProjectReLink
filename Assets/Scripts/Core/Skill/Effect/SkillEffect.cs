using System;
using ProjectReLink.Core.Battle;

namespace ProjectReLink.Core.Skill
{
    /// <summary>
    /// Effect: 실제 로직(대미지, 상태이상 등)을 담당합니다.
    /// </summary>
    [Serializable]
    public abstract class SkillEffect : ISkillComponent
    {
        // Phase 2 확장: 효과가 발동될 트리거 시점
        public TriggerPoint Trigger = TriggerPoint.None;

        public abstract void Execute(SkillContext context);
    }
}