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
        if (File.Exists(GetAnyPath())) 
            gameData = File.ReadAllText(GetAnyPath()).Trim();

        if (gameData == null || gameData == string.Empty || !File.Exists(GetAnyPath()))
        {
            loadGameButton.interactable = false;
            MouseController.isInteractable = loadGameButton.IsInteractable();
            loadGameText.color = Color.black;
        }
        else
        {
            loadGameButton.interactable = true;
            MouseController.isInteractable = loadGameButton.IsInteractable();
            loadGameText.colorGradient = new VertexGradient(Color.black, Color.black, Color.white, Color.white);
        }
    }

    public void NewGame()
    {
        if (File.Exists(GetAnyPath()))
            File.Delete(GetAnyPath());
    }

    public static void SaveGame()
    {
        gameData = StoryController.scene.name + "\n" + StoryController.storyIndex;
        if (!File.Exists(GetAnyPath()))
            File.WriteAllText(GetAnyPath(), gameData);  //Create A File And Write The Receipt
    }

    public static void LoadGame()
    {
        SceneController.LoadScene(gameData.Split('\n')[0].Trim());
        StoryController.storyIndex = Convert.ToDouble(gameData.Split('\n')[1].Trim()); 
    }

    public static bool GameDataChecker()
    {
        if (gameData == null || gameData == string.Empty || !File.Exists(GetAnyPath()))
            return false;
        else
            return true;
    }

    private static string GetAnyPath()
    {
        var basePath = new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath.Split(new string[] { "\\TextGame_EchoesOfDelusions" }, StringSplitOptions.None)[0];
        var filePath = Path.Combine(basePath, "TextGame_EchoesOfDelusions\\SaveStates\\SaveState.txt");
        return filePath;
    }
}