using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    [SerializeField] private int gridSize = 12; 
    [SerializeField] private float cellSize = 1.0f; 
    [SerializeField] private Vector3 gridOrigin = Vector3.zero; 
    public int GridSize => gridSize;

    private Vector3[,] grid;

    void Start()
    {
        grid = new Vector3[gridSize, gridSize];
        for (int x = 0; x < gridSize; x++)
        {
            for (int z = 0; z < gridSize; z++)
            {
                grid[x, z] = new Vector3(gridOrigin.x + x * cellSize, gridOrigin.y, gridOrigin.z + z * cellSize);
            }
        }
    }

    public Vector3 GetCellPosition(int x, int z)
    {
        if (x < 0 || x >= gridSize || z < 0 || z >= gridSize)
        {
            Debug.LogError("Invalid cell position");
            return Vector3.zero;
        }
        return grid[x, z];
    }


    public Vector2Int GetCellIndex(Vector3 position)
    {
        int x = Mathf.FloorToInt((position.x - gridOrigin.x) / cellSize);
        int z = Mathf.FloorToInt((position.z - gridOrigin.z) / cellSize);
        return new Vector2Int(x, z);
    }

    // draw the grid in the scene view
    void OnDrawGizmosSelected()
    {
        if (grid == null) { return; }


        Gizmos.color = Color.white;
        for (int x = 0; x < gridSize; x++)
        {
            for (int z = 0; z < gridSize; z++)
            {
                Gizmos.DrawWireCube(grid[x, z], new Vector3(cellSize, 0, cellSize));
            }
        }
    }
}
