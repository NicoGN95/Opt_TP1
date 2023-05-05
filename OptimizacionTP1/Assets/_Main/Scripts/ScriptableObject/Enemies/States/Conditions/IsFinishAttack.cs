using _Main.Scripts.Enemies;
using UnityEngine;

namespace _Main.Scripts.ScriptableObject.Enemies.States.Conditions
{
    [CreateAssetMenu(menuName = "Main/Enemies/Conditions/IsFinishAttack")]
    public class IsFinishAttack : StateCondition
    {
        public override bool ConditionComplete(EnemyModel p_model)
        {
            return p_model.OnFinishAttack;
        }
    }
}