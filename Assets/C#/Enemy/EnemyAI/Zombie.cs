using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
	Player player;
	Rigidbody2D rb;
	EnemyMovement movement;

	//stats
	[SerializeField] float detectRadius;
	[SerializeField] float attackRadius;
	[SerializeField] float dmg;
	[SerializeField] float mSpeed;
	[SerializeField] float attackInterval;

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
			if (isPlayerinSight())
			{
				if (isPlayerinAttackRange())
				{
					movement.Stop();
					if (lastAttack + attackInterval < Time.time && !GetComponent<Enemy>().hitShock)
					{
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
				movement.Stop();
			}
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
	void Attack()
	{
		player.TakeDamage(dmg);
		lastAttack = Time.time;
	}


}
