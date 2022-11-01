using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ThreeHead : MonoBehaviour
{
	Player player;
	//stats

	[SerializeField] float dmg;
	[SerializeField] float attackInterval;
	[SerializeField] float bulletSpeed;
	[SerializeField] GameObject projectilePrefab;
	[SerializeField] Image hpBar;
	[SerializeField] Image ammoBar;
	[SerializeField] Transform[] firepoints;
	[SerializeField] Collider2D trigger;
	[SerializeField] GameObject mobSpawner;
	bool start = false;
	bool canShoot = true;

	float lastAttack;
	Enemy enemy;

	private void Start()
	{
		player = FindObjectOfType<Player>();
		if (player == null) Debug.Log("Gracza nie ma");
		lastAttack = Time.time;
		enemy = GetComponent<Enemy>();
		if (enemy == null) Debug.Log("null");
	}


	void Update()
	{
		hpBar.fillAmount = enemy.hp / enemy.maxHp;

		if (trigger.IsTouchingLayers(LayerMask.GetMask("Player")))
		{
			start = true;
		}
		if (ammoBar.fillAmount == 0)
		{
			canShoot = false;
			mobSpawner.SetActive(true);
			StartCoroutine(Fill());
		}

		if (start)
		{
			if (lastAttack + attackInterval < Time.time && canShoot)
			{
				mobSpawner.SetActive(false);
				Attack(firepoints[Random.Range(0, 3)]);
			}

		}

	}


	IEnumerator Fill()
	{

		while (ammoBar.fillAmount < 1)
		{
			ammoBar.fillAmount += 0.1f;
			yield return new WaitForSeconds(1f);
		}
		canShoot = true;
	}

	void Attack(Transform firepoint)
	{
		//shooot
		Vector2 shootDir = (player.transform.position - firepoint.position).normalized;
		Quaternion rot = Quaternion.Euler(0, 0, Mathf.Atan2(shootDir.y, shootDir.x) * Mathf.Rad2Deg);
		GameObject fireBall = Instantiate(projectilePrefab, firepoint.position, rot);
		fireBall.GetComponent<Rigidbody2D>().AddForce(shootDir * bulletSpeed);
		fireBall.GetComponent<EnemyFireball>().dmg = dmg;

		lastAttack = Time.time;
		ammoBar.fillAmount -= 0.01f;
	}
}

