using UnityEngine;
using UnityEngine.UI;

public class OpenDoor : MonoBehaviour
{
    public Sprite OpenDoorImage;
    Sprite OriginalImage;

    private void Start()
    {
        OriginalImage = GetComponent<Image>().sprite;
    }

    public void SwapImage()
    {
        GetComponent<Image>().sprite = OpenDoorImage;
    }

    public void RevertImage()
    {
        GetComponent<Image>().sprite = OriginalImage;
    }
}
