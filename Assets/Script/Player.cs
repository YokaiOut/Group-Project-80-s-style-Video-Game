using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections;

public class Player : MonoBehaviour
{
	public Vector2 velocity;
    public AudioClip movementClip;
    public AudioClip movementStairsClip;
    public Sprite Bust;
    public bool facingleft = false;
    string DialogueName = "The Player";

    AudioSource audioSource { get { return GetComponent<AudioSource>(); } }
	GameController controller { get { return GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameController>(); } }

    GameObject dialogue { get { return GameObject.Find("DialogueBox"); } }
    DialogueBox box { get { return dialogue.GetComponent<DialogueBox>(); } }
    bool dialogueenabled { get { return dialogue.GetComponent<Canvas>().enabled; } }

    float hori = 0.0f;
    float vert = 0.0f;
    bool sub = false;
    bool cancel = false;
    
    bool atMainSign = false;
    bool intro = true;

    bool checktolibrary = false;
    bool checktoinb = false;

    bool atLectureInspector = false;
    bool atGroupInspector = false;

    bool busy = false;
    bool inLecture = false;
    bool checkToLecture = false;
    bool checkToWorkshop = false;
    bool checkToWorkshop2 = false;

    public bool inWorkshop = false;
    public bool INBWorkshop = false;    

