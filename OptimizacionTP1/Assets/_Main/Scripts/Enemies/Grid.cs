using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Main.Scripts.Enemies
{
    public class Grid : MonoBehaviour
    {
        [SerializeField] private LayerMask unWalkableMask;
        [SerializeField] private Vector2 gridWorldSize;
        [SerializeField] private float nodeRadius;

        private Node[,] m_grid;

        private float m_nodeDiameter;
        private int m_gridSizeX, m_gridSizeY;

        private void Awake()
        {
            Initialize();
        }

        [ContextMenu("ReloadGrid")]
        private void Initialize()
        {
            m_nodeDiameter = nodeRadius * 2;
            m_gridSizeX = Mathf.RoundToInt(gridWorldSize.x / m_nodeDiameter);
            m_gridSizeY = Mathf.RoundToInt(gridWorldSize.y / m_nodeDiameter);
            CreateGrid();
        }
        
        private void CreateGrid()
        {
            m_grid = new Node[m_gridSizeX, m_gridSizeY];
            var l_worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;

            var l_test = Vector3.one * (m_nodeDiameter * 0.1f);
            for (var l_x = 0; l_x < m_gridSizeX; l_x++)
            {
                for (var l_y = 0; l_y < m_gridSizeY; l_y++)
                {
                    var l_worldPoint = l_worldBottomLeft + Vector3.right * (l_x * m_nodeDiameter + nodeRadius) + Vector3.forward * (l_y * m_nodeDiameter + nodeRadius);
                    var l_walkable = !(Physics.CheckBox(l_worldPoint, l_test, Quaternion.identity, unWalkableMask));
                    m_grid[l_x, l_y] = new Node(l_walkable, l_worldPoint, l_x, l_y);
                }
            }
        }

        public IEnumerable<Node> GetNeighbours(Node p_node)
        {
            var l_neighbours = new List<Node>();

            if (p_node.GridX-1 >-1)
                l_neighbours.Add(m_grid[(p_node.GridX - 1), p_node.GridY]);

            if (p_node.GridY - 1 > -1)
                l_neighbours.Add(m_grid[p_node.GridX, (p_node.GridY-1)]);

            if (p_node.GridX + 1 <= m_gridSizeX-1)
                l_neighbours.Add(m_grid[(p_node.GridX + 1), p_node.GridY]);

            if (p_node.GridY + 1 <= m_gridSizeY-1)
                l_neighbours.Add(m_grid[p_node.GridX, (p_node.GridY + 1)]);

            return l_neighbours.Where(p_x=> p_x.Walkable);
        }

        public Node NodeFromWorldPoint(Vector3 p_worldPosition)
        {
            var l_position = transform.position;
            var l_percentX = ((p_worldPosition.x - l_position.x) + gridWorldSize.x / 2) / gridWorldSize.x;
            var l_percentY = ((p_worldPosition.z - l_position.z) + gridWorldSize.y / 2) / gridWorldSize.y;
            l_percentX = Mathf.Clamp01(l_percentX);
            l_percentY = Mathf.Clamp01(l_percentY);

            var l_x = Mathf.RoundToInt((m_gridSizeX - 1) * l_percentX);
            var l_y = Mathf.RoundToInt((m_gridSizeY - 1) * l_percentY);
            return m_grid[l_x, l_y];
        }

        public Node GetRandomNode()
        {
            var l_randomX = Random.Range(0, m_gridSizeX);
            var l_randomY = Random.Range(0, m_gridSizeY);

            var l_node = m_grid[l_randomX, l_randomY];

            while (!l_node.Walkable)
            {
                l_randomX =Random.Range(0, m_gridSizeX);
                l_randomY = Random.Range(0, m_gridSizeY);

                l_node = m_grid[l_randomX, l_randomY];
            }

            return l_node;
        }
        
        public Node GetRandomNode(Node p_skipNode)
        {
            var l_randomX = Random.Range(0, m_gridSizeX);
            var l_randomY = Random.Range(0, m_gridSizeY);

            var l_node = m_grid[l_randomX, l_randomY];

            while (!l_node.Walkable || p_skipNode == l_node)
            {
                l_randomX =Random.Range(0, m_gridSizeX);
                l_randomY = Random.Range(0, m_gridSizeY);

                l_node = m_grid[l_randomX, l_randomY];
            }

            return l_node;
        }

        public Vector3 GetRandomPos()
        {
            var l_node = GetRandomNode();

            return l_node.worldPosition;
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));

            if (m_grid == default) 
                return;
        
            foreach (var l_n in m_grid)
            {
                Gizmos.color = (l_n.Walkable) ? Color.white : Color.red;
                
                Gizmos.DrawCube(l_n.worldPosition, Vector3.one * (m_nodeDiameter - .1f));
            }
        }
#endif
    }
}
