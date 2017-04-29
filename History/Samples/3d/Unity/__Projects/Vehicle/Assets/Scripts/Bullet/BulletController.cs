using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BulletController : MonoBehaviour {
	public float BulletSpeed = 1f;
	public float BulletGravity = 0.1f;
	//public int ParticlePoints = 10;
	List<BulletBehavior> bullets = new List<BulletBehavior>();
	List<BulletBehavior> cached = new List<BulletBehavior>();
	GameObject template;
	//ParticleSystem.Particle[] points;
	public void Fire(string name)
	{
		if (cached.Count < 1)
		{
			BulletBehavior b = (BulletBehavior)Instantiate(template.GetComponent("BulletBehavior"));
			b.renderer.enabled = false;
			//GameBullet b = new GameBullet { Model = o, Gravity = BulletGravity, Speed = BulletSpeed };
			b.Reset();
			b.renderer.enabled = false;
			b.transform.position = transform.position;
			b.transform.rotation = transform.rotation;
			b.OnRangeExceeded += b_OnRangeExceeded;
			bullets.Add(b);
		}
		else
		{
			BulletBehavior b = cached[0];
			cached.RemoveAt(0);
			b.Reset();
			b.renderer.enabled = false;
			b.transform.position = transform.position;
			b.transform.rotation = transform.rotation;
			bullets.Add(b);
		}
	}

	void b_OnRangeExceeded(BulletBehavior b)
	{
		bullets.Remove(b);
		cached.Add(b);
		b.Reset();

	}
	// Use this for initialization
	void Start () {
		template = GameObject.Find("BulletB");
		template.renderer.enabled = false;
		//points = new ParticleSystem.Particle[10];
	}
	
	// Update is called once per frame
	void Update () {
		for (int l=bullets.Count, i = 0; i < l; i++)
		{
			if (i < bullets.Count && bullets[i].IsShooted)
			{
				bullets[i].Move();
			}
			else
			{
				break;
			}
		}
	}

}
