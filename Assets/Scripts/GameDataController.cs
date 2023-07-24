using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameDataController : MonoBehaviour
{
    [SerializeField]
    private Button loadGameButton;
    [SerializeField]
    private TextMeshProUGUI loadGameText;
    private static string gameData = null, basePath, filePath, saveStatePath, endingsPath;

    private void Start()
    {
        SetDirectory();
        CheckLoadButton();
        LoadEndings();
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private void SetDirectory()
    {
        basePath = Application.streamingAssetsPath.Split("/TextGame_EchoesOfDelusions")[0];
        filePath = Path.Combine(basePath, "TextGame_EchoesOfDelusions");

        if (!Directory.Exists(filePath) || (filePath == string.Empty ^ filePath == null))
        {
            basePath = Application.streamingAssetsPath.Split("/Echoes Of Delusions")[0];
            filePath = Path.Combine(basePath, "Echoes Of Delusions");

            if (!Directory.Exists(filePath))
            {
                basePath = Application.streamingAssetsPath.Split("/Echoes Of Delusions_Data")[0];
                filePath = Path.Combine(basePath, "Echoes Of Delusions_Data");

                if (!Directory.Exists(filePath))
                    Directory.CreateDirectory(filePath);
            }
        }

        basePath = filePath;
        filePath = Path.Combine(basePath, "GameData");

        if (!Directory.Exists(filePath))
            Directory.CreateDirectory(filePath);

        saveStatePath = Path.Combine(filePath, "SaveStates");
        endingsPath = Path.Combine(filePath, "Endings");

        if (!Directory.Exists(saveStatePath))
            Directory.CreateDirectory(saveStatePath);

        if (!Directory.Exists(endingsPath))
            Directory.CreateDirectory(endingsPath);
    }

    private void CheckLoadButton()
    {
        if (File.Exists(GetSaveState()))
            gameData = File.ReadAllText(GetSaveState()).Trim();

        if ((gameData != null && gameData != string.Empty) || File.Exists(GetSaveState()))
        {
            loadGameButton.interactable = true;
            loadGameText.colorGradient = new VertexGradient(Color.black, Color.black, Color.white, Color.white);
        }
        else
        {
            loadGameButton.interactable = false;
            loadGameText.color = Color.black;
        }
    }

    public static void LoadEndings()
    {
        if (File.Exists(GetEndings("Succumbed Ending (1of2)")) && File.ReadAllText(GetEndings("Succumbed Ending (1of2)")).Trim() == "Too")
            EndingsController.succumbedEndingStar_LeftChecker = true;
        else
            EndingsController.succumbedEndingStar_LeftChecker = false;

        if (File.Exists(GetEndings("Succumbed Ending (2of2)")) && File.ReadAllText(GetEndings("Succumbed Ending (2of2)")).Trim() == "Stare")
            EndingsController.succumbedEndingStar_RightChecker = true;
        else
            EndingsController.succumbedEndingStar_RightChecker = false;

        if (File.Exists(GetEndings("Reconciliation Ending")) && File.ReadAllText(GetEndings("Reconciliation Ending")).Trim() == "Please!")
            EndingsController.reconciliationEndingStar_Checker = true;
        else
            EndingsController.reconciliationEndingStar_Checker = false;

        if (File.Exists(GetEndings("Status Quo Ending (1of2)")) && File.ReadAllText(GetEndings("Status Quo Ending (1of2)")).Trim() == "Do")
            EndingsController.statusQuoEndingStar_LeftChecker = true;
        else
            EndingsController.statusQuoEndingStar_LeftChecker = false;

        if (File.Exists(GetEndings("Status Quo Ending (2of2)")) && File.ReadAllText(GetEndings("Status Quo Ending (2of2)")).Trim() == "Long!")
            EndingsController.statusQuoEndingStar_RightChecker = true;
        else
            EndingsController.statusQuoEndingStar_RightChecker = false;

        if (File.Exists(GetEndings("Solitude Ending (1of2)")) && File.ReadAllText(GetEndings("Solitude Ending (1of2)")).Trim() == "Not")
            EndingsController.solitudeEndingStar_LeftChecker = true;
        else
            EndingsController.solitudeEndingStar_LeftChecker = false;

        if (File.Exists(GetEndings("Solitude Ending (2of2)")) && File.ReadAllText(GetEndings("Solitude Ending (2of2)")).Trim() == "For")
            EndingsController.solitudeEndingStar_RightChecker = true;
        else
            EndingsController.solitudeEndingStar_RightChecker = false;
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
        if (StoryController.scene.name != "Title_Scene" && StoryController.scene.name != "Loader_Scene")
        {
            gameData = StoryController.scene.name + "\n" + StoryController.storyIndex;
            File.WriteAllText(GetSaveState(), gameData);
        }
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
        return Path.Combine(saveStatePath, "SaveState.txt").ToString();
    }

    private static string GetEndings(string ending)
    {
        return Path.Combine(endingsPath, $"{ending}.txt").ToString();
    }

    public static void SaveEndings(string ending, string endingPasscode)
    {
        File.WriteAllText(GetEndings(ending), endingPasscode);
    }

    public void ResetGame()
    {
        if (File.Exists(GetEndings("Succumbed Ending (1of2)")) && File.ReadAllText(GetEndings("Succumbed Ending (1of2)")).Trim() == "Too")
            File.Delete(GetEndings("Succumbed Ending (1of2)"));

        if (File.Exists(GetEndings("Succumbed Ending (2of2)")) && File.ReadAllText(GetEndings("Succumbed Ending (2of2)")).Trim() == "Stare")
            File.Delete(GetEndings("Succumbed Ending (2of2)"));

        if (File.Exists(GetEndings("Reconciliation Ending")) && File.ReadAllText(GetEndings("Reconciliation Ending")).Trim() == "Please!")
            File.Delete(GetEndings("Reconciliation Ending"));

        if (File.Exists(GetEndings("Status Quo Ending (1of2)")) && File.ReadAllText(GetEndings("Status Quo Ending (1of2)")).Trim() == "Do")
            File.Delete(GetEndings("Status Quo Ending (1of2)"));

        if (File.Exists(GetEndings("Status Quo Ending (2of2)")) && File.ReadAllText(GetEndings("Status Quo Ending (2of2)")).Trim() == "Long!")
            File.Delete(GetEndings("Status Quo Ending (2of2)"));

        if (File.Exists(GetEndings("Solitude Ending (1of2)")) && File.ReadAllText(GetEndings("Solitude Ending (1of2)")).Trim() == "Not")
            File.Delete(GetEndings("Solitude Ending (1of2)"));

        if (File.Exists(GetEndings("Solitude Ending (2of2)")) && File.ReadAllText(GetEndings("Solitude Ending (2of2)")).Trim() == "For")
            File.Delete(GetEndings("Solitude Ending (2of2)"));

        NewGame();
        SceneController.LoadScene(SceneManager.GetActiveScene().name);
    }
}
