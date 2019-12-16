using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;

public class WorkshopGame : MonoBehaviour
{
    public int ID;

    Canvas canvas { get { return transform.parent.GetComponent<Canvas>(); } }
    Text question { get { return GameObject.Find("Question").GetComponent<Text>(); } }
    Text answer { get { return GameObject.Find("UserChoice").GetComponent<Text>(); } }

    int useranswer = 0;
    int userquestion = 0;

    public GameController controller;

    string[] questions;
    List<string[]> answers;
    int[] correctanswers;

    List<int> useranswers;

    private void Start()
    {
        if(ID == 1)
        {
            useranswers = new List<int>();
            questions = new string[]
            {
                "During your course you will be working with various programming languages. Which of these will you not be working with in your first and second year?",
                "Which of these modules will not be covered in your first year?",
                "Which company developed the ZX Spectrum?",
                "PS2, Xbox and Gamecube are considered what generation?"
            };
            answers = new List<string[]>
            {
                new string[]{ "C++", "Python", "C Sharp", "HTML" },
                new string[]{ "Programming and Data Structures", "Introductory Games Studies", "Computer Architectures", "Writing Poetry" },
                new string[]{ "Acorn Computers", "Sinclair Research", "Penguin Group", "Apple" },
                new string[]{ "4th Generation", "5th Generation", "6th Generation", "8th Generation" }
            };
            correctanswers = new int[]
            {
                1, 3, 1, 2
            };
        }
        if(ID ==2)
        {
            questions = new string[]
            {
                "During your course you will be working with various programming languages. Which of these will you not be working with in your first and second year?",
                "Which of these modules will not be covered in your first year?",
                "Which company developed the ZX Spectrum?",
                "PS2, Xbox and Gamecube are considered what generation?"
            };
            answers = new List<string[]>
            {
                new string[]{ "C++", "Python", "C Sharp", "HTML" },
                new string[]{ "Programming and Data Structures", "Introductory Games Studies", "Computer Architectures", "Writing Poetry" },
                new string[]{ "Acorn Computers", "Sinclair Research", "Penguin Group", "Apple" },
                new string[]{ "4th Generation", "5th Generation", "6th Generation", "8th Generation" }
            };
            correctanswers = new int[]
            {
                1, 3, 1, 2
            };
        }
    }

    private void FixedUpdate()
    {
        var key1 = Input.GetButtonDown("Fire1");
        var key2 = Input.GetButtonDown("Fire2");
        var key3 = Input.GetButtonDown("Fire3");
        var sub = Input.GetButtonDown("Submit");
        var cancel = Input.GetButtonDown("Cancel");

        if(canvas.enabled)
        {
            if (cancel)
            {
                controller.MoveToLocation(Location.INBInside);
                controller.player.inWorkshop = false;
            }

            if(key2)
            {
                useranswer++;
                if (useranswer > 3)
                    useranswer = 0;
            }
            if(key3)
            {
                userquestion++;
                if (userquestion > 3)
                    userquestion = 0;
            }

            question.text = questions[userquestion];
            answer.text = answers[userquestion][useranswer];

            if(sub)
            {
                useranswers.Add(useranswer);
                if (userquestion == 3)
                {
                    GameObject.Find("Quiz").GetComponent<Canvas>().enabled = false;
                    GameObject.Find("Results").GetComponent<Canvas>().enabled = true;
                    int questionsright = 0;
                    for(int x=0;x<questions.Length;x++)
                    {
                        if (useranswers[x] == correctanswers[x])
                            questionsright++;
                    }
                    GameObject.Find("ResultsText").GetComponent<Text>().text = questionsright+"/" + questions.Length;
                }
                userquestion++;
                if (userquestion > 3)
                    userquestion = 0;
            }
        }
    }

    public void Reset()
    {
        useranswer = 0;
        userquestion = 0;
        useranswers = new List<int>();
    }
}
