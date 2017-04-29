using UnityEngine;
using System.Collections;

public class TireController:MonoBehaviour
{

    public bool IsKeepTurning;
    public GameObject[] TireModels;
    public GameObject[] TireSpaces;

    float defaultAngle = 30;
    float totalAngle = 0f;
    float rotateSpeed = 5f;
    public TireController()
    {
        TireSpaces = new GameObject[2];
        TireModels = new GameObject[2];
    }
    public void TurnLeft()
    {
        Turn(-defaultAngle);
    }
    public void TurnRight()
    {
        Turn(defaultAngle);
    }
	bool isRestore;
    public void Restore()
    {
        if (totalAngle != 0)
        {
			isRestore = true;
            Turn(-totalAngle);
			isRestore = false;
            totalAngle = 0;
        }
    }
    void Turn(float angle)
    {
        if (totalAngle == 0 || isRestore)
        {
            totalAngle = angle;
            foreach (GameObject i in TireSpaces)
            {
                i.transform.Rotate(Vector3.up, angle);
            }
        }
    }
	public void Forward()
    {
        if (TireModels != null)
        {
            foreach (GameObject i in TireModels)
            {
                i.transform.Rotate(Vector3.right, rotateSpeed);
            }
        }
    }
    public void Backward()
    {
        if (TireModels != null)
        {
            foreach (GameObject i in TireModels)
            {
                i.transform.Rotate(Vector3.right, -rotateSpeed);
            }
        }
    }


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
