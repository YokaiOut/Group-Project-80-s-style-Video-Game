using UnityEngine;
using UnityEngine.UI;

public class Group : MonoBehaviour
{
    public Sprite[] Groups;
    
	void Start () {
        GetComponent<Image>().sprite = Groups[Random.Range(0, Groups.Length - 1)];
	}
}
