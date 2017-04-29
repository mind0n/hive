using UnityEngine;
using System.Collections;

public class G : MonoBehaviour {

	public int resolution = 10;

	private int currentResolution;
	private ParticleSystem.Particle[] points;

	void Start()
	{
		CreatePoints();
	}

	private void CreatePoints()
	{
		currentResolution = resolution;
		points = new ParticleSystem.Particle[resolution];
		float increment = 1f / (resolution - 1);
		for (int i = 0; i < resolution; i++)
		{
			float x = i * increment;
			points[i].position = new Vector3(x, 0f, 0f);
			points[i].color = new Color(x, 0f, 0f);
			points[i].size = 0.1f;
		}
	}

	void Update()
	{
		if (currentResolution != resolution)
		{
			CreatePoints();
		}
		particleSystem.SetParticles(points, points.Length);
	}
}