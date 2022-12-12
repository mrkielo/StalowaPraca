using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	[SerializeField] public float maxHp;
	[HideInInspector] public float hp;
	[SerializeField] GameObject[] drop;
	[SerializeField] float hitShockTime;
	[HideInInspector] public EnemyAnimations animations;
	[HideInInspector] public EnemyMovement movement;
	[HideInInspector] public bool hitShock;
	WaveManager waves;
	private void Start()
	{
		waves = FindObjectOfType<WaveManager>();
		hitShock = false;
		hp = maxHp;
		movement = GetComponent<EnemyMovement>();
		animations = GetComponent<EnemyAnimations>();
	}

	public void TakeDamage(float amount)
	{
		StartCoroutine(DamageCoroutine(amount));
	}

	private void Update()
	{
		if (hp <= 0) Die();
	}

	void Die()
	{
		if (waves != null) waves.EnemyDown();
		int randomIndex = Random.Range(0, drop.Length - 1);

		if (drop != null && drop[randomIndex] != null) Instantiate(drop[randomIndex], transform.position, transform.rotation);
		Destroy(gameObject);
	}

	IEnumerator DamageCoroutine(float amount)
	{
		if (animations) animations.autoSwitch = false;
		if (animations) animations.Switcher("Hit");
		if (movement) movement.Stop();
		if (movement) movement.enabled = false;
		hitShock = true;
		yield return new WaitForSeconds(hitShockTime);
		hp -= amount;
		if (hp <= 0) Die();
		if (animations) animations.autoSwitch = true;
		if (animations) animations.Switcher("IdleSide");
		if (movement) movement.enabled = true;
		hitShock = false;

	}

}
