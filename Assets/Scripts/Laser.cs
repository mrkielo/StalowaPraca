using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
	public float dmg;
	bool playerIn = false;
	float lastHit;
	Player p;
	[SerializeField] Transform startPoint;
	[SerializeField] Transform parent;
	void Start()
	{
		p = FindObjectOfType<Player>();
	}

	private void Update()
	{

		Vector2 dir = new Vector2(Mathf.Tan(parent.rotation.eulerAngles.z), 1).normalized;
		playerIn = Physics2D.Raycast(startPoint.position, dir, transform.lossyScale.x, LayerMask.GetMask("Player"));
		Debug.DrawRay(parent.position, dir * 100, Color.black);
		if (playerIn && lastHit + 0.5f < Time.time)
		{
			p.TakeDamage(dmg / 2);
			lastHit = Time.time;
		}
	}


}
