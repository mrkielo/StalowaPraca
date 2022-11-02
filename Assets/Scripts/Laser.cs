using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
	public float dmg;
	float lastHit;
	Player p;
	[SerializeField] Transform startPoint;
	void Start()
	{
		p = FindObjectOfType<Player>();
	}

	private void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
		{
			if (lastHit + 0.1f < Time.time)
			{
				p.TakeDamage(dmg / 10);
				lastHit = Time.time;
			}
		}
	}
}
