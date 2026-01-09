using System;

namespace ProjectReLink.Core.Skill
{
    /// <summary>
    /// Action: 스킬 실행 방식(투사체, 즉발, 범위 등)을 담당합니다.
    /// </summary>
    [Serializable]
    public abstract class SkillAction : ISkillComponent
    {
        public abstract void Play(SkillContext context, Action onComplete);
    }
}