using _Main.Scripts.ScriptableObject.Enemies.States;
using UnityEngine;

namespace _Main.Scripts.ScriptableObject.Enemies
{
    [CreateAssetMenu(menuName = "Main/Enemies/FSMData")]
    public class FSMData : UnityEngine.ScriptableObject
    {
        public StateData[] fsmStates;
    }
}