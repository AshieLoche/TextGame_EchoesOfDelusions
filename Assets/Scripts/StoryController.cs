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
    private static string previousScene;
    public static Scene scene;
    public static double storyIndex = 0D;
    public static bool continueChecker = false, gameDataRetrieved = false;

    private IDictionary<string, string> storyLines = new Dictionary<string, string>()
    {
        //Title and Loeader - Scene
        { "S0", "Echoes\r\nof\r\nDelusion" },
        //Wake Up - Scene
        { "S1.A", "As the clock strikes 12 in the afternoon, your\r\nalarm starts to blare out waking you up from your\r\nslumber. You grumpily hid under your blanket,\r\nbut gave up as your alarm kept on wailing\r\nintensely. You turned the alarm off, sitting up\r\nwith a sigh as your disheviled hair cover your\r\nface." },
        //Shower - Scene
        { "S2.1", "You notice your hair covering your eyes with\r\ntangles and locks. You got up and gradually\r\nshuffled over to the bathroom.\r\n\r\nYou stood right infront of the sink and lean over\r\nto wash your hair before lifting your head up \r\nflipping your hair to the back of your head." },
        { "S2.2", "Suddenly you caught a glance of your reflection staring \r\nright at you. You stared right back, but as you do you \r\nsuddenly get overwhelmed with apiercing memory causing \r\nyou to flinch away.\r\n\r\nYou turned on the shower trying to forget the unwelcomed \r\nthought. However, you just end up with your hands against \r\nthe wall watching droplets of water trickling down your \r\nbody onto to floor, as your eyes starts to well up." },
        { "S2.3", "You just wanted to bawl your eyes out but nothing came\r\nout. Just silent whimpers escaping your clenched jaw.\r\n\r\nAs you stood there with your eyes shut, a memory of your\r\nfriends laughing and smile with you came into your mind\r\nsending a sense of relief to your body. You smile softly\r\nand scoffs tenderily as tears rush down your cheeks,\r\nwhile reminscing at the fond memory wondering why you\r\nfeel so alone despite it." },
        //Explore House - Scene
        { "S3", "You stood up and instantly felt the weight of\r\nyour body pulling you back down. You feel rather\r\nfeeble and lethargic as you fix up your hair and\r\nmeander through your room and out to the hallway.\r\nAs you reached the end, you find yourself between\r\nthe path towards the kitchen and the sliding doors\r\nleading to the backyard." },
        //Exercise - Scene
        { "S4.1", "You gave out a somber sigh as you look over to your\r\ncloset. You saw your exercise equipment propped up \r\nagainst it as you reminisce the good old day.\r\n\r\nYou linger for a bit before getting up and walking over to the\r\nsame equipment you used to use before finding yourself in this\r\nspiralling mess. You stare at it for a moment dreading the\r\nreality that the joy of working out might have also been sucked\r\naway by this god awful rut. However, you took a deep breathe,\r\ngrabbing the equipment and begin exercising." },
        { "S4.2", "At first, you feel quite sore and frail. But you push\r\nthrough, gradually filling yourself with vigor and energy. \r\nYou find yourself smiling, something you haven't done for a\r\nlong while now. The soreness now fills you with ecstacy\r\ninstead of pain.\r\n\r\nAfterwards, you took a bath and got ready to do more things,\r\nexcited for what's to come. You look over to your computer desk,\r\nthen to the window outside pondering \"What should you choose?\"." },
        //Check Phone - Scene
        { "S5", "Your spirit was slightly lifted up as you gain a hint of\r\nvigor. You rushed through your shower and clothed yourself\r\nbefore grabbing your phone frantically opening messenger.\r\nYour eyes swiftly glanced over to the chat head of your\r\nfriend group chat. It was silent, not a mere message.\r\nYou paused and lingered, pondering if you are doing the\r\nright thing. However, you quickly snapped out of it as\r\nyou ask to yourself, \"What could possibly go wrong?\"." },
        //Finish Bathing - Scene
        { "S6", "However, those unwelcomed thoughts came\r\nrushing back in. You resumed your bathing taking\r\nyour time as you pause every so often just to wallow.\r\nYou went out of the bathroom and clothed yourself,\r\nbefore walking out to the hallway. As you reached the\r\nend, you find yourself between the path towards the\r\nkitchen and the sliding doors leading to the backyard." },
        //Check Kitchen - Scene
        { "S7.1", "You looked down and placed a hand on \r\nyour stomach as it growls. You shrugged and\r\nwalked over to the kitchen where you found your\r\nmother cooking something.\r\n\r\nYour mom noticed and greeted you with a smile. You\r\nsmiled back as your stomach growls again cutting \r\nyou off before being able to say a single word." },
        { "S7.2", "Your mom looks back to the pan and \r\ngiggles as she say, \"You're in luck! I just \r\nfinished cooking us lunch.\".\r\n\r\nShe hands you a hearty meal of Gravy Steak. You\r\nsmiled softly as it was your favourite food. You\r\nlook over to your mom and thanked her for the\r\nfood as you grabbed a fork and dug in." },
        { "S7.3", "Your first bite was such a delight, but it was short\r\nlived as a rush of negative thoughts invaded your\r\nmind. Your mom noticed the shift in your mood\r\nas she asked, \"What's wrong, dear?\"." },
        //Check Backyard - Scene
        { "S8.1", "You walked out to the backyard, finding your\r\ndad mowing the lawn. Your eyes widen as you\r\nwere afraid of being tasked to the rest of the mowing.\r\nYou tried to sneak inside the house but you heard a\r\nvoice calling you over.\r\n\r\nYou sighed and turned around to see your dad waving\r\nover to you. You wave back to him and smile. He\r\ngestured to come over as you worringly follow suit." },
        { "S8.2", "He greeted you and gave you a massive\r\nbear hug and smiles asking, \"How is my\r\nsport doing?\".\r\n\r\nYou were relieved but your smile quickly fade as\r\nyour dad notice. He frowns and lifts your chin up\r\nmaking you look at him in the eye as he ask,\r\n\"What's the matter?\"." },
        { "S8.3", "You looked away trying to avoid the question,\r\nbut your father's worried look won you over.\r\nYou were about to speak, instead you yelped\r\nas he quickly grabs you and sat you on his lap.\r\n\r\nYou giggle softly helping to ease up your mood.\r\nYou explained to him the pain of feeling left out by\r\nyour friends." },
        { "S8.4", "After everything, he hugs you from behind and said,\r\n\"You should go exercise! You shouldn't drown yourself\r\nwith all this negativity! And if I remember correctly\r\nyou really enjoy working out!\".\r\n\r\nYou told your dad that you'll think about it. He lets you go and\r\ncontinues mowing. You walk over to your room and felt something\r\nheavy pressing against your chest. As dread fills your mind,\r\nsince it has been a long while since you exercised properly." },
        //Play Video Games - Scene
        { "S9.1", "You hopped onto your computer and saw your friends\r\nwas playing as well. You decided to ask them if you can join in,\r\nsomething you are usually to shy to do so, and they agreed.\r\n\r\nHowever, as you guys played you noticed that they were mainly\r\ntalking to each other. You feel a twinge of pain as you felt a\r\nlittle left out, slightly triggering your abandonment issues." },
        { "S9.2", "However, you kept on playing with them but as you go\r\non you felt more and more left out as they chatted and\r\ncelebrated together and everytime you try to butt in it\r\nlands on deaf ears.\r\n\r\nDuring your latest game, you performed very well and was so\r\nexcited. You tried to celebrate it with your friends but you\r\nwere silenced by one of the friend as they resume chattering\r\nwith one another." },
        { "S9.3", "You sat there in shock, as you felt your\r\nhands shaking. You grit your teeth trying to\r\nignore the influx of negative thoughts. Your eyes\r\nshakes as it starts to water. You felt your heart sink\r\nas your breathing becomes heavier and faster. You\r\nsay to yourself, \"Th-They do care about me! Th-They\r\nare didn't mean to fucking shut me up... O-Or d-did\r\nthey???... M-Maybe they don't really care...\"" },
        { "S9.4", "You paused and left the game. You stood up\r\nstaring blankly at the floor. The paranoia of just\r\nbeing included for pity's sake, the paranoia of your\r\nfriends not really being your friends, the paranoia of\r\nyour friends just being pretend was swallowing your\r\nmind whole." },
        { "S9.5", "You try to convince yourself by saying, \"T-That's\r\npreposterous...\". As you wrapped your arms around yourself,\r\nsqueezing yourself. You grabbed your head trying to fight these\r\nthoughts, knowing that these aren't truem causing your breathing\r\nto get even faster. You are frantically panicking, as memories\r\nflash about in your head. The negative ones were like bullets\r\nthrough your flesh, the good ones being tainted with these\r\nmalicious thoughts destroying any hope left." },
        { "S9.6", "You looked at the window in your room. As you\r\nsay your final words, \"N-Nobody would really miss\r\nme, right?\". You started charging at the window and\r\ncrash through it smashing through the glass and\r\nlanding head first onto the ground." },
        //Go Out For A Jog - Scene
        { "S10.1", "You wore your jogging clothes and took off. You jogged\r\nthroughout the neighbourhood getting a good sweat on.\r\n\r\nAfter some time, you took a break near the park entrance,\r\ndrinking from your canteen as you suddenly heard a familiar\r\nvoice. You look around to see your friends together within the\r\npark. They were all present except you." },
        { "S10.2", "Your mood drop as you question youself \"Wait... I\r\nthought they cancelled the meet up?\". You begin to panic\r\nas your eyes start to quiver with an influx of mixed\r\nemotions. You tried regaining your composure and carry on\r\nwith your jog despite it.\r\n\r\nOne of the friend got a quick glance of someone jogging pass\r\nthe park entrance but didn't saw who it was. So he just shrugged\r\nit off and resumed chitchatting with the rest." },
        { "S10.3", "After your jog, you took a break in a cafe with the\r\nthought of your friends lying and intentionally leaving you\r\nout still searing through your brain, you took your phone\r\nout and check if they even attempted to invite you again\r\nbut there only the lsat message of cancelling the meet up.\r\n\r\nSuddenly, the cafe doors swung open and your so-called friends\r\nwalk in laughing. You scowl and glared at them lividly. Your\r\nfriends notice you and stopped seeing your glare and froze up." },
        { "S10.4", "They panicked as they got caught while one of them\r\nsays, \"H-Hey there Elosh! Fancy seeing you here!\".\r\n\r\nHowever, you stayed silent and kept glaring at them. The same\r\nfriend frantically explains, \"O-OH!!! I-I can explain! Pl-Plans\r\nchanged again! However, w-we didn't know if you were busy or\r\nnot... S-So we decided not to invite you...\"." },
        { "S10.5", "Your eyes widen from that blatant lie, you looked\r\nat your phone reading your message about being\r\nfree all week. Your blood starts to boil from the\r\naudacity of these bitches." },
        //Delay - Scene
        { "S11.1", "You stared at your phone unsure what to\r\nsay or how to say it. You left the group chat trying\r\nto clear your head before finding your friends' My\r\nDay. Your curiosity got the better of you and clicked\r\non it. You saw several pictures of them in the park\r\nand cafe having fun together." },
        { "S11.2", "Your eyes widen as you swipe through all of\r\nit. With every new picture, you felt your heart\r\nsinking deeper and deeper. You quickly opened the\r\ngroup chat once again to see the latest message\r\nsaying \"Sorry guys, we have to cancel today's meet\r\nup. Something came up.\". You leaned closer to check\r\nif what you are reading is true." },
        { "S11.3", "You lowered your phone as you feel your\r\nhands and eyes shake with anger and sorrow.\r\nYou scowl \"THOSE FUCKERS!\". You threw your\r\nphoneto the side as you grabbed your head\r\nshaking itvigorously. As thoughts rush into\r\nyour head saying, \"THEY LIED!!!\", \"THEY NEVER\r\nLIKED YOU!!!\", \"THEY TRICKED YOU!!!\", \"THEY\r\nABANDONED YOU!!!\"." },
        { "S11.4", "Negative thoughts engulfed your head as\r\nyou cried your eyes red. You sniffle and whimper\r\nstruggling to utter a single word. You locked yourself\r\ninside your room for a couple of days, never leaving\r\nuntil you pass out from the lack of food and water." },
        //Commit - Scene
        { "S12.1", "You open the group chat and began with a hello.\r\nThey greeted you back, then you feel your hands shake\r\nleading you to take a deep breathe.\r\n\r\nAs you gave out a heavy exhale you proceed to confess about\r\nhow left out you felt around them. Despite hanging out with them\r\nyou still feel alone as you often contemplate if they really see\r\nyou as a friend or they are friends with you out of pity."},
        { "S12.2", "They all suddenly went quiet, making you fear the\r\nworse. You stare at your phone as you slowly shrink\r\ninto yourself, before hearing a knock from your front door.\r\n\r\nYou got up and walked over to it. As you opened it, you were\r\ngreeted with massive warm and loving hugs from your friends.\r\nYou yelped from the surprise and looked at them one by one as\r\nthey keep on hugging you." },
        { "S12.3", "As you feel the warmth of their embrance, you begin\r\nto tear up feeling their love without a word being spoken.\r\nIt was the thing you always needed.\r\n\r\nThey asked for forgiveness that you felt that way and that they\r\ndidn't know. They also acknowledge that they haven't been the\r\nbest of friends to you either, promising they will make it up to\r\nyou as you promise to work on yourself with their help." },
        { "S12.4", "You guys hugged once again even tighter than last time.\r\nAs you just slowly melt into their arms, crying tenderly. While,\r\nthey comfort you and not leaving your side until you felt better.\r\n\r\nAs time go by, your friends ensure your are doing well as you\r\ngradually work on yourself trying to overcome your paranoia and\r\nabandonment issues. While they help you every time they can." },
        //Confess - Scene
        { "S13.1", "You sighed as you put your platter aside. You\r\ntook a deep breathe and confessed to her that you\r\nfelt a tad bit left out with your friends. Even though\r\nyou were around them you still felt alone and unseen.\r\nYou confess that you are getting a bit paranoid that\r\nmaybe they are just faking to be your friend and that\r\ndeep down they despise you or pity you." },
        { "S13.2", "Your Mom looked at you with a broken heart and\r\ntears in her eyes. She knelt down and pulled you in\r\nfor a warm and comforting hug as she said, \"Oh\r\nbaby... thank you for telling me this. I know how hard\r\nit is to tell the truth and I am so proud of you.\"." },
        { "S13.3", "She pushes you back gently and stared into\r\nyour eyes. \"Honey, if you have problems with your\r\nfriends then you need to fix it with your friends.\".\r\n\r\nYou stare at her for a few seconds as everything\r\nslowly sinks in. You softly nodded causing your mom\r\nto smile and hug you once again." },
        { "S13.4", "You were still in shock due to what just happened, but\r\nended up hugging your mom back smiling and tearing up as\r\nwell. Your mom stood up and wipes her tear before saying,\r\n\"I love you so much, and I hope your friends loves you too.\"\r\n\r\nYou smile at her brightly as you took your platter and went back\r\nto your room. Hearing your mom's word still running through your\r\nhead. You placed your platter aside and grabbed your phone\r\nopening your group chat." },
        { "S13.5", "Suddenly, you feel a bit unsettled as you worry\r\nabout what your friends' reactions might be. As you\r\nponder if you should go through with it or not." },
        //Lie - Scene
        { "S14.1", "You told her you were fine and asked if you\r\ncould bring the food back to your room. There\r\nwas an desperate look on your mom's face, but she\r\nreluctantly agrees.\r\n\r\nYou walked to your room gripping your chest as you\r\nwanted to tell her the truth but it was just too painful\r\nto do so." },
        { "S14.2", "You placed the platter on your nightstand and\r\njumped onto your bed. At first panting as your mind\r\nis slowly being flooded by all these negative\r\nthoughts, until you end up crying onto your pillow.\r\nYou ended up falling asleep for the rest of the day." }
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
            else if (scene.name == "WakeUp_Scene")
                storyIndex = 1D;
            else if (scene.name == "Shower_Scene")
                storyIndex = 2.1D;
            else if (scene.name == "ExploreHouse_Scene")
                storyIndex = 3D;
            else if (scene.name == "Exercise_Scene")
                storyIndex = 4.1D;
            else if (scene.name == "CheckPhone_Scene")
                storyIndex = 5D;
            else if (scene.name == "FinishHouse_Scene")
                storyIndex = 6D;
            else if (scene.name == "CheckKitchen_Scene")
                storyIndex = 7.1D;
            else if (scene.name == "CheckBackyard_Scene")
                storyIndex = 8.1D;
            else if (scene.name == "PlayVideoGames_Scene")
                storyIndex = 9.1D;
            else if (scene.name == "GoOutForAJog_Scene")
                storyIndex = 10.1D;
            else if (scene.name == "Delay_Scene")
                storyIndex = 11.1D;
            else if (scene.name == "Commit_Scene")
                storyIndex = 12.1D;
            else if (scene.name == "Confess_Scene")
                storyIndex = 13.1D;
            else if (scene.name == "Lie_Scene")
                storyIndex = 14.1D;
        }

        GetStoryLine();
        GetButtonChoices();
    }

    public void SetPreviousScene()
    {
        previousScene = scene.name;
    }

    private void GetStoryLine()
    {
        if (previousScene == "Loader_Scene")
            storyKey = $"S{storyIndex}.A";
        else if (previousScene == "Lie_Scene" || previousScene == "Ignore_Scene")
            storyKey = $"S{storyIndex}.B";
        else
            storyKey = $"S{storyIndex}";

        storyLine = storyLines[storyKey];
        story.text = storyLine;

        if (storyKey == "S0")
            story.fontSize = 145;
        else if (storyKey == "S2.2" || storyKey == "S2.3" || storyKey == "S4.1" ||
            storyKey == "S4.2" || storyKey == "S5" || storyKey == "S8.1" ||
            storyKey == "S8.4" || storyKey == "S9.1" || storyKey == "S9.2" ||
            storyKey == "S9.5" || storyKey == "S10.1" || storyKey == "S10.2" ||
            storyKey == "S10.3" || storyKey == "S10.4" || storyKey == "S12.1" ||
            storyKey == "S12.2" || storyKey == "S12.3" || storyKey == "S12.4" ||
            storyKey == "S13.4")
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
        if ((Math.Truncate(storyIndex) < storyIndex && storyLines.ContainsKey("S" + (storyIndex + 0.1D))) || storyKey == $"S{storyIndex}.B")
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