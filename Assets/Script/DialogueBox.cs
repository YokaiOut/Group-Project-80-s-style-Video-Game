using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBox : MonoBehaviour {

    bool isShown { get { return GetComponent<Canvas>().enabled; } }
    public bool finishedTypewriting;
    Image bustImg { get { return GameObject.Find("CharacterImage").GetComponent<Image>(); } }
    Text charText { get { return GameObject.Find("CharacterName").GetComponent<Text>(); } }
    Text dialogueText { get { return GameObject.Find("DialogueText").GetComponent<Text>(); } }
    Text continueText { get { return GameObject.Find("ContinueText").GetComponent<Text>(); } }

    void Start()
    {
        finishedTypewriting = true;
    }

    void Update()
    {
        if(isShown)
        {
            if(finishedTypewriting)
            {
                continueText.enabled = true;
                return;
            }
            else
            {
                continueText.enabled = false;
            }
        }
    }

    public void SetText(DialogueText text)
    {
        finishedTypewriting = false;
        charText.text = text.Name;
        dialogueText.text = text.Dialogue;
        finishedTypewriting = true;
    }

    public void SetImage(Sprite sprite)
    {
        bustImg.sprite = sprite;
        bustImg.preserveAspect = true;
    }

    public void SetTextWithImage(DialogueText text, Sprite sprite)
    {
        if(finishedTypewriting)
        {
            finishedTypewriting = false;
            bustImg.sprite = sprite;
            bustImg.preserveAspect = true;
            charText.text = text.Name;
            dialogueText.text = text.Dialogue;
            finishedTypewriting = true;
        }
    }

    public void SetTextAsTypewriter(DialogueText text, float totalseconds)
    {
        if(finishedTypewriting)
        {
            finishedTypewriting = false;
            charText.text = text.Name;
            dialogueText.text = "";
            StartCoroutine(Typewrite(text.Dialogue, 5));
        }
    }

    public void SetTextAsTypewriter(DialogueText text, Sprite sprite, float totalseconds)
    {
        if(finishedTypewriting)
        {
            finishedTypewriting = false;
            bustImg.sprite = sprite;
            bustImg.preserveAspect = true;
            charText.text = text.Name;
            dialogueText.text = "";
            StartCoroutine(Typewrite(text.Dialogue, 5));
        }
    }

    public void SetTextAsTypewriter(DialogueText text, Sprite sprite, AudioClip audioclip, AudioSource audioSource, float totalseconds)
    {
        if(finishedTypewriting)
        {
            finishedTypewriting = false;
            bustImg.sprite = sprite;
            bustImg.preserveAspect = true;
            charText.text = text.Name;
            dialogueText.text = "";
            audioSource.clip = audioclip;
            StartCoroutine(TypewriteWithSound(text.Dialogue, 5, audioSource));
        }
    }

    IEnumerator Typewrite(string dialogue, float totalseconds)
    {
        int total = dialogue.Length;
        int progress = 0;
        while (progress < total)
        {
            dialogueText.text += dialogue[progress];
            progress++;
            yield return new WaitForSeconds(totalseconds / (total / 0.25f));
        }
        finishedTypewriting = true;
        yield return null;
    }

    IEnumerator TypewriteWithSound(string dialogue, float totalseconds, AudioSource audioSource)
    {
        int total = dialogue.Length;
        int progress = 0;
        while (progress < total)
        {
            dialogueText.text += dialogue[progress];
            audioSource.Play();
            progress++;
            yield return new WaitForSeconds(totalseconds/(total/0.5f));
        }
        finishedTypewriting = true;
        yield return null;
    }
}
