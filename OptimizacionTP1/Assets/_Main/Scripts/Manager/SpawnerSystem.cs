using System;
using _Main.Scripts.Enemies;
using _Main.Scripts.Update;
using UnityEngine;
using Grid = _Main.Scripts.Enemies.Grid;

namespace _Main.Scripts.Manager
{
    public class SpawnerSystem : MonoBehaviour, IUpdateObject
    {
        [SerializeField] private float delayToSpawn;
        [SerializeField] private int maxEnemyOnGrid;
        [SerializeField] private Grid grid;

        private int m_currEnemyOnGrid;
        private float m_timer;

        private void Start()
        {
            m_timer = Time.time + delayToSpawn;
            
            SubscribeUpdateManager();
        }

        private void OnDisable()
        {
            UnSubscribeUpdateManager();
        }

        public void MyUpdate()
        {
            var l_levelManager = LevelManager.Instance;
            if (l_levelManager.GetDefeatedEnemyCount() >= l_levelManager.TotalEnemies)
                return;
            
            if (m_timer > Time.time || m_currEnemyOnGrid >= maxEnemyOnGrid)
                return;

            m_timer = Time.time + delayToSpawn;
            var l_enemy = LevelManager.Instance.GetEnemyForPool();
            
            if (l_enemy == default)
                return;
            
            l_enemy.InitializeEnemy(grid);
            AddEnemyCount();
        }
        
        public void SubscribeUpdateManager()
        {
            UpdateManager.Instance.AddListener(this);
        }

        public void UnSubscribeUpdateManager()
        {
            UpdateManager.Instance.RemoveListener(this);
        }

        public void AddEnemyCount() => m_currEnemyOnGrid++;
        public void ReduceEnemyCount() => m_currEnemyOnGrid--;
    }
}