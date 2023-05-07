using System;
using _Main.Scripts.Manager;
using _Main.Scripts.Update;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Main.Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        private PlayerModel model;
        private PlayerData data;
        private float shootCooldownTime;
        private void Awake()
        {
            model = GetComponent<PlayerModel>();
            data = model.GetData();
            var l_inputManager = InputManager.Instance;
            
            
            if(l_inputManager.TryGetInputAction(data.MovementID, out var l_movementAction))
            {
                l_movementAction.performed += MovementActionOnPerformed;
            }

            if (l_inputManager.TryGetInputAction(data.RotationID, out var l_rotateAction))
            {
                l_rotateAction.performed += RotateActionOnPerformed;
            }
            if(l_inputManager.TryGetInputAction(data.ShootID, out var l_shootAction))
            {
                l_shootAction.performed += ShootActionOnPerformed;
            }
            
        }

        private void RotateActionOnPerformed(InputAction.CallbackContext p_obj)
        {
            throw new NotImplementedException();
        }


        private void ShootActionOnPerformed(InputAction.CallbackContext p_obj)
        {
            if (shootCooldownTime - Time.time < 0)
            {
                model.Shoot();
                shootCooldownTime = Time.time + data.DelayTimeToShoot;
            }
        }

        private void MovementActionOnPerformed(InputAction.CallbackContext p_context)
        {
            var l_value = p_context.ReadValue<Vector2>();

            var l_dir = new Vector3(l_value.x, 0, l_value.y);
            model.Move(l_dir);
        }
    }
}