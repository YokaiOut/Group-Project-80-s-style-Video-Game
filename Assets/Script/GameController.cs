using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Collections;

public class GameController : MonoBehaviour
{
    EventSystem @event { get { return GameObject.Find("EventSystem").GetComponent<EventSystem>(); } }

    public DiffEnum Difficulty;
    public ContEnum Control;
    public MenuType Status;
    public Location Location;
    public Player player;
    public GameObject[] menuCanvases;
    public GameObject[] gameCanvases;
    List<KeyCode> Keys;
    KeyCode[] Konami = { KeyCode.UpArrow, KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.LeftArrow, KeyCode.RightArrow };
    string[] introspeech =
    {
        "Welcome to Games Computing the Game!",
        "You are about to be put an extremely shortened version of a year in the life of a student studying this course.",
        "With a little less drinking, partying or whatever it is students do nowadays.",
        "Try to keep what you’re taught in mind because yes we do have a nice quiz in store for you at the end.",
        "Good Luck. And have fun!"
    };

    public string[] GroupText =
    {
        "You see a bunch of females talking about their disserations, you believe they are third years.",
        "They're talking about modules similar to mine, maybe they're on my course... or Computer Science?",
        "Looks to me this group is holding a lot of paper work! Good thing Games Computing is all electronical!",
        "It looks like we have to share our building with the engineering students, hope there is enough space for us all!"
    };

    Vector3 leftpos = new Vector3(150f, 110, 0);

    Vector3 rightpos = new Vector3(1250f, 110, 0);

    bool finishedspeech = false;
    
    GameObject hori1Val;
    GameObject hori2Val;

    public int SFXVolume;
    public int MUSVolume;

    public bool atLecture1MIN = false;
    public bool atLecture2INB = false;
    public bool atWorkshop1door = false;
    public bool atWorkshop2door = false;

    public bool atLecture1LIB = false;
    public bool atLecture2LIB = false;
    public bool atWorkshop1LIB = false;
    public bool atWorkshop2LIB = false;

    GameObject currentCanvas;
    GameObject currentGameCanvas;

    public Lecture lecture1;

    public Lecture lecture2;

    public WorkshopData workshop1;

    public WorkshopData workshop2;
    
    public void ChangeMenu(int inval)
    {
        Status = (MenuType)inval;

        if (currentCanvas!=null)
            currentCanvas.GetComponent<Canvas>().enabled = false;

        switch (Status)
        {
            case MenuType.Main:
                currentCanvas = menuCanvases[0];
                GameObject.Find("BackgroundAnimation").GetComponent<Canvas>().enabled = true;
                @event.SetSelectedGameObject(GameObject.Find("MainHeader"));
                @event.sendNavigationEvents = true;
                break;

            case MenuType.Settings:
                currentCanvas = menuCanvases[1];

                hori1Val = GameObject.Find("ControlsValue");
                hori2Val = GameObject.Find("DifficultyValue");

                hori1Val.GetComponent<Text>().text = Control.ToString();
                hori2Val.GetComponent<Text>().text = Difficulty.ToString();

                hori1Val.GetComponent<Button>().onClick.AddListener(() => ChangeValue(hori1Val));
                hori2Val.GetComponent<Button>().onClick.AddListener(() => ChangeValue(hori2Val));

                @event.SetSelectedGameObject(GameObject.Find("SettingsHeader"));
                break;

            case MenuType.SettingsVolume:
                currentCanvas = menuCanvases[2];

                hori1Val = GameObject.Find("SFXValue");
                hori2Val = GameObject.Find("MUSValue");

                hori1Val.GetComponent<Text>().text = SFXVolume.ToString() + "%";
                hori2Val.GetComponent<Text>().text = MUSVolume.ToString() + "%";

                @event.SetSelectedGameObject(GameObject.Find("VolumeHeader"));
                break;

            case MenuType.Game:
                currentCanvas = gameCanvases[0];
                GameObject.Find("BackgroundAnimation").GetComponent<Canvas>().enabled = false;
                GameObject.Find("Clouds").GetComponent<Canvas>().enabled = true;
                @event.sendNavigationEvents = false;
                MoveToLocation(Location.Start);
                break;

            case MenuType.Intro:
                GameObject.Find("BackgroundAnimation").GetComponent<Canvas>().enabled = false;
                currentCanvas.GetComponent<Canvas>().enabled = false;
                currentCanvas = GameObject.Find("IntroCanvas");
                GameObject.Find("IntroCanvas").GetComponent<Canvas>().enabled = true;
                StartCoroutine(Typewrite(GameObject.Find("IntroText").GetComponent<Text>(), introspeech, 5.0f));

                break;
        }

        currentCanvas.GetComponent<Canvas>().enabled = true;
    }

