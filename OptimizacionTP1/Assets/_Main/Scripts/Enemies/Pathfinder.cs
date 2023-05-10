using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Main.Scripts.Enemies
{
    [System.Serializable]
    public class Pathfinder
    {
        private readonly List<Vector3> m_path = new List<Vector3>();

        public List<Vector3> GetPath() => m_path;

        private readonly Grid m_grid;

        public Pathfinder(Grid p_grid)
        {
            m_grid = p_grid;
        }
        
        public void FindPath(Vector3 p_initialPos)
        {
            var l_seekerNode = m_grid.NodeFromWorldPoint(p_initialPos);
            var l_targetNode = m_grid.GetRandomNode(l_seekerNode);

            var l_openSet = new List<Node>();
            var l_closedSet = new HashSet<Node>();


            l_openSet.Add(l_seekerNode);

            while (l_openSet.Count > 0)
            {
                var l_nodeZero = l_openSet[0];
                if (l_nodeZero == l_targetNode)
                    break;

                var l_neighbours = m_grid.GetNeighbours(l_nodeZero).Where(p_x => !l_closedSet.Contains(p_x) && !l_openSet.Contains(p_x));

                foreach (var l_node in l_neighbours)
                {
                    l_node.parent = l_nodeZero;
                    l_openSet.Add(l_node);
                }
                l_closedSet.Add(l_nodeZero);
                l_openSet.Remove(l_nodeZero);
                l_openSet = OrderNodesByDistance(l_openSet, l_targetNode);
            }
            RetracePath(l_seekerNode, l_targetNode);
        }
        private void RetracePath(Node p_startNode, Node p_endNode)
        {
            m_path.Clear();
            var l_currentNode = p_endNode;

            
            
            while (l_currentNode != default && l_currentNode != p_startNode)
            {
                m_path.Add(l_currentNode.worldPosition);
                l_currentNode = l_currentNode.parent;
            }
            m_path.Reverse();
        }

        private static List<Node> OrderNodesByDistance(List<Node> p_nodes, Node p_targetNode)
        {
            foreach ( var l_node in p_nodes) 
            {
                var l_valueX = MathF.Abs(l_node.GridX - p_targetNode.GridX);
                var l_valueY = MathF.Abs(l_node.GridY - p_targetNode.GridY);
                l_node.distance = l_valueX + l_valueY;
            }

            return p_nodes.OrderBy(p_x => p_x.distance).ToList();
        }

        public Vector3 GetPointByIndex(int p_index) => m_path[p_index];
    }
}
