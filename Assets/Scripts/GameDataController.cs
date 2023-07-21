using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameDataController : MonoBehaviour
{
    [SerializeField]
    private Button loadGameButton;
    [SerializeField]
    private TextMeshProUGUI loadGameText;
    private static string gameData = null;

    private void Start()
    {
        if (File.Exists(GetSaveState())) 
            gameData = File.ReadAllText(GetSaveState()).Trim();

        if (!(gameData != null ^ gameData != string.Empty) || File.Exists(GetSaveState()))
        {
            loadGameButton.interactable = true;
            MouseController.isInteractable = loadGameButton.IsInteractable();
            loadGameText.colorGradient = new VertexGradient(Color.black, Color.black, Color.white, Color.white);
        }
        else
        {
            loadGameButton.interactable = false;
            MouseController.isInteractable = loadGameButton.IsInteractable();
            loadGameText.color = Color.black;
        }
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    public void NewGame()
    {
        if (File.Exists(GetSaveState()))
        {
            File.Delete(GetSaveState());
            gameData = null;
            StoryController.gameDataRetrieved = false;
        }  
    }

    public static void SaveGame()
    {
        gameData = StoryController.scene.name + "\n" + StoryController.storyIndex;
        File.WriteAllText(GetSaveState(), gameData);  //Create A File And Write The Receipt
    }

    public void LoadGame()
    {
        if (File.Exists(GetSaveState()))
            gameData = File.ReadAllText(GetSaveState()).Trim();
        SceneController.LoadScene(gameData.Split('\n')[0].Trim());
        StoryController.storyIndex = Convert.ToDouble(gameData.Split('\n')[1].Trim());
        StoryController.gameDataRetrieved = true;
    }

    private static string GetSaveState()
    {
        var basePath = new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath.Split(new string[] { "\\TextGame_EchoesOfDelusions" }, StringSplitOptions.None)[0];
        var filePath = Path.Combine(basePath, "TextGame_EchoesOfDelusions\\SaveStates\\SaveState.txt");
        return filePath;
    }
}
