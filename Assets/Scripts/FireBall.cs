using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
	public float dmg;
	private void OnCollisionEnter2D(Collision2D other)
	{
		Enemy enemy = other.gameObject.GetComponent<Enemy>();
		if (enemy != null)
		{
			enemy.TakeDamage(dmg);
		}

		Destroy(gameObject);
	}
}