using UnityEngine;
using System.Collections;

public class BulletBehavior : MonoBehaviour {


	public float ElapsedMs;
	public float Gravity;
	public float Speed;
	public bool IsShooted;
	int resolution = 50;
	ParticleSystem.Particle[] points = new ParticleSystem.Particle[100];
	float distance;
	float maxDistance = 1200;
	public delegate void DestroyHandler(BulletBehavior b);
	public event DestroyHandler OnRangeExceeded;
	public void Reset()
	{
		IsShooted = true;
		distance = 0;
		renderer.enabled = false;
		collider.isTrigger = true;
		if (particleSystem != null)
		{
			//Model.particleSystem.Play();
			float increment = 0.1f;
			float offset = -1f;
			for (int i = 0; i < resolution; i++)
			{
				float z = i * increment + offset;
				float isize = 0.3f;// / i;
				if (isize < 0.01f)
				{
					isize = 0.01f;
				}
				points[i].position = new Vector3(0f, 0f, z);
				points[i].color = new Color(247, 182, 122, 255);
				points[i].size = isize;
			}
		}
	}
	public void Move()
	{
		if (IsShooted && distance < maxDistance)
		{
			if (distance > 5)
			{
				collider.isTrigger = false;
			}
			//Model.renderer.enabled = true;
			particleSystem.SetParticles(points, points.Length);
			transform.Translate(0, 0, Speed);
			transform.Translate(0, -Gravity * ElapsedMs, 0, Space.World);
			ElapsedMs = Time.deltaTime;
			distance += Speed;
			//if (Model.particleSystem != null && !Model.particleSystem.isPlaying)
			//{
			//	Model.particleSystem.Play();
			//}
		}
		else
		{
			particleSystem.Clear();
			IsShooted = false;
			if (OnRangeExceeded != null)
			{
				OnRangeExceeded(this);
			}
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnCollisionEnter(Collision collision)
	{
		Debug.Log(collision.collider.name + " - bullet hit - " + collision.contacts.Length);
		particleSystem.Stop();
		particleSystem.Clear();
		if (OnRangeExceeded != null)
		{
			OnRangeExceeded(this);
		}
	}

	void OnCollisionExit(Collision collision)
	{
		Debug.Log(collision.collider.name + " - bullet through");
	}
}
