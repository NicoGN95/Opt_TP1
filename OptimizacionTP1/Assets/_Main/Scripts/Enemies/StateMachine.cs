using System;
using System.Collections.Generic;
using System.Linq;
using _Main.Scripts.ScriptableObject.Enemies;
using _Main.Scripts.ScriptableObject.Enemies.States;

namespace _Main.Scripts.Enemies
{
    public class StateMachine
    {
        private CustomState[] m_states;
        private CustomState m_currentState;
        private int m_stateIndex;
        private int m_currentStateConditionsAmount;
        private EnemyModel m_enemyModel;

        public void InitializeStateMachine(EnemyModel p_model)
        {
            m_enemyModel = p_model;
            var l_enemyFsmData = p_model.GetData().FsmData;
            InitializedStatesCheck(l_enemyFsmData);
            InitializeStates(l_enemyFsmData);
            m_currentState = m_states[0];
            m_currentState.State.EnterState(p_model);
            m_currentStateConditionsAmount = m_currentState.ConditionsToExitState.Length;
        }

        private void InitializeStates(FSMData p_enemyFsmData)
        {
            //Keep track of created states references
            Dictionary<State, CustomState> l_statesImplemented = new();
            //Initialize state amount
            var l_statesCount = p_enemyFsmData.fsmStates.Length;
            m_states = new CustomState[l_statesCount];

            //Initialize states with custom data
            for (var l_i = 0; l_i < l_statesCount; l_i++)
            {
                m_states[l_i] = new CustomState()
                {
                    State = p_enemyFsmData.fsmStates[l_i].state,
                    StateIndex = l_i,
                    ConditionsToExitState = p_enemyFsmData.fsmStates[l_i].conditionsToExitState,
                    ExitStates = new CustomState[p_enemyFsmData.fsmStates[l_i].exitStates.Length]
                };

                l_statesImplemented.TryAdd(p_enemyFsmData.fsmStates[l_i].state, m_states[l_i]);
            }

            //Fill remaining data with transitions

            for (var l_i = 0; l_i < m_states.Length; l_i++)
            {
                for (var l_j = 0; l_j < m_states[l_i].ExitStates.Length; l_j++)
                {
                    m_states[l_i].ExitStates[l_j] = l_statesImplemented[p_enemyFsmData.fsmStates[l_i].exitStates[l_j].state];
                }
            }
        }

        #region InitializationCheck

        private void InitializedStatesCheck(FSMData p_enemyFsmData)
        {
            if (p_enemyFsmData.fsmStates.Length < 1)
            {
                throw new Exception($"FSM {p_enemyFsmData} has no states assigned");
            }

            for (var l_i = 0; l_i < p_enemyFsmData.fsmStates.Length; l_i++)
            {
                var l_currState = p_enemyFsmData.fsmStates[l_i];

                if (l_currState == null)
                {
                    throw new Exception($"State in position {l_i} is null");
                }

                if (l_currState.exitStates.Length != l_currState.conditionsToExitState.Length)
                {
                    throw new Exception($"State {l_currState} doesn't have the same amount of exits and conditions");
                }

                if (l_currState.exitStates.Any(p_exitState => p_exitState == null))
                {
                    throw new Exception($"State {l_currState} has an invalid exit state");
                }

                if (l_currState.conditionsToExitState.Any(p_condition => p_condition == null))
                {
                    throw new Exception($"State {l_currState} has an invalid exit condition");
                }
            }
        }

        #endregion


        public void RunStateMachine()
        {
            if (m_currentState == default) return;

            for (var l_i = 0; l_i < m_currentStateConditionsAmount; l_i++)
            {
                if (!m_currentState.ConditionsToExitState[l_i].ConditionComplete(m_enemyModel)) 
                    continue;
                
                ChangeState(m_currentState.ExitStates[l_i]);
                return;
            }

            m_currentState.State.ExecuteState(m_enemyModel);
        }

        private void ChangeState(CustomState p_newState)
        {
            m_currentState.State.ExitState(m_enemyModel);
            m_currentState = m_states[p_newState.StateIndex];
            m_currentState.State.EnterState(m_enemyModel);
            m_currentStateConditionsAmount = m_currentState.ConditionsToExitState.Length;
        }
    }
}