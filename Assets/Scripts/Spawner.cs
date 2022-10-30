using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	[SerializeField] GameObject[] enemies;
	float lastSpawn;
	[SerializeField] float delay;
	[SerializeField] float size;

	private void Start()
	{
		lastSpawn = Time.time;
	}

	private void Update()
	{
		if (lastSpawn + delay < Time.time && !isPlayerClose() && isPlayerTooFar())
		{
			Spawn();
			lastSpawn = Time.time;
		}
	}

	bool isPlayerClose()
	{
		return Physics2D.OverlapBox(transform.position, new Vector2(size * 4, size * 4), 0, LayerMask.GetMask("Player"));
	}

	bool isPlayerTooFar()
	{
		return Physics2D.OverlapBox(transform.position, new Vector2(20, 20), 0, LayerMask.GetMask("Player"));
	}

	void Spawn()
	{
		Vector2 pos = new Vector2(transform.position.x + Random.Range(-size / 2, size / 2), transform.position.y + Random.Range(-size / 2, size / 2));
		Instantiate(enemies[Random.Range(0, enemies.Length)], pos, transform.rotation);
	}
}
