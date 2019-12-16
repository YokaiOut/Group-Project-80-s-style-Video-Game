using UnityEngine;
using UnityEngine.UI;

public class Bounce : MonoBehaviour {
	public GameController controller;
    public Sprite[] frames;
    public int Upper;
    public int Lower;
    public bool LeftRight = false;
    public bool Flipped = false;

    bool GoUp;
    int position = 0;
    bool reverseanim = false;

	void FixedUpdate () {
        if (GetComponent<Image>().enabled && controller.Status == MenuType.Game)
        {
            if (frames.Length > 0)
            {
                if (reverseanim)
                {
                    position--;
                }
                else
                {
                    position++;
                }
                if (position >= frames.Length)
                {
                    position = (frames.Length - 1);
                    reverseanim = true;
                }
                if(position < 0)
                {
                    position = 0;
                    reverseanim = false;
                }
                GetComponent<Image>().sprite = frames[position];
            }
            if (GoUp)
            {
                if (Flipped)
                {
                    transform.Translate(Vector3.down);
                }
                else
                {
                    transform.Translate(Vector3.up);
                }
                if (!LeftRight)
                {
                    if (transform.position.y > Upper)
                    {
                        GoUp = false;
                    }
                }
                else
                {
                    if (transform.position.x > Upper || transform.position.x < Lower)
                    {
                        GoUp = false;
                    }
                }
            }
            else
            {
                if (!Flipped)
                {
                    transform.Translate(Vector3.down);
                }
                else
                {
                    transform.Translate(Vector3.up);
                }
                if (!LeftRight)
                {
                    if (transform.position.y < Lower)
                    {
                        GoUp = true;
                    }
                }
                else
                {
                    if (transform.position.x < Lower || transform.position.x > Upper)
                    {
                        GoUp = true;
                    }
                }
            }
        }
    }
}
