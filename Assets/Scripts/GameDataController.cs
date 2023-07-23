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
    private static string gameData = null, basePath, filePath, newDirectoryPath;
    private static bool newDirectory = false;

    private void Start()
    {
        if (!Directory.Exists(GetDirectory()))
        {
            newDirectory = true;
            newDirectoryPath = GetNewDirectory();
            Directory.CreateDirectory($"{newDirectoryPath}\\Endings");
            Directory.CreateDirectory($"{newDirectoryPath}\\SaveStates");
        }

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

        LoadEndings();
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    public static void NewGame()
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
        if (newDirectory)
        {
            filePath = Path.Combine(newDirectoryPath, "SaveStates\\SaveState.txt");
            return filePath;
        }
        else
        {
            basePath = new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath.Split(new string[] { "\\TextGame_EchoesOfDelusions" }, StringSplitOptions.None)[0];
            filePath = Path.Combine(basePath, "TextGame_EchoesOfDelusions\\SaveStates\\SaveState.txt");
            return filePath;
        }
    }

    private static string GetEndings(string ending)
    {
        if (newDirectory)
        {
            filePath = Path.Combine(newDirectoryPath, $"\\Endings\\{ending}.txt");
            return filePath;
        }
        else
        {
            basePath = new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath.Split(new string[] { "\\TextGame_EchoesOfDelusions" }, StringSplitOptions.None)[0];
            filePath = Path.Combine(basePath, $"TextGame_EchoesOfDelusions\\Endings\\{ending}.txt");
            return filePath;
        }    
    }

    private static string GetDirectory()
    {
        basePath = new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath.Split(new string[] { "\\TextGame_EchoesOfDelusions" }, StringSplitOptions.None)[0];
        filePath = Path.Combine(basePath, "TextGame_EchoesOfDelusions");
        return filePath;
    }

    private static string GetNewDirectory()
    {
        basePath = new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath.Split(new string[] { "\\Downloads" }, StringSplitOptions.None)[0];
        filePath = Path.Combine(basePath, "Desktop\\TextGame_EchoesOfDelusions");

        if(!Directory.Exists(filePath))
        {
            basePath = new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath.Split(new string[] { "\\Documents" }, StringSplitOptions.None)[0];
            filePath = Path.Combine(basePath, "Desktop\\TextGame_EchoesOfDelusions");
        }

        return filePath;
    }

    public static void SaveEndings(string ending, string endingPasscode)
    {
        File.WriteAllText(GetEndings(ending), endingPasscode);
    }

    public static void LoadEndings()
    {
        if (File.Exists(GetEndings("Succumbed Ending (1of2)")) && File.ReadAllText(GetEndings("Succumbed Ending (1of2)")).Trim() == "Too")
            EndingsController.succumbedEndingStar_LeftChecker = true;
        
        if (File.Exists(GetEndings("Succumbed Ending (2of2)")) && File.ReadAllText(GetEndings("Succumbed Ending (2of2)")).Trim() == "stare")
            EndingsController.succumbedEndingStar_RightChecker = true;
        
        if (File.Exists(GetEndings("Reconciliation_Ending")) && File.ReadAllText(GetEndings("Reconciliation_Ending")).Trim() == "Please!")
            EndingsController.reconciliationEndingStar_Checker = true;
        
        if (File.Exists(GetEndings("Status Quo Ending (1of2)")) && File.ReadAllText(GetEndings("Status Quo Ending (1of2)")).Trim() == "Do")
            EndingsController.statusQuoEndingStar_LeftChecker = true;
        
        if (File.Exists(GetEndings("Status Quo Ending (2of2)")) && File.ReadAllText(GetEndings("Status Que Ending (2of2)")).Trim() == "Long!")
            EndingsController.statusQuoEndingStar_RightChecker = true;
        
        if (File.Exists(GetEndings("Solitude Ending (1of2)")) && File.ReadAllText(GetEndings("Solitude Ending (1of2)")).Trim() == "Not")
            EndingsController.solitudeEndingStar_LeftChecker = true;
        
        if (File.Exists(GetEndings("Solitude Ending (2of2)")) && File.ReadAllText(GetEndings("Solitude Ending (2of2)")).Trim() == "For")
            EndingsController.solitudeEndingStar_RightChecker = true;
    }
}