using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            TitleScene();
    }

    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void TitleScene()
    {
        GameDataController.SaveGame();
        SceneManager.LoadScene("Title_Scene");
        StoryController.storyIndex = 0;
    }

    public void ReturnToMainMenu()
    {
        GameDataController.NewGame();
        SceneManager.LoadScene("Title_Scene");
        StoryController.storyIndex = 0;
    }
}