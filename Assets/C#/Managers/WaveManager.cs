using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class WaveManager : MonoBehaviour
{
	[Header("Spawners")]
	[SerializeField] Transform[] gates;
	[SerializeField] Transform bossGate;
	[Header("Enemies")]
	[SerializeField] int[] enemiesQty;
	[Serializable] //Enemies spawning rules
	public class WaveEnemies
	{
		public GameObject[] enemy;
		public int to;
	}
	public WaveEnemies[] waves;


	[Header("UI")]
	[SerializeField] Text waveText;
	[SerializeField] GameObject shopUI;
	[SerializeField] float shopTime;
	int enemiesLeft;
	int currentWave;

	void Start()
	{
		currentWave = 1;
		enemiesLeft = enemiesQty[currentWave];
		waveText.enabled = false;
		StartCoroutine(Wave(currentWave));
	}

	void Update()
	{
		if (enemiesLeft <= 0)
		{
			currentWave++;
			enemiesLeft = enemiesQty[currentWave];
			StartCoroutine(Wave(currentWave));
		}
	}

	public void EnemySpawned(int qty = 1)
	{
		enemiesLeft += qty;
	}

	public void EnemyDown()
	{
		enemiesLeft--;
	}

	IEnumerator Wave(int index)
	{
		Debug.Log("Wave cor");
		yield return StartCoroutine(Shopping());
		StartCoroutine(EnemySpawning());


	}

	IEnumerator Shopping()
	{
		Debug.Log("Shopping start");
		//shopping in
		shopUI.SetActive(true);
		yield return new WaitForSeconds(shopTime);
		shopUI.SetActive(false);
		Debug.Log("Shopping finish");
	}

	IEnumerator EnemySpawning()
	{
		Debug.Log("EnemySpawning cor");
		int enemyArrayIndex = 0;
		StartCoroutine(WaveText());
		for (int i = 0; i < waves.Length - 1; i++)
		{
			if (waves[i].to > currentWave)
			{
				enemyArrayIndex = i - 1;
				Debug.Log("enemy array index: " + enemyArrayIndex);
				break;
			}
			Debug.Log("i: " + i);
		}
		int subWaves = UnityEngine.Random.Range(1, 4);
		int m = 1;
		for (int i = 1; i <= enemiesQty[currentWave]; i++)
		{
			int enemyIndex = UnityEngine.Random.Range(0, waves[enemyArrayIndex].enemy.Length);
			int gateIndex = UnityEngine.Random.Range(0, gates.Length);
			Instantiate(waves[enemyArrayIndex].enemy[enemyIndex], gates[gateIndex].position, gates[gateIndex].rotation);
			if (enemiesQty[currentWave] / subWaves * m == i)
			{
				yield return new WaitForSeconds(2f);
				m++;
			}
		}
		yield return null;
	}


	IEnumerator WaveText()
	{
		waveText.text = "Wave " + currentWave;
		waveText.enabled = true;
		waveText.gameObject.GetComponent<Fade>().StartIn(1);
		yield return new WaitForSeconds(2f);
		waveText.gameObject.GetComponent<Fade>().StartOut(1);

		waveText.enabled = false;

	}

	int Search(WaveEnemies enemy, WaveEnemies[] tab)
	{
		//if not found returns -1
		//otherwise returns index
		for (int i = 0; i < tab.Length; i++)
		{
			if (enemy.enemy == tab[i].enemy)
			{
				return i;
			}
		}
		return -1;
	}


	public void ArrayPush<T>(ref T[] table, object value)
	{
		Array.Resize(ref table, table.Length + 1); // Resizing the array for the cloned length (+-) (+1)
		table.SetValue(value, table.Length - 1); // Setting the value for the new element
	}





}
