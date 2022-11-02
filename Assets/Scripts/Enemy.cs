using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	[SerializeField] public float maxHp;
	[HideInInspector] public float hp;
	[SerializeField] GameObject[] drop;

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
		if (drop != null) Instantiate(drop[Random.Range(0, drop.Length - 1)], transform.position, transform.rotation);
		Destroy(gameObject);
	}

}
