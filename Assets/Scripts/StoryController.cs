using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class StoryController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI story;
    [SerializeField]
    private GameObject buttonChoices;
    [SerializeField]
    private Button continueButton;
    private string storyLine, storyKey;
    public static Scene scene;
    public static double storyIndex = 0D;
    public static bool continueChecker = false, gameDataRetrieved = false;

    private IDictionary<string, string> storyLines = new Dictionary<string, string>()
    {
        { "S0", "Echoes\r\nof\r\nDelusion" },
        { "S1", "As the clock strikes 12 in the afternoon, your\r\nalarm starts to blare out waking you up from your\r\nslumber. You grumpily hid under your blanket,\r\nbut gave up as your alarm kept on wailing\r\nintensely. You turned the alarm off, sitting up\r\nwith a sigh as your disheviled hair cover your\r\nface." },
        { "S2.1", "You notice your hair covering your eyes with\r\ntangles and locks. You got up and gradually\r\nshuffled over to the bathroom.\r\n\r\nYou stood right infront of the sink and lean over\r\nto wash your hair before lifting your head up \r\nflipping your hair to the back of your head." },
        { "S2.2", "Suddenly you caught a glance of your reflection staring \r\nright at you. You stared right back, but as you do you \r\nsuddenly get overwhelmed with apiercing memory causing \r\nyou to flinch away.\r\n\r\nYou turned on the shower trying to forget the unwelcomed \r\nthought. However, you just end up with your hands against \r\nthe wall watching droplets of water trickling down your \r\nbody onto to floor, as your eyes starts to well up." },
        { "S2.3", "You just wanted to bawl your eyes out but nothing came\r\nout. Just silent whimpers escaping your clenched jaw.\r\n\r\nAs you stood there with your eyes shut, a memory of your\r\nfriends laughing and smile with you came into your mind\r\nsending a sense of relief to your body. You smile softly\r\nand scoffs tenderily as tears rush down your cheeks,\r\nwhile reminscing at the fond memory wondering why you\r\nfeel so alone despite it." },
        { "S3", "You stood up and instantly felt the weight of\r\nyour body pulling you back down. You feel rather\r\nfeeble and lethargic as you fix up your hair and\r\nmeander through your room and out to the hallway.\r\nAs you reached the end, you find yourself between\r\nthe path towards the kitchen and the sliding doors\r\nleading to the backyard." },
        { "S4.1", "You gave out a somber sigh as you look over to your\r\ncloset. You saw your exercise equipment propped up \r\nagainst it as you reminisce the good old day.\r\n\r\nYou linger for a bit before getting up and walking over to the\r\nsame equipment you used before finding yourself in this\r\nspiralling mess. You stare at it for a moment dreading the\r\nreality that the joy of working out might have also been sucked\r\naway by this god awful rut. However, you took a deep breathe,\r\ngrabbing the equipment and begin exercising." },
        { "S4.2", "At first, you feel quite sore and frail. But you push\r\nthrough, gradually filling yourself with vigor and energy. \r\nYou find yourself smiling, something you haven't done for a\r\nlong while now. The soreness now fills you with ecstacy\r\ninstead of pain.\r\n\r\nAfterwards you took a bath and got ready to do more things,\r\nexcited for what's to come. You look over to your computer desk,\r\nthen to the window outside pondering \"What should you choose?\"." },
        { "S5", "Your spirit was slightly lifted up as you gain a hint of\r\nvigor. You rushed through your shower and clothed yourself\r\nbefore grabbing your phone frantically opening messenger.\r\nYour eyes swiftly glanced over to the chat head of your\r\nfriend group chat. It was silent, not a mere message.\r\nYou paused and lingered, pondering if you are doing the\r\nright thing. However, you quickly snapped out of it as\r\nyou ask to yourself, \"What could possibly go wrong?\"." },
        { "S6", "However, those unwelcomed thoughts came\r\nrushing back in. You resumed your bathing taking\r\nyour time as you pause every so often just to wallow.\r\nYou went out of the bathroom and clothed yourself,\r\nbefore walking out to the hallway. As you reached the\r\nend, you findyourself between the path towards the\r\nkitchen and the sliding doors leading to the backyard." },
        { "S7.1", "You looked down and placed a hand on \r\nyour stomach as it growls. You shrugged and\r\nwalked over to the kitchen where you found your\r\nmother cooking something.\r\n\r\nYour mom noticed and greeted you with a smile. You\r\nsmiled back as your stomach growls again cutting \r\nyou off before being able to say a single word." },
        { "S7.2", "Your mom looks back to the pan and \r\ngiggles as she say, \"You're in luck! I just \r\nfinished cooking us lunch.\".\r\n\r\nShe hands you a hearty meal of Gravy Steak. You\r\nsmiled softly as it was your favourite food. You\r\nlook over to your mom and thanked her for the\r\nfood as you grabbed a fork and dug in." },
        { "S7.3", "Your first bite was such a delight, but it was short\r\nlived as a rush of negative thoughts invaded your\r\nmind. Your mom noticed the shift in your mood\r\nas she asked, \"What's wrong, dear?\"." }
    };


    private void Start()
    {
        if (gameDataRetrieved)
            gameDataRetrieved = false;
        else
        {
            scene = SceneManager.GetActiveScene();

            if (scene.name == "Title_Scene" || scene.name == "Loader_Scene")
                storyIndex = 0D;
            else if (scene.name == "Play_Scene")
                storyIndex = 1D;
            else if (scene.name == "Shower_Scene")
                storyIndex = 2.1D;
            else if (scene.name == "StandUp_Scene")
                storyIndex = 3D;
            else if (scene.name == "Exercise_Scene")
                storyIndex = 4.1D;
            else if (scene.name == "CheckPhone_Scene")
                storyIndex = 5D;
            else if (scene.name == "ExploreHouse_Scene")
                storyIndex = 6D;
            else if (scene.name == "CheckKitchen_Scene")
                storyIndex = 7.1D;
            else if (scene.name == "CheckBackyard_Scene")
                storyIndex = 8.1D;
        }
        
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
        else if (storyKey == "S2.2" || storyKey == "S2.3" || storyKey == "S4.1" || storyKey == "S4.2" || storyKey == "S5")
            story.fontSize = 60;
        else
            story.fontSize = 75;
    }

    public void NextStoryLine()
    {
        storyIndex += 0.1D;
        GetStoryLine();
        GetButtonChoices();
    }

    private void GetButtonChoices()
    {
        if (Math.Truncate(storyIndex) < storyIndex && storyLines.ContainsKey("S" + (storyIndex + 0.1D)))
            continueChecker = true;
        else
            continueChecker = false;

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
}