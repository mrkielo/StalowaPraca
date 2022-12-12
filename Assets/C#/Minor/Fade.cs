using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
	CanvasGroup cg;
	void Start()
	{
		cg = GetComponent<CanvasGroup>();
		if (cg == null)
		{
			Debug.Log("No CanvasGroup in GameObj!");
		}
	}

	public void StartIn(float time)
	{
		StartCoroutine(In(time));
	}

	public void StartOut(float time)
	{
		StartCoroutine(Out(time));
	}

	IEnumerator In(float time)
	{
		float current = 0;
		float target = 1;
		while (current < target)
		{
			cg.alpha = Mathf.Lerp(cg.alpha, target, current / target);
			current += Time.deltaTime;
			yield return null;
		}
	}

	IEnumerator Out(float time)
	{
		float current = 1;
		float target = 0;
		while (current < target)
		{
			cg.alpha = Mathf.Lerp(cg.alpha, target, current / target);
			current -= Time.deltaTime;
			yield return null;
		}
	}
}