    public void MoveToLocation(Location inLoc)
    {
        Location = inLoc;

        atLecture1MIN = atLecture2INB = atWorkshop1door = atWorkshop2door = false;
        atLecture1LIB = atLecture2LIB = atWorkshop1LIB = atWorkshop2LIB = false;

        GameObject.Find("Player").GetComponent<Image>().enabled = true;

        switch (Location)
        {
            case Location.Start:
                if (!player.facingleft)
                    player.transform.position = leftpos;
                else
                    player.transform.position = rightpos;

                if (currentGameCanvas != null)
                {
                    currentGameCanvas.GetComponent<Canvas>().enabled = false;
                }
                currentGameCanvas = gameCanvases.FirstOrDefault(x=>x.name == "Intro");
                currentGameCanvas.GetComponent<Canvas>().enabled = true;
                GameObject.Find("Outside Floor").GetComponent<Image>().enabled = true;
                break;

            case Location.MinervaOutside:
                if (!player.facingleft)
                    player.transform.position = leftpos;
                else
                    player.transform.position = rightpos;

                currentGameCanvas.GetComponent<Canvas>().enabled = false;
                currentGameCanvas = gameCanvases.FirstOrDefault(x=>x.name=="MinervaOutside");
                currentGameCanvas.GetComponent<Canvas>().enabled = true;
                GameObject.Find("Outside Floor").GetComponent<Image>().enabled = true;
                break;

            case Location.MinervaLecture:
                currentGameCanvas.GetComponent<Canvas>().enabled = false;
                currentGameCanvas = gameCanvases.FirstOrDefault(x => x.name == "MINLecture");
                currentGameCanvas.GetComponent<Canvas>().enabled = true;
                GameObject.Find("Outside Floor").GetComponent<Image>().enabled = false;
                atLecture1MIN = true;
                break;

            case Location.INBOutside:
                player.transform.position = leftpos;

                currentGameCanvas.GetComponent<Canvas>().enabled = false;
                currentGameCanvas = gameCanvases.FirstOrDefault(x => x.name == "INBOutside");
                currentGameCanvas.GetComponent<Canvas>().enabled = true;
                GameObject.Find("Outside Floor").GetComponent<Image>().enabled = true;
                break;

            case Location.INBInside:
                player.transform.position = leftpos;

                currentGameCanvas.GetComponent<Canvas>().enabled = false;
                currentGameCanvas = gameCanvases.FirstOrDefault(x => x.name == "INBInside");
                currentGameCanvas.GetComponent<Canvas>().enabled = true;
                GameObject.Find("Outside Floor").GetComponent<Image>().enabled = false;
                break;

            case Location.INBLecture:
                player.transform.position = leftpos;

                currentGameCanvas.GetComponent<Canvas>().enabled = false;
                currentGameCanvas = gameCanvases.FirstOrDefault(x => x.name == "INBLecture");
                currentGameCanvas.GetComponent<Canvas>().enabled = true;
                GameObject.Find("Outside Floor").GetComponent<Image>().enabled = false;
                atLecture2INB = true;
                break;

            case Location.LibraryOutside:
                if (!player.facingleft)
                    player.transform.position = leftpos;
                else
                    player.transform.position = rightpos;

                currentGameCanvas.GetComponent<Canvas>().enabled = false;
                currentGameCanvas = gameCanvases.FirstOrDefault(x => x.name == "LibraryOutside");
                currentGameCanvas.GetComponent<Canvas>().enabled = true;
                GameObject.Find("Outside Floor").GetComponent<Image>().enabled = true;
                break;

            case Location.Filler1:
                if (!player.facingleft)
                    player.transform.position = leftpos;
                else
                    player.transform.position = rightpos;

                currentGameCanvas.GetComponent<Canvas>().enabled = false;
                currentGameCanvas = gameCanvases.FirstOrDefault(x => x.name == "Filler1");
                currentGameCanvas.GetComponent<Canvas>().enabled = true;
                GameObject.Find("Outside Floor").GetComponent<Image>().enabled = true;
                break;

            case Location.Filler2:
                if (!player.facingleft)
                    player.transform.position = leftpos;
                else
                    player.transform.position = rightpos;

                currentGameCanvas.GetComponent<Canvas>().enabled = false;
                currentGameCanvas = gameCanvases.FirstOrDefault(x => x.name == "Filler2");
                currentGameCanvas.GetComponent<Canvas>().enabled = true;
                GameObject.Find("Outside Floor").GetComponent<Image>().enabled = true;
                break;

            case Location.Workshop1:
                currentGameCanvas.GetComponent<Canvas>().enabled = false;
                currentGameCanvas = gameCanvases.FirstOrDefault(x => x.name == "Workshop");
                GameObject.Find("Outside Floor").GetComponent<Image>().enabled = false;
                atWorkshop1door = true;
                GameObject.Find("Workshop").GetComponent<Canvas>().enabled = true;
                GameObject.Find("Screen").GetComponent<WorkshopGame>().ID = 1;
                GameObject.Find("Player").GetComponent<Image>().enabled = false;
                break;

            case Location.Workshop2:
                currentGameCanvas.GetComponent<Canvas>().enabled = false;
                GameObject.Find("Outside Floor").GetComponent<Image>().enabled = false;
                currentGameCanvas = gameCanvases.FirstOrDefault(x => x.name == "Workshop");
                atWorkshop2door = true;
                GameObject.Find("Workshop").GetComponent<Canvas>().enabled = true;
                GameObject.Find("Screen").GetComponent<WorkshopGame>().ID = 2;
                GameObject.Find("Player").GetComponent<Image>().enabled = false;
                break;

            case Location.LibraryComputer:
                currentGameCanvas.GetComponent<Canvas>().enabled = false;
                GameObject.Find("Outside Floor").GetComponent<Image>().enabled = false;
                atLecture1LIB = atLecture2LIB = atWorkshop1LIB = atWorkshop2LIB = true;
                break;
        }
    }

