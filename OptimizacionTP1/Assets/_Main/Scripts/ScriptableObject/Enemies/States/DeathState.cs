using _Main.Scripts.Enemies;
using _Main.Scripts.Manager;
using UnityEngine;

namespace _Main.Scripts.ScriptableObject.Enemies.States
{
    [CreateAssetMenu(menuName = "Main/Enemies/States/DeathState")]
    public class DeathState : State
    {
        public override void EnterState(EnemyModel p_model)
        {
            LevelManager.Instance.DeactivateEnemy(p_model);
        }

        public override void ExecuteState(EnemyModel p_model)
        {
            
        }
    }
}