using _Main.Scripts.ScriptableObject.Enemies.States.Conditions;
using UnityEngine;

namespace _Main.Scripts.ScriptableObject.Enemies.States
{
    [CreateAssetMenu(menuName = "Main/Enemies/StateData")]
    public class StateData : UnityEngine.ScriptableObject
    {
        public State state;
        public StateCondition[] conditionsToExitState;
        public StateData[] exitStates;
    }
}

