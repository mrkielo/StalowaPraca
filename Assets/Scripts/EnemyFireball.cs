using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireball : MonoBehaviour
{
	public float dmg;
	private void OnCollisionEnter2D(Collision2D other)
	{
		Player player = other.gameObject.GetComponent<Player>();
		if (player != null)
		{
			player.TakeDamage(dmg);
		}

		Destroy(gameObject);
	}
}
