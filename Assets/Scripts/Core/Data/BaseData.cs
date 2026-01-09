using System;
using UnityEngine;

namespace ProjectReLink.Core.Data
{
    /// <summary>
    /// 모든 데이터 테이블의 기본 단위입니다.
    /// </summary>
    [Serializable]
    public abstract class BaseData
    {
        [SerializeField] protected string id;
        public string ID => id;
    }
}