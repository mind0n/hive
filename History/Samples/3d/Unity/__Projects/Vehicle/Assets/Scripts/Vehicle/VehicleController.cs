using UnityEngine;
using System.Collections;

public class VehicleController : MonoBehaviour {
	public bool IsAI;
    public Camera TailCamera;
    public Camera FireCamera;
	public TireController Tires;
    public CupolaController Cupola;
	public float Speed = 0.5f;
	public float RotSpeed = 5f;
	public bool IsTank;
    public bool IsAntiTurn;
    public GameObject TailPos;
	bool isMoving;
	bool isForwarding;
    float maxCameraDist = 3f;
    float curtDist = 0f;
    float mouseX, mouseY;
    float cameraDistUnit = 0.05f;


	// Use this for initialization
	void Start()
	{
		if (!IsAI)
		{
			ResetUserControl(true);
		}
	}

	public void ResetUserControl(bool status = false)
	{
		IsAI = !status;
		FireCamera.enabled = status;
		TailCamera.enabled = status;
		FireCamera.gameObject.SetActive(status);
		TailCamera.gameObject.SetActive(status);
	}
	// Update is called once per frame
	void Update()
	{
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");
        ProcessMouseAction(mouseX, mouseY);
        ProcessKeyAction();
	}

    private void ProcessMouseAction(float angle, float angleY)
    {
        if (Cupola != null && !IsAI)
        {
            Cupola.Turn(angle);
            Cupola.Hold(-angleY);
        }
		Cupola.ProcessFire();
    }

	void OnCollisionEnter(Collision collision)
	{
		//Debug.Log(collision.collider.name + " hit " + collision.contacts.Length);
	}

	void OnCollisionExit(Collision collision)
	{
		//Debug.Log(collision.collider.name + "Through");
	}

    void ProcessKeyAction()
    {
		if (!IsAI)
		{
			isMoving = false;
			isForwarding = false;
			if (Input.GetKey(KeyCode.Alpha1))
			{
				if (TailCamera != null)
				{
					TailCamera.enabled = false;
				}
				FireCamera.enabled = true;
			}
			else if (Input.GetKey(KeyCode.Alpha2))
			{
				if (TailCamera != null)
				{
					TailCamera.enabled = true;
				}
				FireCamera.enabled = false;
			}
			if (Input.GetKey(KeyCode.W))
			{
				Forward();
			}
			else if (Input.GetKey(KeyCode.S))
			{
				Backward();
			}
			if (Input.GetKey(KeyCode.UpArrow))
			{
				Forward();
			}
			else if (Input.GetKey(KeyCode.DownArrow))
			{
				Backward();
			}
			if (Input.GetKey(KeyCode.A))
			{
				//Tires.TurnLeft();
				SteelLeft();
			}
			else if (Input.GetKey(KeyCode.D))
			{
				//Tires.TurnRight();
				SteelRight();
			}
			else
			{
				Tires.Restore();
				RestoreCamera();
			}
		}
    }

	public void Forward()
	{
		Tires.Forward();
		isMoving = true;
		isForwarding = true;
		transform.Translate(0, 0, Speed);
	}
	public void Backward()
	{
		Tires.Backward();
		isMoving = true;
		isForwarding = false;
		transform.Translate(0, 0, -Speed);
	}
	public void SteelLeft()
	{
		if (isMoving || IsTank)
		{
            float angle = -RotSpeed * (isForwarding || !isMoving ? 1 : -1);
            Steel(angle);
        }
	}

	public void SteelRight()
	{
		if (isMoving || IsTank)
		{
            float angle = RotSpeed * (isForwarding || !isMoving ? 1 : -1);
            Steel(angle);
        }
	}

    private void Steel(float angle)
    {
		if (isForwarding)
		{
			if (angle < 0)
			{
				Tires.TurnLeft();
			}
			else if (angle > 0)
			{
				Tires.TurnRight();
			}
		}
		else
		{
			if (angle < 0)
			{
				Tires.TurnRight();
			}
			else if (angle > 0)
			{
				Tires.TurnLeft();
			}
		}
		if (TailPos != null)
        {
            transform.RotateAround(TailPos.transform.position, Vector3.up, angle);
        }
        else
        {
            transform.Rotate(Vector3.up, angle);
        }
		if (IsAntiTurn)
        {
            ProcessMouseAction(-angle, 0);
        }
        TurnCamera(angle);
    }
    private void RestoreCamera()
    {
        float angle = 0f;
        if (Mathf.Abs(curtDist) > cameraDistUnit)
        {
            if (curtDist > 0)
            {
                angle = -1f;
            }
            else if (curtDist < 0)
            {
                angle = 1f;
            }
            TurnCamera(angle);
        }
    }
    private void TurnCamera(float angle)
    {
        if (TailCamera != null && angle != 0)
        {
            float dist = 0f;
            if (angle > 0)
            {
                dist = cameraDistUnit;
            }
            else
            {
                dist = -cameraDistUnit;
            }
            curtDist += dist;
            if (Mathf.Abs(curtDist) < Mathf.Abs(maxCameraDist))
            {
                TailCamera.transform.Translate(dist, 0, 0);
            }
            else
            {
                curtDist -= dist;
            }
        }
    }
}
