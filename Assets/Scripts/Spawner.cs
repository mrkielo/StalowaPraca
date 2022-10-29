using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	[SerializeField] GameObject[] enemies;
	float lastSpawn;
	[SerializeField] float delay;

	private void Start()
	{
		lastSpawn = Time.time;
	}

	private void Update()
	{
		if (lastSpawn + delay < Time.time)
		{
			Spawn();
			lastSpawn = Time.time;
		}
	}

	void Spawn()
	{
		Vector2 pos = new Vector2(transform.position.x + Random.Range(-10, 10), transform.position.y + Random.Range(-10, 10));
		Instantiate(enemies[Random.Range(0, enemies.Length)], pos, transform.rotation);
	}
}
