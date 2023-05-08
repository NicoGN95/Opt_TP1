using _Main.Scripts.Bullets;
using _Main.Scripts.Enemies;
using UnityEngine;

namespace _Main.Scripts.Manager
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager Instance;
        public const int TOTAL_ENEMIES = 4;

        [SerializeField] private BulletController bulletPrefab;
        [SerializeField] private EnemyModel enemyModelPrefab;
        [SerializeField] private SpawnerSystem spawnerSystem;
        
        private PoolGeneric<BulletController> m_bulletPool;
        private PoolGeneric<EnemyModel> m_enemiesPool;

        
        private int defeatedEnemyCount;
        public int DefeatedEnemyCount => defeatedEnemyCount;
        private int remainingEnemyCount;
        public int RemainingEnemyCount => remainingEnemyCount;
        private void Awake()
        {
            if (Instance != default)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            m_bulletPool = new PoolGeneric<BulletController>(bulletPrefab);
            m_enemiesPool = new PoolGeneric<EnemyModel>(enemyModelPrefab);
            remainingEnemyCount = TOTAL_ENEMIES;
        }

        private void OnDestroy()
        {
            Instance = default;
            m_bulletPool.ClearData();
            m_enemiesPool.ClearData();
        }

        public BulletController GetBulletForPool()
        {
            var l_bullet = m_bulletPool.GetorCreate();
            l_bullet.OnDeactivateBullet += OnDeactivateBulletHandler;
            return l_bullet;
        }

        private void OnDeactivateBulletHandler(BulletController p_bulletController)
        {
            p_bulletController.OnDeactivateBullet -= OnDeactivateBulletHandler;
            p_bulletController.gameObject.SetActive(false);
            m_bulletPool.AddPool(p_bulletController);
        }
        
        public EnemyModel GetEnemyForPool()
        {
            var l_bullet = m_enemiesPool.GetorCreate();
            return l_bullet;
        }

        public void DeactivateEnemy(EnemyModel p_enemyModel)
        {
            p_enemyModel.gameObject.SetActive(false);
            m_enemiesPool.AddPool(p_enemyModel);
            spawnerSystem.ReduceEnemyCount();

            defeatedEnemyCount++;
            remainingEnemyCount--;
            UiManager.Instance.OnEnemyDefeated();
        }
    }
}