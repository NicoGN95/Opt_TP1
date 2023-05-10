using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _Main.Scripts.Manager
{
    public class UiManager : MonoBehaviour
    {
        [SerializeField] private Text defeatedEnemyCountText;
        [SerializeField] private Text remainingEnemyCountText;
        [SerializeField] private GameObject gameOverPanel;

        private void Start()
        {
            remainingEnemyCountText.text = LevelManager.Instance.TotalEnemies.ToString();
            gameOverPanel.SetActive(false);
        }

        public void SetInfoEnemiesTextInUi(int p_defeatedEnemyCount, int p_remainingEnemyCount)
        {
            defeatedEnemyCountText.text = p_defeatedEnemyCount.ToString();
            remainingEnemyCountText.text = p_remainingEnemyCount.ToString();

            if (p_remainingEnemyCount <= 0)
            {
                OnGameOver();
            }
        }

        public void OnGameOver()
        {
            gameOverPanel.SetActive(true);

            if (InputManager.Instance.TryGetInputAction("Enter", out var l_action))
            {
                l_action.performed += ActionOnPerformed;
            }
        }

        private void ActionOnPerformed(InputAction.CallbackContext p_obj)
        {
            SceneManager.LoadScene("MainMenuScene");
        }
    }
}