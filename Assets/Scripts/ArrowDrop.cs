using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowDrop : MonoBehaviour
{
	[SerializeField] int drop;
	private void OnTriggerEnter2D(Collider2D other)
	{
		Player player = other.GetComponent<Player>();
		if (player != null)
		{
			player.ammo += drop;
			Destroy(gameObject);
		}
	}
}
