using _Main.Scripts.Enemies;

namespace _Main.Scripts.ScriptableObject.Enemies.States.Conditions
{
    public abstract class StateCondition : UnityEngine.ScriptableObject
    {
        public abstract bool ConditionComplete(EnemyModel p_model);

    }
}
