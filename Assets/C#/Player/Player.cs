using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cinemachine;

[SelectionBase]
public class Player : MonoBehaviour
{
	[SerializeField] HudManager hud;
	[SerializeField] CinemachineTargetGroup camtarget;
	[SerializeField] float maxHp;
	[SerializeField] float maxStamina;
	[SerializeField] public float dmg;
	[HideInInspector] public float hp;
	[HideInInspector] public float stamina;
	[SerializeField] public float ammo;
	[SerializeField] float iceTime;
	[SerializeField] float iceDps;
	float ice;

	[Header("UI")]
	[SerializeField] float hitFilterTime;
	[Header("Cheats")]
	[SerializeField] bool godMode;

	[HideInInspector] public bool eye = false;
	[HideInInspector] public bool poison = false;
	[HideInInspector] public bool bonfire = false;

	float lastIce;

	void Start()
	{
		ice = iceTime;
		hp = maxHp;
		stamina = maxStamina;
		UpdateHud();
	}

	void Update()
	{
		if (!godMode) Ice();
		ifWin();

		if (hp <= 0 && !godMode) SceneManager.LoadScene(3);
		if (poison) hud.poisonImg.enabled = true;
		if (eye) hud.eyeImg.enabled = true;
	}

	//CAM Group

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Cam")
		{
			camtarget.AddMember(other.gameObject.transform, 1, 2);
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.tag == "Cam")
		{
			camtarget.RemoveMember(other.gameObject.transform);
		}
	}

	public void UpdateHud()
	{
		hud.hpBar.fillAmount = hp / maxHp;
		hud.ammoText.text = ammo.ToString();
		hud.iceBar.fillAmount = (iceTime - ice) / iceTime;
	}


	public void TakeDamage(float damage)
	{
		StartCoroutine(HitFilter());
		hp -= damage;
		UpdateHud();
	}

	void Ice()
	{
		if (lastIce + 0.25 < Time.time)
		{
			UpdateHud();
			if (BonfireCheck() && hp < maxHp)
			{
				hp += 0.5f;
			}

			if (BonfireCheck() && ice < iceTime)
			{
				ice += 0.5f;

			}
			else
			{
				if (ice > 0)
				{
					ice -= 0.25f;
				}
				else
				{
					hp -= iceDps / 4;
				}
			}
			lastIce = Time.time;

		}
		if (ice > 0) hud.iceFilter.enabled = false;
		else hud.iceFilter.enabled = true;
	}

	bool BonfireCheck()
	{
		return Physics2D.OverlapCircle(transform.position, 3, LayerMask.GetMask("Bonfire"));
	}

	void ifWin()
	{
		if (poison && eye && Physics2D.OverlapCircle(transform.position, 3, LayerMask.GetMask("Cross")))
		{
			Win();
		}
	}

	void Win()
	{
		SceneManager.LoadScene(2);
	}

	IEnumerator HitFilter()
	{
		hud.hitFilter.enabled = true;
		yield return new WaitForSeconds(hitFilterTime);
		hud.hitFilter.enabled = false;
	}

}





