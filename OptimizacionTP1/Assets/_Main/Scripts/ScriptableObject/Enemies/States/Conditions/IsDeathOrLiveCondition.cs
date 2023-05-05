using _Main.Scripts.Enemies;
using UnityEngine;

namespace _Main.Scripts.ScriptableObject.Enemies.States.Conditions
{
    [CreateAssetMenu(menuName = "Main/Enemies/Conditions/IsDeathCondition")]
    public class IsDeathOrLiveCondition : StateCondition
    {
        [SerializeField] private bool isLive;
        public override bool ConditionComplete(EnemyModel p_model)
        {
            return isLive ? !p_model.GetIsDeath() : p_model.GetIsDeath();
        }
    }
}