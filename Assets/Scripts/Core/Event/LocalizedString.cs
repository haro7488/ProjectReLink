using System;

namespace ProjectReLink.Core.Data
{
    [Serializable]
    public class LocalizedString
    {
        public string Key; // LID_NAME_LUCY
        
        // 실제 게임에서는 LocalizationManager.Get(Key)를 호출
        public string GetValue() => $"Translated_{Key}"; 
    }
}