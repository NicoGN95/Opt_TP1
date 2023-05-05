using _Main.Scripts.Enemies;

namespace _Main.Scripts.ScriptableObject.Enemies.States
{
    public abstract class State : UnityEngine.ScriptableObject
    {
        public virtual void EnterState(EnemyModel p_model)
        {
        }

        public abstract void ExecuteState(EnemyModel p_model);

        public virtual void ExitState(EnemyModel p_model)
        {
        }
    }
}