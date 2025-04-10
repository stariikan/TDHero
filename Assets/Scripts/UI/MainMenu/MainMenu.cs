using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string sceneToLoad = "GameScene"; // Set the scene name in Inspector
    public string newGame;
    public string multiplayer;
    public string mapEditor;

    public void NewGame()
    {
        Debug.Log("New Game");
        SceneManager.LoadScene(0);
    }
    public void ExitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}
