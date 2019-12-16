using System.Collections.Generic;
using UnityEngine;

public class Clouds : MonoBehaviour {
	public float Upper;
	public float Lower;
	public float SpeedLow = 0.01f;
	public float SpeedMax = 0.1f;
    public short AmountOfClouds = 3;
    public GameObject[] CloudObjects;
    System.Random random;
    List<GameObject> CloudsInstantiated;
	Canvas canvas { get { return GetComponent<Canvas>(); } }
    Camera mainCamera { get { return GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>(); } }

	void Start()
	{
        random = new System.Random();
        CloudsInstantiated = new List<GameObject>();
        Instantiate();
	}

    void Instantiate()
    {
        int zloc = 0;
        for (int x = 0; x < AmountOfClouds; x++)
        {
            zloc--;
            var obj = Instantiate(CloudObjects[random.Next(CloudObjects.Length)], canvas.transform);
            float y = Random.Range(Lower, Upper);
            if (obj.tag == "clouds")
            {
                obj.transform.SetPositionAndRotation(new Vector3(random.Next(-150, -50), y, zloc), new Quaternion(0, 0, 0, 0));
            }
            CloudsInstantiated.Add(obj);
        }
    }

    // Update is called once per frame
    void Update ()
    {
        if(mainCamera.GetComponent<GameController>().Status==MenuType.Game)
        {
            if(CloudsInstantiated.Count==0)
            {
                Instantiate();
            }
            else
            {
                int index = 0;
                if (canvas.enabled == true)
                {
                    foreach (var obj in CloudsInstantiated)
                    {
                        if (obj.tag == "clouds")
                        {
                            float x = -(Random.Range(SpeedLow, SpeedMax) / (obj.transform.position.z / 2));
                            obj.transform.Translate(x, 0, 0);
                            if (obj.transform.position.x > 1080)
                            {
                                obj.transform.SetPositionAndRotation(new Vector3(random.Next(-150, -50), obj.transform.position.y, obj.transform.position.z), new Quaternion(0, 0, 0, 0));
                            }
                        }
                        index++;
                    }
                }
            }
        }
        else
        {
            if(CloudsInstantiated.Count>0)
            {
                foreach(var obj in CloudsInstantiated)
                {
                    Destroy(obj);
                }
                CloudsInstantiated.Clear();
            }
        }
	}
}
