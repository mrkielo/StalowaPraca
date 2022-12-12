using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
	Player player;
	Rigidbody2D rb;

	//stats
	[SerializeField] float detectRadius;
	[SerializeField] float attackRadius;
	[SerializeField] float spread;
	[SerializeField] float dmg;
	[SerializeField] float mSpeed;
	[SerializeField] float attackInterval;
	[SerializeField] float bulletSpeed;
	[SerializeField] GameObject projectilePrefab;
	EnemyMovement movement;

	float lastAttack;

	private void Start()
	{
		movement = GetComponent<EnemyMovement>();
		rb = GetComponent<Rigidbody2D>();
		player = FindObjectOfType<Player>();
		if (player == null) Debug.Log("Gracza nie ma");
	}

	private void Update()
	{
		if (!GetComponent<Enemy>().hitShock)
		{
			if (isPlayerinSight())
			{
				if (isPlayerinAttackRange())
				{
					Shoot();
				}
				else
				{
					movement.Chase(player.transform.position, mSpeed);
				}
			}
			else
			{
				movement.Patrol(player.transform.position, mSpeed);
			}
		}
		else lastAttack = Time.time;
	}


	bool isPlayerinSight()
	{
		if (Vector2.Distance(player.transform.position, transform.position) < detectRadius)
			return true;
		else
			return false;
	}

	bool isPlayerinAttackRange()
	{
		if (Vector2.Distance(player.transform.position, transform.position) < attackRadius)
			return true;
		else
			return false;
	}
	void Attack(float angleMod = 0)
	{
		//shooot
		Vector2 shootDir = (player.transform.position - transform.position).normalized;
		//multishoot
		Quaternion rot = Quaternion.Euler(0, 0, Mathf.Atan2(shootDir.y, shootDir.x) * Mathf.Rad2Deg + angleMod);



		GameObject fireBall = Instantiate(projectilePrefab, transform.position, rot);
		fireBall.GetComponent<Rigidbody2D>().AddForce(shootDir.Rotate(angleMod) * bulletSpeed);
		fireBall.GetComponent<EnemyFireball>().dmg = dmg;

		lastAttack = Time.time;
	}

	void Shoot()
	{
		if (lastAttack + attackInterval < Time.time)
		{
			Attack(-2 * spread);
			Attack(-spread);
			Attack(0);
			Attack(spread);
			Attack(2 * spread);

			lastAttack = Time.time;
		}
	}


}
public static class Vector2Extension
{

	public static Vector2 Rotate(this Vector2 v, float degrees)
	{
		float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
		float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

		float tx = v.x;
		float ty = v.y;
		v.x = (cos * tx) - (sin * ty);
		v.y = (sin * tx) + (cos * ty);
		return v;
	}
}
