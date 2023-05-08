using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _Main.Scripts.Manager
{
    public class UiManager : MonoBehaviour
    {
        public static UiManager Instance;
        
        [SerializeField] private Text defeatedEnemyCountText;
        [SerializeField] private Text remainingEnemyCountText;
        [SerializeField] private GameObject gameOverPanel;
        
        
        private void Awake()
        {
            if (Instance != default)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        private void Start()
        {
            remainingEnemyCountText.text = LevelManager.TOTAL_ENEMIES.ToString();
            gameOverPanel.SetActive(false);
        }

        public void OnEnemyDefeated()
        {

            defeatedEnemyCountText.text = LevelManager.Instance.DefeatedEnemyCount.ToString();
            remainingEnemyCountText.text = LevelManager.Instance.RemainingEnemyCount.ToString();

            if (LevelManager.Instance.RemainingEnemyCount <= 0)
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