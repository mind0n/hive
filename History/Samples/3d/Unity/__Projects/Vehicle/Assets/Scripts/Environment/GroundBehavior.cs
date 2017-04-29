using UnityEngine;
using System.Collections;

public class GroundBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnCollisionEnter(Collision collision)
	{
		Debug.Log(collision.collider.name + " ground hit " + collision.contacts.Length);
	}

	void OnCollisionExit(Collision collision)
	{
		Debug.Log(collision.collider.name + " ground through ");
	}
}
