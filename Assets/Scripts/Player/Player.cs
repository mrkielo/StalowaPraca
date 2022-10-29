using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
	[SerializeField] float maxHp;
	[SerializeField] float maxStamina;
	[SerializeField] public float dmg;
	[HideInInspector] public float hp;
	[HideInInspector] public float stamina;
	[SerializeField] public float ammo;
	[Header("UI")]
	[SerializeField] Text hpText;
	[SerializeField] Text staminaText;
	[SerializeField] Text ammoText;

	void Start()
	{
		hp = maxHp;
		stamina = maxStamina;
		UpdateHud();
	}

	void Update()
	{

	}

	public void UpdateHud()
	{
		hpText.text = hp.ToString();
		staminaText.text = stamina.ToString();
		ammoText.text = ammo.ToString();
	}


	public void TakeDamage(float damage)
	{
		hp -= damage;
		UpdateHud();
	}
}
