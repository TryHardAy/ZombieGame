using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public Text scoreText;  // Ensure this is assigned in the Inspector
    private int cumulatedXP;
    private bool isActive = false;

    void Start()
    {
        gameOverPanel.SetActive(isActive); // Hide the game over panel at the start
    }

    public void ShowGameOver(int xp)
    {
        isActive = !isActive;

        if (isActive)
        {
            cumulatedXP = xp;
            scoreText.text = "Score: " + cumulatedXP.ToString();
            gameOverPanel.SetActive(true);

            // Pause the game
            Time.timeScale = 0;

            // Show the cursor and unlock it
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            gameOverPanel.SetActive(false);

            // Unpause the game
            Time.timeScale = 1;

            // Show the cursor and unlock it
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void Retry()
    {
        // Unpause the game
        Time.timeScale = 1;

        // Hide the cursor and lock it
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Exit()
    {
        // Unpause the game before quitting
        Time.timeScale = 1;
        
#if UNITY_EDITOR
        // End simulation in Unity editor
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Exit the game in build
        Application.Quit();
#endif
        Debug.Log("Game is exiting");
    }
}
