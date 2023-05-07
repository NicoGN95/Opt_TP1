using System;
using _Main.Scripts.Manager;
using _Main.Scripts.Update;
using UnityEngine;

namespace _Main.Scripts.Player
{
    public class PlayerModel : MonoBehaviour, IUpdateObject
    {
        [SerializeField] private PlayerData data;
        [SerializeField] private Transform shootTransform;

        private float shootCooldown;

        private float previusFrameTime;
        private float fixedDeltaTime;

        public void MyUpdate()
        {
            fixedDeltaTime = Time.time - previusFrameTime;
            previusFrameTime = Time.time;
        }
        public void Shoot()
        {
            var l_bullet = LevelManager.Instance.GetBulletForPool();
            l_bullet.InitializeBullet(data.BulletData, transform.forward, shootTransform.position);
        }

        public void Move(Vector3 p_dir)
        {
            transform.position += p_dir * (data.MovementSpeed);
        }


        public PlayerData GetData() => data;

        public void SubscribeUpdateManager()
        {
            UpdateManager.Instance.AddListener(this);
        }

        public void UnSubscribeUpdateManager()
        {
            UpdateManager.Instance.RemoveListener(this);
        }
        
        
    }
}