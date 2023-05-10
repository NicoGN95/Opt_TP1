using System.Collections.Generic;
using UnityEngine;

namespace _Main.Scripts
{
    public class PoolGeneric<T> where T : Object
    {
        private readonly T prefab;
        private readonly Queue<T> availables = new();
        private readonly List<T> m_inUse = new();

        public PoolGeneric(T p_prefab)
        {
            prefab = p_prefab;
        }

        public T GetorCreate()
        {
            if (availables.Count > 0)
            {
                var l_obj = availables.Dequeue();
                m_inUse.Add(l_obj);
                return l_obj;
            }

            var l_newObj = Object.Instantiate(prefab);
            m_inUse.Add(l_newObj);
            return l_newObj;
        }

        public void AddPool(T p_poolEntry)
        {
            if (!m_inUse.Contains(p_poolEntry)) 
                return;
            
            m_inUse.Remove(p_poolEntry);
            availables.Enqueue(p_poolEntry);
        }

        public void ClearData()
        {
            m_inUse.Clear();
            availables.Clear();
        }
    }
}
