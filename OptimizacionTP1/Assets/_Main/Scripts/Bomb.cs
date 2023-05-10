using System;
using _Main.Scripts.Interface;
using _Main.Scripts.ScriptableObject;
using _Main.Scripts.Update;
using UnityEngine;

namespace _Main.Scripts
{
    public class Bomb : MonoBehaviour, IUpdateObject
    {
        [SerializeField] private BombData data;

        private bool m_isActivate;
        private float m_timer;
        private Collider[] m_colliders;
        
        public event Action<Bomb> OnDeactivateBomb;

        private void Awake()
        {
            m_colliders = new Collider[data.ObjectToExplode];
        }

        public void Initialize(Vector3 p_position)
        {
            transform.position = p_position;
            SubscribeUpdateManager();
            gameObject.SetActive(true);
            m_isActivate = false;
            m_timer = Time.time + data.TimeToExplode;
        }

        private void OnDisable()
        {
            UnSubscribeUpdateManager();
        }

        private void OnDestroy()
        {
            UnSubscribeUpdateManager();
        }

        public void MyUpdate()
        {
            if (m_isActivate)
                return;

            if (m_timer > Time.time)
                return;

            var l_objCount =
                Physics.OverlapSphereNonAlloc(transform.position, data.Radius, m_colliders, data.LayerMaskToAffect);
            
            for (var l_i = 0; l_i < l_objCount; l_i++)
            {
                if (m_colliders[l_i].TryGetComponent(out IDeathController l_deathController))
                    l_deathController.SetIsDeath(true);
            }
            
            OnDeactivateBomb?.Invoke(this);
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