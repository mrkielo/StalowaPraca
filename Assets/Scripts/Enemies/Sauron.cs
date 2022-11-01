using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sauron : MonoBehaviour
{
	Player player;
	//stats

	[SerializeField] Image hpBar;
	[SerializeField] Collider2D trigger;
	[SerializeField] Transform arm;

	[SerializeField] GameObject mobSpawner;
	bool start = false;

	[SerializeField] float shootTime;
	[SerializeField] float spawnTime;
	float startTime;
	Enemy enemy;
	float angle;

	private void Start()
	{
		startTime = Time.time;
		player = FindObjectOfType<Player>();
		if (player == null) Debug.Log("Gracza nie ma");

		enemy = GetComponent<Enemy>();
		if (enemy == null) Debug.Log("null");
	}

	IEnumerator Rot()
	{
		angle += 0.1f;
		yield return new WaitForSeconds(0.25f);
	}




	void Update()
	{

		hpBar.fillAmount = enemy.hp / enemy.maxHp;

		if (trigger.IsTouchingLayers(LayerMask.GetMask("Player")))
		{
			start = true;
			StartCoroutine(Rot());
		}
		if (start)
		{
			if (startTime + shootTime > Time.time)
			{
				//shooot
				arm.gameObject.SetActive(true);
				arm.transform.rotation = Quaternion.Euler(0, 0, angle);
			}

			else if (startTime + shootTime + spawnTime > Time.time)
			{
				//spawn
				arm.gameObject.SetActive(false);
				mobSpawner.SetActive(true);
			}
			else
			{
				mobSpawner.SetActive(false);
				startTime = Time.time;
			}
		}

	}





}
