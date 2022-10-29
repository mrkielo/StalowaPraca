using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : MonoBehaviour
{

	Player player;
	Rigidbody2D rb;

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
		rb = GetComponent<Rigidbody2D>();
		player = FindObjectOfType<Player>();
		if (player == null) Debug.Log("Gracza nie ma");
	}

	private void Update()
	{
		if (isPlayerinAttackRange() && !isPlayerTooClose())
		{
			Stop();
			if (lastAttack + attackInterval < Time.time) Attack();
		}

		if (isPlayerTooClose())
		{
			Escape();
		}

		if (isPlayerinSight() && !isPlayerinAttackRange())
		{
			//kill him
			ChasePlayer();
		}
		if (!isPlayerinSight())
		{
			//idk znik
			Patrol();
		}


	}
	private void OnDrawGizmos()
	{
		Gizmos.DrawSphere(transform.position, detectRadius);
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

	void ChasePlayer()
	{

		rb.velocity = (player.transform.position - transform.position).normalized * mSpeed * Time.deltaTime;
	}

	void Escape()
	{
		Debug.Log("IM ESCAPINGGG");
		rb.velocity = (transform.position - player.transform.position).normalized * 2 * mSpeed * Time.deltaTime;

	}

	void Patrol()
	{
		rb.velocity = Vector2.zero;
	}

	void Stop()
	{
		rb.velocity = Vector2.zero;
	}

	void Attack()
	{
		//shooot
		Vector2 shootDir = (player.transform.position - transform.position).normalized;
		Quaternion rot = Quaternion.Euler(0, 0, Mathf.Atan2(shootDir.y, shootDir.x) * Mathf.Rad2Deg);
		GameObject fireBall = Instantiate(projectilePrefab, transform.position, rot);
		fireBall.GetComponent<Rigidbody2D>().AddForce(shootDir * bulletSpeed);
		fireBall.GetComponent<EnemyFireball>().dmg = dmg;

		lastAttack = Time.time;
	}


}
