using System.Collections.Generic;
using _Main.Scripts.Enemies;
using UnityEngine;

namespace _Main.Scripts.ScriptableObject.Enemies.States
{
    [CreateAssetMenu(menuName = "Main/Enemies/States/MovementByPathState")]
    public class MovementByPathState : State
    {
        private class MovementData
        {
            public int IndexPath;
            public int MaxIndex;
            public readonly Pathfinder Pathfinder;
            public float Timer;

            public MovementData(Pathfinder p_pathfinder, float p_timer)
            {
                Pathfinder = p_pathfinder;
                MaxIndex = Pathfinder.GetPath().Count;
                Timer = p_timer;
            }
        }

        private readonly Dictionary<EnemyModel, MovementData> m_dictionaryMovementData =
            new Dictionary<EnemyModel, MovementData>();

        public override void EnterState(EnemyModel p_model)
        {
            var l_pathFinder = p_model.GetPathFinder();
            
            l_pathFinder.FindPath(p_model.transform.position);

            var l_timer = Time.time + p_model.GetData().DelayTimeToShoot;

            m_dictionaryMovementData[p_model] = new MovementData(l_pathFinder, l_timer);

            p_model.OnMovement = true;
        }

        public override void ExecuteState(EnemyModel p_model)
        {
            var l_data = m_dictionaryMovementData[p_model];
            var l_enemyPosition = p_model.transform.position;

            var l_point = l_data.Pathfinder.GetPointByIndex(l_data.IndexPath);

            l_enemyPosition += p_model.transform.forward * (p_model.GetData().MovementSpeed * Time.deltaTime);
            p_model.transform.position = l_enemyPosition;
            p_model.transform.LookAt(l_point);

            if (l_data.Timer < Time.time)
            {
                p_model.OnMovement = false;
            }
            
            var l_distance = Vector3.Distance(l_point, l_enemyPosition);

            if (l_distance < 0.5f)
                l_data.IndexPath++;
            
            if (l_data.IndexPath < l_data.MaxIndex) 
                return;
            l_data.Pathfinder.FindPath(p_model.transform.position);
            l_data.MaxIndex = l_data.Pathfinder.GetPath().Count;
            l_data.IndexPath = 0;
        }

        public override void ExitState(EnemyModel p_model)
        {
            m_dictionaryMovementData.Remove(p_model);
        }
    }
}