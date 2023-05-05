using UnityEditor;
using UnityEngine;

namespace _Main.Scripts.Editor
{
    public class CreateMap : EditorWindow
    {
        [MenuItem("Main/CreateMap")]
        private static void ShowWindow()
        {
            var l_window = GetWindow<CreateMap>();
            l_window.titleContent = new GUIContent("Generator Map");
            l_window.Show();
        }

        private void OnGUI()
        {
            var l_gridSize = 12;
            var l_cellSize = 1f;
            var l_gridOrigin = new Vector3(0, 1);
            
            if (!GUILayout.Button("Generate")) 
                return;
            
            var l_grid = new GameObject[l_gridSize, l_gridSize];
            for (var x = 0; x < l_gridSize; x++)
            {
                for (var z = 0; z < l_gridSize; z++)
                {
                    l_grid[x, z] = new GameObject
                    {
                        transform =
                        {
                            position = new Vector3(l_gridOrigin.x + x * l_cellSize, l_gridOrigin.y, l_gridOrigin.z + z * l_cellSize)
                        }
                    };
                }
            }
        }
    }
}