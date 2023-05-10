using _Main.Scripts.Update;
using UnityEngine;

namespace _Main.Scripts.Enemies
{
    public class EnemyController : MonoBehaviour, IUpdateObject
    {
        private StateMachine m_stateMachine;

        public void InitializeController(EnemyModel p_model)
        {
            m_stateMachine = new StateMachine();
            m_stateMachine.InitializeStateMachine(p_model);
        }

        private void OnDisable()
        {
            UnSubscribeUpdateManager();
        }

        public void MyUpdate()
        {
            m_stateMachine?.RunStateMachine();
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