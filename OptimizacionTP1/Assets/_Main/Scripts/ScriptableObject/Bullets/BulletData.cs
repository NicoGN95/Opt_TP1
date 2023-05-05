using UnityEngine;

namespace _Main.Scripts.ScriptableObject.Bullets
{
    [CreateAssetMenu(menuName = "Main/Bullets/BulletData")]
    public class BulletData : UnityEngine.ScriptableObject
    {
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public int OwnerLayerId { get; private set; }
        [field: SerializeField] public int DamageLayerId { get; private set; }
    }
}