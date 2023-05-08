using System;
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
        private Rigidbody rigidbody;
        private float shootCooldown;

        private Vector3 currDir;
        private Vector3 currRot;
        
        
        private float previusFrameTime;
        private float fixedDeltaTime;
        private void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
            SubscribeUpdateManager();
            transform.position = spawnPoint.position;
        }

        public void MyUpdate()
        {
            fixedDeltaTime = Time.time - previusFrameTime;
            previusFrameTime = Time.time;
            
            Move(currDir);
            Rotate(currRot);
        }
        public void Shoot()
        {
            var l_bullet = LevelManager.Instance.GetBulletForPool();
            l_bullet.InitializeBullet(data.BulletData, transform.forward, shootTransform.position);
        }

        private void Move(Vector2 p_dir)
        {
            var l_vel = transform.forward;
            
            if (p_dir.y > 0.2)
            {
                l_vel *= p_dir.y * data.MovementSpeed;
            }
            else if (p_dir.y < 0.2)
            {
                l_vel *= p_dir.y * data.MovementSpeed;
            }
            
            rigidbody.AddForce(l_vel, ForceMode.Force);
            
        }

        private void Rotate(Vector3 p_dirToRotate)
        {
            transform.forward = Vector3.Lerp(transform.forward, transform.right * p_dirToRotate.x, fixedDeltaTime * data.RotationSpeed);
        }
        public PlayerData GetData() => data;
        public void SetCurrRot(Vector3 p_rotation) => currRot = p_rotation; 
        public void SetCurrDir(Vector3 p_dir) => currDir = p_dir;

        private void Die()
        {
            transform.position = spawnPoint.position;
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


        private void OnCollisionEnter(Collision collision)
        {
            var l_otherLayerMask = collision.gameObject.layer;
            
            //No entiendo porque no anda con !=
            if (l_otherLayerMask == data.EnemyLayer)
                return;

            if (collision.gameObject.TryGetComponent(out IDeathController l_deathController))
            {
                l_deathController.SetIsDeath(true);
                Die();
            }
        }
    }
}