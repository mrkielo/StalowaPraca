using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
	[SerializeField] bool poison;
	private void OnTriggerEnter2D(Collider2D other)
	{
		Player player = other.GetComponent<Player>();
		if (player != null)
		{
			if (poison)
			{
				player.poison = true;
			}
			else
			{
				player.eye = true;
			}
			Destroy(gameObject);
		}
	}
}
