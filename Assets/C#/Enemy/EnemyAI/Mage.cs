using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : MonoBehaviour
{

	Player player;
	Rigidbody2D rb;
	EnemyMovement movement;

	//stats
	[SerializeField] float detectRadius;
	[SerializeField] float attackRadius;
	[SerializeField] float escapeRadius;
	[SerializeField] float dmg;
	[SerializeField] float mSpeed;
	[SerializeField] float attackInterval;
	[SerializeField] float bulletSpeed;
	[SerializeField] GameObject projectilePrefab;

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
					if (isPlayerTooClose())
					{
						movement.Escape(player.transform.position, mSpeed * 1.5f);
					}
					else
					{
						movement.Stop();
						//StartCoroutine(movement.Turn(player.transform.position));
						Attack();
					}
				}
				else
				{
					movement.Chase(player.transform.position, mSpeed);
				}
			}
			else
			{
				movement.Patrol(player.transform.position, mSpeed / 2);
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

	bool isPlayerTooClose()
	{
		if (Vector2.Distance(player.transform.position, transform.position) < escapeRadius)
			return true;
		else
			return false;
	}

	void Attack()
	{
		if (lastAttack + attackInterval < Time.time)
		{
			//create fireball
			Vector2 shootDir = (player.transform.position - transform.position).normalized;
			Quaternion rot = Quaternion.Euler(0, 0, Mathf.Atan2(shootDir.y, shootDir.x) * Mathf.Rad2Deg);
			GameObject fireBall = Instantiate(projectilePrefab, transform.position, rot);
			//assign force and dmg
			fireBall.GetComponent<Rigidbody2D>().AddForce(shootDir * bulletSpeed);
			fireBall.GetComponent<EnemyFireball>().dmg = dmg;
			//reset timer
			lastAttack = Time.time;
		}
	}
}
