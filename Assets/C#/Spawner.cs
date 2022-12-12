using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	public void Spawn(GameObject[] enemies)
	{
		Instantiate(enemies[Random.Range(0, enemies.Length)], transform.position, transform.rotation);
	}
}
