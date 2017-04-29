using UnityEngine;
using System.Collections;

public class TireController : MonoBehaviour {
	public bool IsKeepTurning;
	public float MaxAngle = 30;
	public GameObject[] TireSpaces = new GameObject[2];
	public GameObject[] TireModels = new GameObject[2];
    public GameObject[] StaticTires = new GameObject[4];

	float defaultAngle = 2f;
	float totalAngle = 0f;
	float defaultRollingSpeed = 5f;

	internal void TurnLeft()
	{
		Turn(-defaultAngle, false);
	}

	private void Turn(float angle, bool isRestore)
	{
		if (Mathf.Abs(totalAngle + angle) <= MaxAngle)
		{
			if (isRestore && Mathf.Abs(totalAngle) < Mathf.Abs(angle))
			{
				angle = -totalAngle;
			}
			totalAngle += angle;
			foreach (GameObject i in TireSpaces)
			{
				i.transform.Rotate(Vector3.up, angle);
			}
		}
	}

	internal void TurnRight()
	{
		Turn(defaultAngle, false);
	}

	internal void Restore()
	{
		if (totalAngle != 0)
		{
			//Turn(-totalAngle);
			if (totalAngle < 0)
			{
				Turn(defaultAngle, true);
			}
			else
			{
				Turn(-defaultAngle, true);
			}
		}
	}

	internal void Forward()
	{
		Roll(defaultRollingSpeed);
	}

	private void Roll(float speed)
	{
		foreach (GameObject i in TireModels)
		{
			i.transform.Rotate(Vector3.right, speed);
		}
        foreach (GameObject i in StaticTires)
        {
            i.transform.Rotate(Vector3.right, speed);
        }
	}

	internal void Backward()
	{
		Roll(-defaultRollingSpeed);
	}
}
