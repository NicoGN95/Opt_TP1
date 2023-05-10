using UnityEngine;

namespace _Main.Scripts.ScriptableObject
{
    [CreateAssetMenu(menuName = "Main/Bombs/BombData")]
    public class BombData : UnityEngine.ScriptableObject
    {
        [field: SerializeField] public float TimeToExplode { get; private set; }
        [field: SerializeField] public float Radius { get; private set; }
        [field: SerializeField] public int ObjectToExplode { get; private set; }
        [field: SerializeField] public LayerMask LayerMaskToAffect { get; private set; }
    }
}