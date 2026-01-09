using System.Collections.Generic;
using ProjectReLink.Utils;
using UnityEngine;

namespace ProjectReLink.Core.Skill
{
    /// <summary>
    /// 기획자가 조합하는 최종 스킬 데이터 구조입니다.
    /// </summary>
    [CreateAssetMenu(fileName = "NewSkill", menuName = "ProjectReLink/Data/Skill")]
    public class SkillData : ScriptableObject
    {
        public string SkillID;
        public string DisplayName;

        [SerializeReference, SubclassSelector] public SkillAction Action;

        [SerializeReference, SubclassSelector] public List<SkillEffect> Effects = new List<SkillEffect>();

        /// <summary>
        /// 스킬 실행 프로세스
        /// </summary>
        public void Cast(SkillContext context)
        {
            Debug.Log($"Casting Skill: {DisplayName}");

            // 1. Action 실행 (연출 및 타이밍 제어)
            Action?.Play(context, () =>
            {
                // 2. Action 완료 시 모든 Effect 적용 (Trigger 시스템 확장이 용이함)
                foreach (var effect in Effects)
                {
                    effect.Execute(context);
                }
            });
        }
    }
}