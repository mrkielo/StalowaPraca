using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNavi : MonoBehaviour
{
	[SerializeField] Image[] imgs;
	void Update()
	{
		bool bonfireCheck = Physics2D.OverlapCircle(transform.position, 20f, LayerMask.GetMask("Bonfire"));
		bool closeCheck = Physics2D.OverlapCircle(transform.position, 5f, LayerMask.GetMask("Bonfire"));

		Collider2D bonfire = Physics2D.OverlapCircle(transform.position, 20f, LayerMask.GetMask("Bonfire"));
		if (!bonfireCheck || closeCheck)
		{
			foreach (Image img in imgs)
			{
				img.enabled = false;
			}
		}
		else
		{
			Vector2 dir = transform.position - bonfire.transform.position;
			float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
			if (angle > -22.5 && angle < 22.5) Switcher(0);
			if (angle >= 22.5 && angle < 67.5) Switcher(1);
			if (angle >= 67.5 && angle < 112.5) Switcher(2);
			if (angle >= 112.5 && angle < 157.5) Switcher(3);
			if (angle >= 157.5 && angle < -157.5) Switcher(4);
			if (angle >= -157.5 && angle < -112.5) Switcher(5);
			if (angle >= -112.5 && angle < -67.5) Switcher(6);
			if (angle >= -67.5 && angle < -22.5) Switcher(7);
		}
	}


	void Switcher(int index)
	{
		foreach (Image img in imgs)
		{
			img.enabled = false;
		}
		imgs[index].enabled = true;
	}
}
