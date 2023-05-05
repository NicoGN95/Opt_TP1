using _Main.Scripts.ScriptableObject.Enemies.States.Conditions;
using _Main.Scripts.ScriptableObject.Enemies.States;

namespace _Main.Scripts.Enemies
{
    public class CustomState
    {
        public int StateIndex { get; set; }
        public State State;
        public StateCondition[] ConditionsToExitState;
        public CustomState[] ExitStates;
    }
}