using UnityEngine;

namespace ProjectReLink.Core.Skill
{
    /// <summary>
    /// 스킬 실행 시 필요한 컨텍스트 데이터 전달용 구조체입니다.
    /// </summary>
    public struct SkillContext
    {
        public GameObject Caster;
        public GameObject Target;
        public Vector3 TargetPosition;

        public SkillData Data;
        // 전투 연산 엔진을 위한 추가 스탯 정보 전달 가능
    }
}