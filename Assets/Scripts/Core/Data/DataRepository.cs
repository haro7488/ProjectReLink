using System.Collections.Generic;
using UnityEngine;

namespace ProjectReLink.Core.Data
{
    /// <summary>
    /// 기획 데이터(SO)를 런타임에서 ID 기반으로 조회하기 위한 저장소입니다.
    /// </summary>
    public class DataRepository<T> where T : BaseData
    {
        private readonly Dictionary<string, T> _dataMap = new Dictionary<string, T>();

        public void Initialize(IEnumerable<T> dataList)
        {
            _dataMap.Clear();
            foreach (var data in dataList)
            {
                if (!_dataMap.TryAdd(data.ID, data))
                {
                    Debug.LogWarning($"Duplicate ID detected in {typeof(T).Name}: {data.ID}");
                }
            }
        }

        public T Get(string id)
        {
            _dataMap.TryGetValue(id, out var data);
            return data;
        }
    }
}