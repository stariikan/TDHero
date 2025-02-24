<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject pauseMenuUI; // Assign in Inspector
    public GameObject gameOverUI;
    public bool isPaused = false;
    public bool gameOver = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && gameOver == false)
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;

        // Show/hide the pause menu
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(isPaused);
        }
    }
    public void GameOver()
    {
        if (gameOver == false)
        {
            gameOver = true;
            isPaused = !isPaused;
            Time.timeScale = isPaused ? 0f : 1f;

            // Show/hide the pause menu
            if (pauseMenuUI != null)
            {
                gameOverUI.SetActive(true);
            }
        }
    }
    public void ResumeGame()
    {
        if (isPaused && gameOver == false) TogglePause();
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Ensure time resumes before restarting
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ReturnToMenu()
    {
        Time.timeScale = 1f; // Ensure time resumes before restarting
        SceneManager.LoadScene("");
    }
    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}
=======
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject pauseMenuUI; // Assign in Inspector
    public GameObject gameOverUI;
    public bool isPaused = false;
    public bool gameOver = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && gameOver == false)
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;

        // Show/hide the pause menu
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(isPaused);
        }
    }
    public void GameOver()
    {
        if (gameOver == false)
        {
            gameOver = true;
            isPaused = !isPaused;
            Time.timeScale = isPaused ? 0f : 1f;

            // Show/hide the pause menu
            if (pauseMenuUI != null)
            {
                gameOverUI.SetActive(true);
            }
        }
    }
    public void ResumeGame()
    {
        if (isPaused && gameOver == false) TogglePause();
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Ensure time resumes before restarting
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ReturnToMenu()
    {
        Time.timeScale = 1f; // Ensure time resumes before restarting
        SceneManager.LoadScene("");
    }
    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}
>>>>>>> 8341d68b8fd658505bbd1e276ebbe49078627311
