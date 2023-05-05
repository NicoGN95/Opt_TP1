using _Main.Scripts.Enemies;
using UnityEngine;

namespace _Main.Scripts.ScriptableObject.Enemies.States.Conditions
{
    [CreateAssetMenu(menuName = "Main/Enemies/Conditions/IsMovementEndCondition")]
    public class IsMovementEndCondition : StateCondition
    {
        public override bool ConditionComplete(EnemyModel p_model)
        {
            return !p_model.OnMovement;
        }
    }
}