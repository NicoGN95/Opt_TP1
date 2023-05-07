using System;
using _Main.Scripts.ScriptableObject.Bullets;
using _Main.Scripts.ScriptableObject.Enemies;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Main.Scripts.Player
{
    [CreateAssetMenu(menuName = "Main/Player/PlayerData")]
    public class PlayerData : UnityEngine.ScriptableObject
    {
        [field: SerializeField] public float MovementSpeed { get; private set; }
        [field: SerializeField] public float DelayTimeToShoot { get; private set; }
        [field: SerializeField] public BulletData BulletData { get; private set; }
        [field: SerializeField] public String MovementID { get; private set; }
        [field: SerializeField] public String RotationID { get; private set; }
        [field: SerializeField] public String ShootID { get; private set; }
        
    }
}