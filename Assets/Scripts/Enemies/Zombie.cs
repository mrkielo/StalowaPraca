using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
	Player player;
	Rigidbody2D rb;

	//stats
	[SerializeField] float detectRadius;
	[SerializeField] float attackRadius;
	[SerializeField] float dmg;
	[SerializeField] float mSpeed;
	[SerializeField] float attackInterval;

	float lastAttack;

	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		player = FindObjectOfType<Player>();
		if (player == null) Debug.Log("Gracza nie ma");
	}

	private void Update()
	{
		if (isPlayerinAttackRange() && lastAttack + attackInterval < Time.time)
		{
			Attack();
		}

		if (isPlayerinSight())
		{
			//kill him
			ChasePlayer();
		}
		else
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

	void ChasePlayer()
	{

		rb.velocity = (player.transform.position - transform.position).normalized * mSpeed * Time.deltaTime;
	}

	void Patrol()
	{
		rb.velocity = Vector2.zero;
	}

	void Attack()
	{
		player.TakeDamage(dmg);
		lastAttack = Time.time;
	}


}