    void Start()
    {
        var lecturer1 = GameObject.Find("Lecturer1-Male").GetComponent<LecturerData>().Lecturer;
        var lecturer2 = GameObject.Find("Lecturer2-Female").GetComponent<LecturerData>().Lecturer;
        lecture1 = new Lecture
        {
            LectureID = 1,
            Lecturer = lecturer1,
            Dialogue = new string[]
            {
                "Good morning. And welcome to your first lecture. This is one of 7 modules of your 1st year. These include Programming and Data Structures...",
                "... this is a three year course with the option to do a year in industry after your second year...",
                "...and that's why the meaning of life is 42. Leading on from that though...",
                "...Welcome to Introductory Game Studies. Today we’ll be discussing C#...",
                "...ZX Spectrum, one of the earlier home computers to find success in the UK developed by Sinclair...",
                "...if you need things clarified further I would also recommend looking at page 121 in...",
                "...LeBlanc defined 8 different types. These included discovery, narrative, sensation,...",
                "...Well that about wraps things up for today. Make sure to sign that register before you leave (please note this is a game, you don’t have to actually sign it)."
            }
        };

        lecture2 = new Lecture
        {
            LectureID = 2,
            Lecturer = lecturer2,
            Dialogue = new string[]
            {
                "Afternoon everyone. Welcome back to lectures. Let’s get right into it...",
                "...within the 6th generation, consoles such as the Playstation 2 and Xbox were being developed for...",
                "...we now interrupt your regularly scheduled lecture to bring you memes...",
                "...your next assignment will require you to work with C++ to create a...",
                "...Fantasy is another of these 8 types of fun...",
                "...Today we will be working on Web Authoring. So let's get right into talking about html..."
            }
        };
        workshop1 = new WorkshopData
        {
            WorkshopID = 1
        };
        workshop2 = new WorkshopData
        {
            WorkshopID = 2
        };

        Keys = new List<KeyCode>();
        var start = GameObject.Find("Start");
        var settings = GameObject.Find("Settings");
        var end = GameObject.Find("Exit");

        var startslctable = start.GetComponent<Button>().navigation;
        var settsslctable = settings.GetComponent<Button>().navigation;
        var endslctable = end.GetComponent<Button>().navigation;

        startslctable.selectOnDown = end.GetComponent<Button>();
        endslctable.selectOnUp = start.GetComponent<Button>();

        start.GetComponent<Button>().navigation = startslctable;
        end.GetComponent<Button>().navigation = endslctable;

        foreach (Transform obj in transform)
        {
            obj.gameObject.GetComponent<Canvas>().enabled = false;
        }

        SFXVolume = 100;
        MUSVolume = 100;
        ChangeMenu(0);
    }

