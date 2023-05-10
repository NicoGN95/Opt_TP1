using _Main.Scripts.Manager;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Main.Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        private PlayerModel m_model;
        private PlayerData m_data;
        private float m_shootCooldownTime;
        
        private void Start()
        {
            m_model = GetComponent<PlayerModel>();
            m_data = m_model.GetData();

            var l_inputManager = InputManager.Instance;
            if(l_inputManager.TryGetInputAction(m_data.MovementID, out var l_movementAction))
            {
                l_movementAction.performed += MovementActionOnPerformed;
                l_movementAction.canceled += MovementActionOnCanceled;
            }

            if (l_inputManager.TryGetInputAction(m_data.RotationID, out var l_rotateAction))
            {
                l_rotateAction.performed += RotateActionOnPerformed;
            }
            
            if(l_inputManager.TryGetInputAction(m_data.ShootID, out var l_shootAction))
            {
                l_shootAction.performed += ShootActionOnPerformed;
            }
        }

        private void OnDestroy()
        {
            var l_inputManager = InputManager.Instance;
            if(l_inputManager.TryGetInputAction(m_data.MovementID, out var l_movementAction))
            {
                l_movementAction.performed -= MovementActionOnPerformed;
                l_movementAction.canceled -= MovementActionOnCanceled;
            }

            if (l_inputManager.TryGetInputAction(m_data.RotationID, out var l_rotateAction))
            {
                l_rotateAction.performed -= RotateActionOnPerformed;
            }
            
            if(l_inputManager.TryGetInputAction(m_data.ShootID, out var l_shootAction))
            {
                l_shootAction.performed -= ShootActionOnPerformed;
            }
        }

        private void MovementActionOnCanceled(InputAction.CallbackContext p_obj)
        {
            m_model.SetCurrDir(0);
        }

        private void MovementActionOnPerformed(InputAction.CallbackContext p_context)
        {
            var l_value = p_context.ReadValue<float>();
            m_model.SetCurrDir(l_value);
        }

        private void RotateActionOnPerformed(InputAction.CallbackContext p_context)
        {
            var l_value = p_context.ReadValue<float>();
            m_model.Rotate(l_value);
        }

        private void ShootActionOnPerformed(InputAction.CallbackContext p_context)
        {
            if (m_shootCooldownTime - Time.time < 0)
            {
                m_model.Shoot();
                m_shootCooldownTime = Time.time + m_data.DelayTimeToShoot;
            }
        }
    }
}