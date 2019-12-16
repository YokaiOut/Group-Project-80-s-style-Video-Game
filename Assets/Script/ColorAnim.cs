using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ColorAnim : MonoBehaviour {
    Color currentColor = Color.black;
    public Image targetImage;
    public float Duration = 5.0f;
    public float Smoothness = 0.02f;
    public float Delay = 5.0f;
    public float RepeatDelay = 10.0f;

    void Start()
    {
        InvokeRepeating("LerpColorController", Delay, RepeatDelay);
    }

    void FixedUpdate()
    {
        targetImage.color = currentColor;
    }

    void LerpColorController()
    {
        StartCoroutine(LerpColor());
    }

    IEnumerator LerpColor()
    {
        Color col = Random.ColorHSV();
        var img = GetComponent<Image>();
        float progress = 0.0f;
        float increm = Smoothness / Duration;
        while (progress < 1)
        {
            currentColor = Color.Lerp(img.color, col, progress);
            progress += increm;
            yield return new WaitForSeconds(Smoothness);
        }
    }
}
