using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	[SerializeField] float maxHp;
	[HideInInspector] public float hp;
	[SerializeField] GameObject arrows;

	private void Start()
	{
		hp = maxHp;
	}

	public void TakeDamage(float amount)
	{
		hp -= amount;
	}

	private void Update()
	{
		if (hp <= 0) Die();
	}

	void Die()
	{
		Instantiate(arrows, transform.position, transform.rotation);
		Destroy(gameObject);
	}

}