    void Update()
    {
        var vert = Input.GetAxisRaw("Vertical");
        var currObj = @event.currentSelectedGameObject;
        switch (Status)
        {
            case MenuType.Main:
                foreach(KeyCode code in Enum.GetValues(typeof(KeyCode)))
                {
                    if(Input.GetKeyUp(code))
                    {
                        Keys.Add(code);
                    }
                }
                if(!Konami.Except(Keys).Any())
                {
                    var start = GameObject.Find("Start");
                    var settings = GameObject.Find("Settings");
                    var end = GameObject.Find("Exit");

                    var startslctable = start.GetComponent<Button>().navigation;
                    var endslctable = end.GetComponent<Button>().navigation;

                    startslctable.selectOnDown = settings.GetComponent<Button>();
                    endslctable.selectOnUp = settings.GetComponent<Button>();

                    start.GetComponent<Button>().navigation = startslctable;
                    end.GetComponent<Button>().navigation = endslctable;

                    settings.GetComponent<Text>().enabled = true;
                    settings.GetComponent<Button>().enabled = true;
                }
                break;

            case MenuType.SettingsVolume:
                if (Status == MenuType.SettingsVolume)
                {
                    if (currObj.name == "SFXValue")
                    {
                        SFXVolume += (int)vert;
                        if (SFXVolume > 100)
                            SFXVolume = 100;
                        if (SFXVolume < 0)
                            SFXVolume = 0;
                        currObj.GetComponent<Text>().text = SFXVolume.ToString()+"%";
                        return;
                    }
                    if (currObj.name == "MUSValue")
                    {
                        MUSVolume += (int)vert;
                        if (MUSVolume > 100)
                            MUSVolume = 100;
                        if (MUSVolume < 0)
                            MUSVolume = 0;
                        currObj.GetComponent<Text>().text = MUSVolume.ToString()+"%";
                        return;
                    }
                }
                break;

            case MenuType.Intro:
                if(finishedspeech)
                {
                    ChangeMenu(3);
                }
                break;
        }
    }

    public void ChangeValue(GameObject sender)
    {
        if (sender.name == "ControlsValue")
        {
            if (Control == ContEnum.Arcade)
            {
                Control = ContEnum.All;
            }
            else
            { Control++; }
            hori1Val.GetComponent<Text>().text = Control.ToString();            
        }
        if (sender.name == "DifficultyValue")
        {
            if (Difficulty == DiffEnum.Hard)
            {
                Difficulty = DiffEnum.Easy;
            }
            else
            { Difficulty++; }
            hori2Val.GetComponent<Text>().text = Difficulty.ToString();            
        }
    }

    IEnumerator Typewrite(Text obj, string[] dialogue, float totalseconds)
    {
        foreach(var item in dialogue)
        {
            int total = item.Length;
            int progress = 0;
            while (progress < total)
            {
                obj.text += item[progress];
                progress++;
                yield return new WaitForSeconds(totalseconds / (total / 0.25f));
            }
            yield return new WaitForSeconds(5.0f);
            obj.text = "";
        }
        yield return null;
        finishedspeech = true;
    }
}
