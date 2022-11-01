using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cinemachine;

public class Player : MonoBehaviour
{
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
	[SerializeField] Image hpBar;
	[SerializeField] Text ammoText;
	[SerializeField] Image iceBar;
	[SerializeField] Image iceFilter;
	[SerializeField] Image poisonImg;
	[SerializeField] Image eyeImg;
	[SerializeField] Image hitFilter;
	[SerializeField] float hitFilterTime;

	public bool eye = false;
	public bool poison = false;

	public bool bonfire = false;

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
		Ice();
		ifWin();

		if (hp <= 0) SceneManager.LoadScene(3);
		if (poison) poisonImg.enabled = true;
		if (eye) eyeImg.enabled = true;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log("TrrigerEnter");
		if (other.gameObject.tag == "Cam")
		{
			Debug.Log("if enter");
			camtarget.AddMember(other.gameObject.transform, 1, 1);
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
		hpBar.fillAmount = hp / maxHp;
		ammoText.text = ammo.ToString();
		iceBar.fillAmount = (iceTime - ice) / iceTime;
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
		if (ice > 0) iceFilter.enabled = false;
		else iceFilter.enabled = true;
	}

	bool BonfireCheck()
	{
		return Physics2D.OverlapCircle(transform.position, 3, LayerMask.GetMask("Bonfire"));
	}

	void ifWin()
	{
		Debug.Log("ifwin");
		Debug.Log(eye);
		Debug.Log("poison" + poison);
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
		hitFilter.enabled = true;
		yield return new WaitForSeconds(hitFilterTime);
		hitFilter.enabled = false;
	}

}





