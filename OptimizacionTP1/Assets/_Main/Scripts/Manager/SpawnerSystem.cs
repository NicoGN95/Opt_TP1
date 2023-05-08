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

        private int currEnemyOnGrid;
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
            
            if(LevelManager.Instance.DefeatedEnemyCount >= LevelManager.TOTAL_ENEMIES)
                return;
            
            if (m_timer > Time.time || currEnemyOnGrid >= maxEnemyOnGrid)
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

        public void AddEnemyCount() => currEnemyOnGrid++;
        public void ReduceEnemyCount() => currEnemyOnGrid--;
    }
}