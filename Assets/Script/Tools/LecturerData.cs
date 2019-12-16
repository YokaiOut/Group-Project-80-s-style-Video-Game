using UnityEngine;

public class LecturerData : MonoBehaviour
{
    public Lecturer Lecturer;

    public string Name;
    public Sprite Bust;
    public Sprite Image;

    private void Start()
    {
        Lecturer = new Lecturer
        {
            Name = Name,
            Bust = Bust,
            Sprite = Image
        };
    }
}
