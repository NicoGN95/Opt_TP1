using _Main.Scripts.Interface;
using _Main.Scripts.Manager;
using _Main.Scripts.ScriptableObject.Enemies;
using UnityEngine;

namespace _Main.Scripts.Enemies
{
    public class EnemyModel : MonoBehaviour, IDeathController
    {
        [SerializeField] private EnemyData data;
        [SerializeField] private Transform shootTransform;
        [SerializeField] private EnemyController enemyController;

        private Pathfinder m_pathfinder;

        private bool m_isDeath;
        public bool OnMovement { get; set; }
        public bool OnFinishAttack { get; set; }

        public void InitializeEnemy(Grid p_grid)
        {
            transform.position = p_grid.GetRandomPos();
            
            gameObject.SetActive(true);
            
            m_pathfinder = new Pathfinder(p_grid);
            m_isDeath = false;

            enemyController.SubscribeUpdateManager();
            enemyController.InitializeController(this);
        }

        public Pathfinder GetPathFinder() => m_pathfinder;
        public EnemyData GetData() => data;
        
        public bool GetIsDeath() => m_isDeath;
        public void SetIsDeath(bool p_value) => m_isDeath = p_value;

        public void Shoot()
        {
            var l_bullet = LevelManager.Instance.GetBulletForPool();
            l_bullet.InitializeBullet(data.BulletData, transform.forward, shootTransform.position);
        }
    }
}