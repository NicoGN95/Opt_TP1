using _Main.Scripts.ScriptableObject.Bullets;
using UnityEngine;

namespace _Main.Scripts.ScriptableObject.Enemies
{
    [CreateAssetMenu(menuName = "Main/Enemies/EnemyData")]
    public class EnemyData : UnityEngine.ScriptableObject
    {
        [field: SerializeField] public float MovementSpeed { get; private set; }
        [field: SerializeField] public float DelayTimeToShoot { get; private set; }
        [field: SerializeField] public BulletData BulletData { get; private set; }
        [field: SerializeField] public FSMData FsmData { get; private set; }
    }
}