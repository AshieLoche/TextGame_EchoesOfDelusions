using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingsController : MonoBehaviour
{
    [SerializeField]
    private Image succumbedEndingStar_Left,
        succumbedEndingStar_Right,
        reconciliationEndingStar,
        statusQuoEndingStar_Left,
        statusQuoEndingStar_Right,
        solitudeEndingStar_Left,
        solitudeEndingStar_Right;

    public static bool succumbedEndingStar_LeftChecker = false,
        succumbedEndingStar_RightChecker = false,
        reconciliationEndingStar_Checker = false,
        statusQuoEndingStar_LeftChecker = false,
        statusQuoEndingStar_RightChecker = false,
        solitudeEndingStar_LeftChecker = false,
        solitudeEndingStar_RightChecker = false;

    private void Start()
    {
        SetEndingStars();
    }

    private void SetEndingStars()
    {
        //Succumbed Ending
        if (succumbedEndingStar_LeftChecker)
            succumbedEndingStar_Left.color = new Color32(148, 255, 123, 255);
        else
            succumbedEndingStar_Left.color = new Color32(0, 0, 0, 255);

        if (succumbedEndingStar_RightChecker)
            succumbedEndingStar_Right.color = new Color32(148, 255, 123, 255);
        else
            succumbedEndingStar_Right.color = new Color32(0, 0, 0, 255);

        //Reconciliation_Ending
        if (reconciliationEndingStar_Checker)
            reconciliationEndingStar.color = new Color32(255, 174, 242, 255);
        else
            reconciliationEndingStar.color = new Color32(0, 0, 0, 255);

        //Status Quo Ending
        if (statusQuoEndingStar_LeftChecker)
            statusQuoEndingStar_Left.color = new Color32(175, 0, 255, 255);
        else
            statusQuoEndingStar_Left.color = new Color32(0, 0, 0, 255);

        if (statusQuoEndingStar_RightChecker)
            statusQuoEndingStar_Right.color = new Color32(175, 0, 255, 255);
        else
            statusQuoEndingStar_Right.color = new Color32(0, 0, 0, 255);

        //Solitude Ending
        if (solitudeEndingStar_LeftChecker)
            solitudeEndingStar_Left.color = new Color32(117, 193, 255, 255);
        else
            solitudeEndingStar_Left.color = new Color32(0, 0, 0, 255);

        if (solitudeEndingStar_RightChecker)
            solitudeEndingStar_Right.color = new Color32(117, 193, 255, 255);
        else
            solitudeEndingStar_Right.color = new Color32(0, 0, 0, 255);
    }

    public void SuccumbedEndingStar_Left()
    {
        succumbedEndingStar_LeftChecker = true;
        GameDataController.SaveEndings("Succumbed Ending (1of2)", "Too");
    }

    public void SuccumbedEndingStar_Right()
    {
        succumbedEndingStar_RightChecker = true;
        GameDataController.SaveEndings("Succumbed Ending (2of2)", "Stare");
    }

    public void ReconciliationEndingStar()
    {
        reconciliationEndingStar_Checker = true;
        GameDataController.SaveEndings("Reconciliation Ending", "Please!");
    }

    public void StatusQuoEndingStar_Left()
    {
        statusQuoEndingStar_LeftChecker = true;
        GameDataController.SaveEndings("Status Quo Ending (1of2)", "Do");
    }

    public void StatusQuoEndingStar_Right()
    {
        statusQuoEndingStar_RightChecker = true;
        GameDataController.SaveEndings("Status Quo Ending (2of2)", "Long!");
    }

    public void SolitudeEndingStar_Left()
    {
        solitudeEndingStar_LeftChecker = true;
        GameDataController.SaveEndings("Solitude Ending (1of2)", "Not");
    }

    public void SolitudeEndingStar_Right()
    {
        solitudeEndingStar_RightChecker = true;
        GameDataController.SaveEndings("Solitude Ending (2of2)", "For");
    }
}
