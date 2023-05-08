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
        private void Start()
        {
            model = GetComponent<PlayerModel>();
            data = model.GetData();
            
            
            if(InputManager.Instance.TryGetInputAction(data.MovementID, out var l_movementAction))
            {
                l_movementAction.performed += MovementActionOnPerformed;
                l_movementAction.canceled += MovementActionOnCanceled;
            }

            if (InputManager.Instance.TryGetInputAction(data.RotationID, out var l_rotateAction))
            {
                l_rotateAction.performed += RotateActionOnPerformed;
                l_rotateAction.canceled += RotateActionOnCanceled;
            }
            if(InputManager.Instance.TryGetInputAction(data.ShootID, out var l_shootAction))
            {
                l_shootAction.performed += ShootActionOnPerformed;
            }
            
        }

        private void MovementActionOnPerformed(InputAction.CallbackContext p_context)
        {
            var l_value = p_context.ReadValue<Vector2>();

            var l_dir =  new Vector3(0, 0, l_value.y);
            model.SetCurrDir(l_value);
        }
        private void MovementActionOnCanceled(InputAction.CallbackContext p_obj)
        {
            model.SetCurrDir(Vector3.zero);
        }

        

        private void RotateActionOnPerformed(InputAction.CallbackContext p_context)
        {
            var l_value = p_context.ReadValue<Vector2>();
            model.SetCurrRot(l_value);
        }
        private void RotateActionOnCanceled(InputAction.CallbackContext p_obj)
        {
            model.SetCurrRot(Vector3.zero);
        }

        private void ShootActionOnPerformed(InputAction.CallbackContext p_context)
        {
            if (shootCooldownTime - Time.time < 0)
            {
                model.Shoot();
                shootCooldownTime = Time.time + data.DelayTimeToShoot;
            }
        }

        
    }
}