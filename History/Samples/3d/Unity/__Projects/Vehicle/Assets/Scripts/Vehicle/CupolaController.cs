using UnityEngine;
using System.Collections;
using System;

public class CupolaController : MonoBehaviour {
    public GameObject Cannon;
    public GameObject LinkPos;
	public BulletController BulletPos;
	public VehicleController Vehicle;
	public bool IsSingleFire = true;
	public int CoolDownMs = 400;
	bool isFired;
	DateTime lastFireTime = DateTime.Now;
    public void Hold(float angle)
    {
        if (Cannon != null && LinkPos != null)
        {
            float curtAngle = Cannon.transform.eulerAngles.x;
            if ((angle > 0 && (curtAngle <= 10 || curtAngle > 270)) || (angle < 0 && (curtAngle >= 300 || curtAngle < 90)))
            {
                Cannon.transform.RotateAround(LinkPos.transform.position, LinkPos.transform.right, angle);
            }
        }
    }
    public void Turn(float angle)
    {
        transform.Rotate(Vector3.up, angle);
    }
	public void ProcessFire()
	{
		if (Vehicle != null && !Vehicle.IsAI)
		{
			if (Input.GetMouseButton(0))
			{
				Fire(0);
			}
			if (Input.GetMouseButtonUp(0))
			{
				ResetFire();
			}
		}
	}
	public void Fire(int type = 0)
	{
		if (!IsSingleFire)
		{
			if (DateTime.Now - lastFireTime > TimeSpan.FromMilliseconds(CoolDownMs))
			{
				lastFireTime = DateTime.Now;
				BulletPos.Fire("BulletA");
			}
			else
			{
				//Debug.Log((DateTime.Now - lastFireTime).Milliseconds.ToString());
			}
		}
		else if (!isFired)
		{
			Debug.Log("SingleFire=" + type);
			lastFireTime = DateTime.Now;
		}
		isFired = true;
	}
	public void ResetFire()
	{
		isFired = false;
	}
}
