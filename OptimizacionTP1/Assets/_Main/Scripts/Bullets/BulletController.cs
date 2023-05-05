using System;
using _Main.Scripts.Extension;
using _Main.Scripts.Interface;
using _Main.Scripts.ScriptableObject.Bullets;
using _Main.Scripts.Update;
using UnityEngine;

namespace _Main.Scripts.Bullets
{
    public class BulletController : MonoBehaviour, IUpdateObject
    {
        private BulletData m_bulletData;
        private Vector3 m_dir;

        public event Action<BulletController> OnDeactivateBullet;

        public void InitializeBullet(BulletData p_bulletData, Vector3 p_dir, Vector3 p_positionToSpawn)
        {
            m_bulletData = p_bulletData;
            m_dir = p_dir;

            transform.position = p_positionToSpawn.X1Z();
            
            gameObject.SetActive(true);
            SubscribeUpdateManager();
        }
        
        public void MyUpdate()
        {
            transform.position += m_dir * (m_bulletData.Speed * Time.deltaTime);
        }

        private void DeactivateBullet()
        {
            OnDeactivateBullet?.Invoke(this);
            UnSubscribeUpdateManager();
        }

        private void OnTriggerEnter(Collider p_other)
        {
            var l_otherLayerMask = p_other.gameObject.layer;
            
            if (l_otherLayerMask == m_bulletData.OwnerLayerId)
                return;

            if (l_otherLayerMask == m_bulletData.DamageLayerId)
                if (p_other.TryGetComponent(out IDeathController l_deathController))
                    l_deathController.SetIsDeath(true);
            
            DeactivateBullet();
        }

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