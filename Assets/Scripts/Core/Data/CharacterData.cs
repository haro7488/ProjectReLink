using System.Collections.Generic;
using UnityEngine;

namespace ProjectReLink.Core.Data
{
    [CreateAssetMenu(fileName = "CharData", menuName = "ProjectReLink/Data/Character")]
    public class CharacterData : ScriptableObject
    {
        public string Id;
        public LocalizedString Name;
        public int BaseHp;
        public int BaseAtk;
        public List<string> SkillIds = new List<string>();

        // 유효성 검사 로직 예시
        public bool Validate()
        {
            if (BaseHp <= 0)
            {
                Debug.LogError($"{Id}: HP must be positive.");
                return false;
            }

            return true;
        }
    }
}