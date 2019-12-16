public struct DialogueText
{
    public string Name { get; set; }
    public string Dialogue { get; set; }
}

public struct Lecture
{
    public Lecturer Lecturer { get; set; }
    public short LectureID { get; set; }
    public string[] Dialogue { get; set; }
}

public struct WorkshopData
{
    public short WorkshopID { get; set; }
    public string[] Questions { get; set; }
    public string[] Answers { get; set; }
}

public struct Lecturer
{
    public string Name { get; set; }
    public UnityEngine.Sprite Bust { get; set; }
    public UnityEngine.Sprite Sprite { get; set; }
}