using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StoryManagement : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI story;
    private string[] storyLine;
    private string storyLine_1 = "As the clock strikes 12 in the afternoon, the\r\nalarm starts to blare out waking you up from your\r\nslumber. You grumpily hid under your blanket,\r\nbut gave up as the alarm keeps on wailing\r\nintensely. You turned the alarm off, sitting up\r\nwith a sigh as your disheviled hair cover your\r\nface.";
    private string[] storyLine_2 = { "You notice your hair covering your eyes with\r\ntangles and locks. You got up and gradually\r\nshuffled over to the bathroom.\r\nYou stood right infront of the sink and lean over\r\nto wash your hair before lifting your head back\r\nup flipping your hair to the back of your head.",
        "Suddenly you caught a glance of your reflection\r\nstaring at you. You stared right back, but as you\r\ndo you suddenly get overwhelmed with a\r\npiercing memory causing you to flinch away.\r\nYou turned on the shower trying to forget the\r\nunwelcomed thought. However, you just end up\r\nwith your hands against the wall watching\r\ndroplets of water trickle down to the floor. As\r\nyour eyes starts to well up.",
        "You wanted to just bawl your eyes out but there\r\nwas nothing. Just silent whimpers escaping your\r\nclenched jaw.\r\nAs you stood there with your eyes shut, a sense\r\nof clarity rushes through your head. You wonder\r\nto yourself if this is the \"shower thoughts\"\r\npeople have been talking about."
    };

    void Start()
    {
        SetStoryLine(storyLine_1);
        story.text = storyLine[0];
    }

    public void SetStoryLine(object indicator)
    {
        storyLine = new[] { indicator.ToString() };  
    }
}