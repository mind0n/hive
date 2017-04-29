using UnityEngine;
using System.Collections;

public class VehicleController : MonoBehaviour {

    public TireController Tires;
	// Use this for initialization
	void Start () {
	}


    void ProcessKeyAction()
    {
        if (Input.GetKey(KeyCode.A))
        {
            Tires.TurnLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Tires.TurnRight();
        }
        else
        {
            Tires.Restore();
        }
        if (Input.GetKey(KeyCode.W))
        {
            Tires.Forward();
        }
        else if (Input.GetKey(KeyCode.S))
        {
            Tires.Backward();
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Tires.Forward();
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            Tires.Backward();
        }
    }

	// Update is called once per frame
	void Update () {
		//Debug.Break();
		ProcessKeyAction ();
	}
}
