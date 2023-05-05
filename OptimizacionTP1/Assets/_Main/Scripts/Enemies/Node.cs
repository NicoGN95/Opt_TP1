using UnityEngine;
using UnityEngine.Serialization;

namespace _Main.Scripts.Enemies
{
    [System.Serializable]
    public class Node
    {
        public readonly bool Walkable;
        [FormerlySerializedAs("WorldPosition")] public Vector3 worldPosition;
        public readonly int GridX;
        public readonly int GridY;
        [FormerlySerializedAs("Parent")] public Node parent;
        [FormerlySerializedAs("Distance")] public float distance;

        public Node(bool p_walkable, Vector3 p_worldPos, int p_gridX, int p_gridY)
        {
            Walkable = p_walkable;
            worldPosition = p_worldPos;
            GridX = p_gridX;
            GridY = p_gridY;
        }
    }
}