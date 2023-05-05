using _Main.Scripts.Enemies;
using UnityEngine;

namespace _Main.Scripts.ScriptableObject.Enemies.States
{
    [CreateAssetMenu(menuName = "Main/Enemies/States/ShootState")]
    public class ShootState : State
    {
        public override void EnterState(EnemyModel p_model)
        {
            p_model.Shoot();
            p_model.OnFinishAttack = true;
        }

        public override void ExecuteState(EnemyModel p_model)
        {
            
        }

        public override void ExitState(EnemyModel p_model)
        {
            p_model.OnFinishAttack = false;
        }
    }
}