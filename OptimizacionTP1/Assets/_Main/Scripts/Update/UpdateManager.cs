using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Main.Scripts.Update
{
    public class UpdateManager : MonoBehaviour
    {
        public static UpdateManager Instance;

        private List<IUpdateObject> m_updateObjects;

        private void Awake()
        {
            if (Instance != default)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            if (m_updateObjects == default)
                return;
            
            for (var i = 0; i < m_updateObjects.Count; i++)
            {
                m_updateObjects[i].MyUpdate();
            }
        }

        public void AddListener(IUpdateObject p_updateObject)
        {
            if (m_updateObjects == default)
                m_updateObjects = new List<IUpdateObject>();
            
            if (!m_updateObjects.Contains(p_updateObject))
                m_updateObjects.Add(p_updateObject);
        }

        public void RemoveListener(IUpdateObject p_updateObject)
        {
            if (m_updateObjects.Contains(p_updateObject))
                m_updateObjects.Remove(p_updateObject);

            if (m_updateObjects.Count <= 0)
                m_updateObjects = default;
        }
    }
}