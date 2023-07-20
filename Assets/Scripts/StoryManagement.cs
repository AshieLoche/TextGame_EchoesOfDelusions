using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class StoryManagement : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI story;
    [SerializeField]
    private GameObject buttonChoices;
    [SerializeField]
    private Button continueButton;
    private string storyLine, storyKey;
    public static double storyIndex = 0D;
    public static bool continueChecker = false;

    private IDictionary<string, string> storyLines = new Dictionary<string, string>()
    {
        { "S0", "Echoes\r\nof\r\nDelusion" },
        { "S1", "As the clock strikes 12 in the afternoon, the\r\nalarm starts to blare out waking you up from your\r\nslumber. You grumpily hid under your blanket,\r\nbut gave up as the alarm keeps on wailing\r\nintensely. You turned the alarm off, sitting up\r\nwith a sigh as your disheviled hair cover your\r\nface." },
        { "S2.1", "You notice your hair covering your eyes with\r\ntangles and locks. You got up and gradually\r\nshuffled over to the bathroom.\r\n\nYou stood right infront of the sink and lean over\r\nto wash your hair before lifting your head back\r\nup flipping your hair to the back of your head." },
        { "S2.2", "Suddenly you caught a glance of your reflection\r\nstaring at you. You stared right back, but as you\r\ndo you suddenly get overwhelmed with a\r\npiercing memory causing you to flinch away.\r\n\nYou turned on the shower trying to forget the\r\nunwelcomed thought. However, you just end up\r\nwith your hands against the wall watching\r\ndroplets of water trickle down to the floor. As\r\nyour eyes starts to well up." },
        { "S2.3", "You wanted to just bawl your eyes out but there\r\nwas nothing. Just silent whimpers escaping your\r\nclenched jaw.\r\nAs you stood there with your eyes shut, a sense\r\nof clarity rushes through your head. You wonder\r\nto yourself if this is the \"shower thoughts\"\r\npeople have been talking about."}
    };

    private void Start()
    {
        GetStoryLine();
        GetButtonChoices();
    }

    private void GetStoryLine()
    {
        storyKey = "S" + storyIndex;
        storyLine = storyLines[storyKey];
        story.text = storyLine;

        if (storyKey == "S0")
            story.fontSize = 145;
        else if (storyKey == "S2.2")
            story.fontSize = 60;
        else
            story.fontSize = 75;

    }

    public void NextStoryLine()
    {
        if (storyLines.ContainsKey("S" + (storyIndex + 0.2D)))
            continueChecker = true;
        else
            continueChecker = false;

        storyIndex += 0.1D;
        GetStoryLine();
        GetButtonChoices();
    }

    public void SetStoryLine(string index)
    {
        storyIndex = Convert.ToDouble(index);
    }

    private void GetButtonChoices()
    {
        if (continueChecker)
        {
            buttonChoices.SetActive(false);
            continueButton.gameObject.SetActive(true);
        }
        else
        {
            buttonChoices.SetActive(true);
            continueButton.gameObject.SetActive(false);
        }
    }

    public void SetButtonChoices(string check)
    {
        continueChecker = Convert.ToBoolean(check);
    }
}