    void Update ()
    {
        if (controller.Status == MenuType.Game)
        {
            if(Input.GetKeyDown(KeyCode.Alpha1))
            {
                controller.MoveToLocation(Location.INBLecture);
            }

            sub = Input.GetButtonDown("Submit");
            cancel = Input.GetButtonDown("Cancel");

            if (!inLecture && !inWorkshop && !busy)
            {
                hori = Input.GetAxisRaw("Horizontal");
                vert = Input.GetAxisRaw("Vertical");
                     
                if(sub)
                {
                    if(dialogueenabled)
                    {
                        dialogue.GetComponent<Canvas>().enabled = false;
                    }
                }
                
                if(controller.Location == Location.Filler1 || controller.Location == Location.Filler2)
                {
                    if(atGroupInspector)
                    {
                        if(sub || vert > 0)
                        {
                            int location = 0;
                            if(controller.Location == Location.Filler1)
                            {
                                location = System.Convert.ToInt32(GameObject.Find("Filler1").transform.GetChild(1).GetComponent<Image>().sprite.name.Replace("Group ", ""))-1;
                            }
                            else
                            {
                                location = System.Convert.ToInt32(GameObject.Find("Filler2").transform.GetChild(1).GetComponent<Image>().sprite.name.Replace("Group ", ""))-1;
                            }
                            dialogue.GetComponent<Canvas>().enabled = true;
                            box.SetTextAsTypewriter(new DialogueText
                            {
                                Name = DialogueName,
                                Dialogue = controller.GroupText[location]
                            },
                            Bust, 5.0f);
                            return;
                        }
                    }
                }

                if (controller.Location == Location.LibraryOutside)
                {
                    if (checktolibrary)
                    {
                        if (sub || vert > 0)
                        {
                            controller.MoveToLocation(Location.Filler2);
                            checktolibrary = false;
                            return;
                        }
                    }
                }

                if (controller.Location == Location.INBOutside)
                {
                    if (checktolibrary)
                    {
                        if (sub || vert > 0)
                        {
                            controller.MoveToLocation(Location.Filler2);
                            checktolibrary = false;
                            return;
                        }
                    }

                    if (checktoinb)
                    {
                        if (sub || vert > 0)
                        {
                            controller.MoveToLocation(Location.INBInside);
                            checktoinb = false;
                            return;
                        }
                    }
                }
                if (controller.Location == Location.INBInside)
                {
                    if (checktoinb)
                    {
                        if (sub || vert > 0)
                        {
                            controller.MoveToLocation(Location.INBOutside);
                            checktoinb = false;
                            return;
                        }
                    }
                    if(checkToLecture)
                    {
                        if (sub || vert > 0)
                        {
                            controller.MoveToLocation(Location.INBLecture);
                            checkToLecture = false;
                            return;
                        }
                    }
                    if(checkToWorkshop)
                    {
                        if (sub || vert > 0)
                        {
                            controller.MoveToLocation(Location.Workshop1);
                            checkToLecture = false;
                            return;
                        }
                    }
                    if (checkToWorkshop2)
                    {
                        if (sub || vert > 0)
                        {
                            controller.MoveToLocation(Location.Workshop2);
                            checkToLecture = false;
                            return;
                        }
                    }
                }
                if (controller.Location == Location.MinervaOutside)
                {
                    if (checkToLecture)
                    {
                        if (sub || vert > 0)
                        {
                            GameObject.Find("DoorArrow").GetComponent<Image>().enabled = false;
                            GameObject.Find("Minerva").GetComponent<OpenDoor>().RevertImage();
                            controller.MoveToLocation(Location.MinervaLecture);
                            checkToLecture = false;
                            return;
                        }
                    }
                }

                if (hori != 0)
                {
                    if (hori < 0)
                    {
                        if (!facingleft)
                        {
                            transform.Rotate(0, 180, 0);
                            facingleft = true;
                        }
                    }
                    if (hori > 0)
                    {
                        if (facingleft)
                        {
                            transform.Rotate(0, 180, 0);
                            facingleft = false;
                        }
                    }

                    if (!facingleft)
                    {
                        transform.Translate(Vector3.right * (hori * velocity.x));
                    }
                    else
                    {
                        transform.Translate(Vector3.right * (-hori * velocity.x));
                    }
                    audioSource.clip = movementClip;
                    if (!audioSource.isPlaying)
                    {
                        audioSource.Play();
                    }
                }
                else
                {
                    audioSource.Stop();
                }
                if (vert != 0)
                {
                    if (atLectureInspector)
                    {
                        if (controller.atLecture2INB || controller.atLecture1MIN)
                        {
                            inLecture = true;
                        }
                    }
                    if (controller.atWorkshop1door || controller.atWorkshop1LIB)
                    {
                        controller.MoveToLocation(Location.Workshop1);
                        return;
                    }
                    if (controller.atWorkshop2door || controller.atWorkshop2LIB)
                    {
                        controller.MoveToLocation(Location.Workshop2);
                        return;
                    }
                    if (controller.atLecture1MIN || controller.atLecture1LIB)
                    {
                        controller.MoveToLocation(Location.MinervaLecture);
                        return;
                    }
                    if (controller.atLecture2INB || controller.atLecture2LIB)
                    {
                        controller.MoveToLocation(Location.INBLecture);
                        return;
                    }
                }
                if (sub)
                {
                    if (atLectureInspector)
                    {
                        if (controller.atLecture2INB || controller.atLecture1MIN)
                        {
                            inLecture = true;
                        }
                    }
                    if (atMainSign && !intro)
                    {
                        dialogue.GetComponent<Canvas>().enabled = true;
                        box.SetTextAsTypewriter(new DialogueText
                        {
                            Name = DialogueName,
                            Dialogue = "Something something the sign is nice, something something."
                        },
                        Bust, 5.0f);
                    }
                    if (atMainSign && intro)
                    {
                        dialogue.GetComponent<Canvas>().enabled = true;
                        box.SetTextAsTypewriter(new DialogueText
                        {
                            Name = DialogueName,
                            Dialogue = "Well, here's to spending 3 years of my life to further my chances of obtaining my dream!"
                        },
                        Bust, 5.0f);
                        intro = false;
                    }
                }
            }
            else
            {
                Lecture lecture;
                WorkshopData workshop;

                if(controller.Location == Location.INBLecture)
                {
                    lecture = controller.lecture2;
                }
                else
                {
                    lecture = controller.lecture1;
                }

                if(controller.Location == Location.Workshop2)
                {
                    workshop = controller.workshop2;
                }
                else
                {
                    workshop = controller.workshop1;
                }

                if(inLecture)
                {
                    busy = true;
                    inLecture = false;
                    dialogue.GetComponent<Canvas>().enabled = true;
                    StartCoroutine(Lecture(lecture));
                    return;
                }
                if(inWorkshop)
                {
                    //StartCoroutine(Workshop(workshop));
                    return;
                }
            }
        }
    }

    void HideDialogue()
    {
        dialogue.GetComponent<Canvas>().enabled = false;
    }

