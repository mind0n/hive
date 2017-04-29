using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {

	VehicleController ai;
	// Use this for initialization
	void Start () {
		GameObject aiVehicleObj = GameObject.Find("VehicleAI");
		VehicleController aiVehicle = (VehicleController) aiVehicleObj.GetComponent("VehicleController");
		aiVehicle.ResetUserControl();
		aiVehicle.gameObject.SetActive(false);
		ai = (VehicleController)Instantiate(aiVehicle, new Vector3(-10, 0, -10), new Quaternion(0, 0, 0, 0));
		ai.gameObject.SetActive(true);
		//ai.transform.Translate(new Vector3(10, 10, 10));
		ai.ResetUserControl();
	}
	
	// Update is called once per frame
	void Update () {
		if (ai != null)
		{
			//ai.transform.Translate(0, 0, 0.1f);
			ai.Forward();
			ai.SteelLeft();
		}
	}
}
