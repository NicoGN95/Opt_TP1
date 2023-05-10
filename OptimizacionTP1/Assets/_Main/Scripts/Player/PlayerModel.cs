using System;
using _Main.Scripts.Extension;
using _Main.Scripts.Interface;
using _Main.Scripts.Manager;
using _Main.Scripts.Update;
using UnityEngine;

namespace _Main.Scripts.Player
{
    public class PlayerModel : MonoBehaviour, IUpdateObject, IDeathController
    {
        [SerializeField] private PlayerData data;
        [SerializeField] private Transform shootTransform;
        [SerializeField] private Transform spawnPoint;
        
        private Rigidbody m_rigidbody;
        private float m_shootCooldown;

        private float m_currDir;

        private void Start()
        {
            m_rigidbody = GetComponent<Rigidbody>();
            SubscribeUpdateManager();
            transform.position = spawnPoint.position;
        }

        public void MyUpdate()
        {
            Move();
        }
        public void Shoot()
        {
            var l_bullet = LevelManager.Instance.GetBulletForPool();
            l_bullet.InitializeBullet(data.BulletData, transform.forward, shootTransform.position);
        }

        public void Move()
        {
            var l_force = transform.forward * (m_currDir * data.MovementSpeed);
            
            if (l_force.magnitude < 0.25f)
                return;
            
            m_rigidbody.AddForce(l_force);
        }

        private void OnDestroy()
        {
            UnSubscribeUpdateManager();
        }

        public void Rotate(float p_dirToRotate)
        {
            transform.Rotate(new Vector3(0f, 90 * p_dirToRotate));
        }
        
        public PlayerData GetData() => data;
        public void SetCurrDir(float p_dir) => m_currDir = p_dir;

        private void Die()
        {
            transform.position = spawnPoint.position;
            transform.rotation = Quaternion.Euler(Vector3.zero);
        }
        public void SubscribeUpdateManager()
        {
            UpdateManager.Instance.AddListener(this);
        }

        public void UnSubscribeUpdateManager()
        {
            UpdateManager.Instance.RemoveListener(this);
        }

        public bool GetIsDeath()
        {
            return false;
        }

        public void SetIsDeath(bool p_value) => Die();


        private void OnCollisionEnter(Collision p_collision)
        {
            var l_otherLayerMask = p_collision.gameObject.layer;
            
            if (l_otherLayerMask != data.EnemyLayerId)
                return;

            if (!p_collision.gameObject.TryGetComponent(out IDeathController l_deathController)) 
                return;
            
            l_deathController.SetIsDeath(true);
            Die();
        }
    }
}