    IEnumerator Lecture(Lecture lecture)
    {
        int progress = 0;
        foreach(var line in lecture.Dialogue)
        {
            box.SetTextAsTypewriter(new DialogueText
            {
                Name = lecture.Lecturer.Name,
                Dialogue = line
            },
            lecture.Lecturer.Bust, 5.0f);
            progress++;
            yield return new WaitForSeconds(5.0f);
        }
        HideDialogue();
        busy = false;
        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.name)
        {
            case "ToMinervaFromStart":
                if (controller.Location == Location.Start)
                {
                    controller.MoveToLocation(Location.MinervaOutside);
                }
                break;

            case "Minerva":
                if (controller.Location == Location.MinervaOutside)
                {
                    GameObject.Find("DoorArrow").GetComponent<Image>().enabled = true;
                    collision.GetComponent<OpenDoor>().SwapImage();
                    checkToLecture = true;
                    return;
                }
                break;

            case "ToINBFromFiller1":
                if (controller.Location == Location.Filler1)
                {
                    controller.MoveToLocation(Location.INBOutside);
                    return;
                }
                break;

            case "ToMINFromFiller1":
                if (controller.Location == Location.Filler1)
                {
                    controller.MoveToLocation(Location.MinervaOutside);
                    return;
                }
                break;

            case "ToINBFromFiller2":
                if (controller.Location == Location.Filler2)
                {
                    controller.MoveToLocation(Location.INBOutside);
                    return;
                }
                break;

            case "ToLIBFromFiller2":
                if (controller.Location == Location.Filler2)
                {
                    controller.MoveToLocation(Location.LibraryOutside);
                    return;
                }
                break;

            case "ToFiller2FromINB":
                if (controller.Location == Location.INBOutside)
                {
                    GameObject.Find("Filler2ArrowINB").GetComponent<Image>().enabled = true;
                    checktolibrary = true;
                    return;
                }
                break;

            case "ToFiller2FromLIB":
                if (controller.Location == Location.LibraryOutside)
                {
                    controller.MoveToLocation(Location.Filler2);
                    return;
                }
                break;

            case "ToMinervaFromINB":
                if (controller.Location == Location.INBOutside)
                {
                    controller.MoveToLocation(Location.MinervaOutside);
                    return;
                }
                break;
            case "ToINBFromMinerva":
                if (controller.Location == Location.MinervaOutside)
                {
                    controller.MoveToLocation(Location.INBOutside);
                    return;
                }
                break;
            case "UniSign":
                if (controller.Location == Location.Start)
                {
                    collision.transform.GetChild(0).GetComponent<Image>().enabled = true;
                    atMainSign = true;
                }
                break;

            case "INB":
                if (controller.Location == Location.INBOutside)
                {
                    GameObject.Find("ToInsideINB").GetComponent<Image>().enabled = true;
                    checktoinb = true;
                }
                break;
            case "StairsINB":
                if (controller.Location == Location.INBInside)
                {
                    GameObject.Find("StairsArrowINB").GetComponent<Image>().enabled = true;
                    checkToWorkshop = true;
                }
                break;

            case "ExitINB":
                if (controller.Location == Location.INBInside)
                {
                    GameObject.Find("ExitArrowINB").GetComponent<Image>().enabled = true;
                    checktoinb = true;
                }
                break;

            case "Workshop2DoorINB":
                if (controller.Location == Location.INBInside)
                {
                    GameObject.Find("Workshop2Arrow").GetComponent<Image>().enabled = true;
                    checkToWorkshop2 = true;
                }
                break;

            case "LectureDoorINB":
                if (controller.Location == Location.INBInside)
                {
                    GameObject.Find("LectureINBArrow").GetComponent<Image>().enabled = true;
                    checkToLecture = true;
                }
                break;

            case "NPCGroup":
                if (controller.Location == Location.Filler1 || controller.Location == Location.Filler2)
                {
                    collision.transform.GetChild(0).GetComponent<Image>().enabled = true;
                    atGroupInspector = true;
                }
                break;

            case "INBLectureHall":
                if (controller.Location == Location.INBLecture)
                {
                    collision.transform.GetChild(0).GetComponent<Image>().enabled = true;
                    atLectureInspector = true;
                }
                break;

            case "MINLectureHall":
                if (controller.Location == Location.MinervaLecture)
                {
                    collision.transform.GetChild(0).GetComponent<Image>().enabled = true;
                    atLectureInspector = true;
                }
                break;

            case "Library":
                if (controller.Location == Location.LibraryOutside)
                {
                    collision.transform.GetChild(0).GetComponent<Image>().enabled = true;
                }
                break;
        }
    }
    
	void OnTriggerExit2D(Collider2D collision)
    {
        switch (collision.name)
        {
            case "Minerva":
                if (controller.Location == Location.MinervaOutside)
                {
                    GameObject.Find("DoorArrow").GetComponent<Image>().enabled = false;
                    collision.GetComponent<OpenDoor>().RevertImage();
                    checkToLecture = false;
                }
                break;

            case "MinervaArrowStart":
                if (controller.Location == Location.Start)
                {
                    GameObject.Find("ToMinervaStart").GetComponent<Image>().enabled = false;
                }
                break;

            case "MinervaArrowFiller1":
                if (controller.Location == Location.Filler1)
                {
                    GameObject.Find("ToMinervaFiller1").GetComponent<Image>().enabled = false;
                }
                break;

            case "INBArrowFiller1":
                if (controller.Location == Location.Filler1)
                {
                    GameObject.Find("ToINBFiller1").GetComponent<Image>().enabled = false;
                }
                break;

            case "Filler1ArrowINB":
                if (controller.Location == Location.INBOutside)
                {
                    GameObject.Find("ToFiller1INB").GetComponent<Image>().enabled = false;
                }
                break;

            case "Filler1ArrowMIN":
                if (controller.Location == Location.MinervaOutside)
                {
                    GameObject.Find("ToFiller1Minerva").GetComponent<Image>().enabled = false;
                }
                break;

            case "ToFiller2FromINB":
                if (controller.Location == Location.INBOutside)
                {
                    GameObject.Find("Filler2ArrowINB").GetComponent<Image>().enabled = false;
                    checktolibrary = false;
                }
                break;

            case "Filler2ArrowLIB":
                if (controller.Location == Location.LibraryOutside)
                {
                    GameObject.Find("Filler2ArrowLIB").GetComponent<Image>().enabled = false;
                    checktolibrary = false;
                }
                break;

            case "INBFiller2Arrow":
                if (controller.Location == Location.Filler2)
                {
                    GameObject.Find("ToINBFiller2").GetComponent<Image>().enabled = false;
                }
                break;

            case "LIBFiller2Arrow":
                if (controller.Location == Location.Filler2)
                {
                    GameObject.Find("ToLIBFiller2").GetComponent<Image>().enabled = false;
                }
                break;

            case "UniSign":
                if (controller.Location == Location.Start)
                {
                    collision.transform.GetChild(0).GetComponent<Image>().enabled = false;
                    atMainSign = false;
                }
                break;
                
            case "INB":
                if (controller.Location == Location.INBOutside)
                {
                    GameObject.Find("ToInsideINB").GetComponent<Image>().enabled = false;
                    checktoinb = false;
                }
                break;

            case "StairsINB":
                if (controller.Location == Location.INBInside)
                {
                    GameObject.Find("StairsArrowINB").GetComponent<Image>().enabled = false;
                    checkToWorkshop = true;
                }
                break;

            case "ExitINB":
                if (controller.Location == Location.INBInside)
                {
                    GameObject.Find("ExitArrowINB").GetComponent<Image>().enabled = false;
                    checktoinb = false;
                }
                break;

            case "Workshop2DoorINB":
                if (controller.Location == Location.INBInside)
                {
                    GameObject.Find("Workshop2Arrow").GetComponent<Image>().enabled = false;
                    checkToWorkshop = false;
                }
                break;

            case "LectureDoorINB":
                if (controller.Location == Location.INBInside)
                {
                    GameObject.Find("LectureINBArrow").GetComponent<Image>().enabled = false;
                    checkToLecture = false;
                }
                break;

            case "NPCGroup":
                if (controller.Location == Location.Filler1 || controller.Location == Location.Filler2)
                {
                    collision.transform.GetChild(0).GetComponent<Image>().enabled = false;
                    atGroupInspector = false;
                    box.GetComponent<Canvas>().enabled = false;
                }
                break;

            case "INBLectureHall":
                if (controller.Location == Location.INBLecture)
                {
                    collision.transform.GetChild(0).GetComponent<Image>().enabled = false;
                    atLectureInspector = false;
                }
                break;

            case "MINLectureHall":
                if(controller.Location == Location.MinervaLecture)
                {
                    collision.transform.GetChild(0).GetComponent<Image>().enabled = false;
                    atLectureInspector = false;
                }
                break;

            case "Library":
                if (controller.Location == Location.LibraryOutside)
                {
                    collision.transform.GetChild(0).GetComponent<Image>().enabled = false;
                }
                break;
        }
    }
}
