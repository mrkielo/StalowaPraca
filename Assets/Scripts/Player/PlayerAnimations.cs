using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
	Vector3 mousePos;
	PlayerMovement m;
	[SerializeField] string[] animParams;
	Animator animator;
	void Start()
	{
		animator = GetComponent<Animator>();
		m = GetComponent<PlayerMovement>();
	}

	// Update is called once per frame
	void Update()
	{
		if (mousePos.x > transform.position.x) FaceRight();
		else FaceLeft();


		mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		if (m.mx == 0 && m.my == 0)
		{
			if (mousePos.y > transform.position.y) Switcher("IdleUp");
			else Switcher("IdleDown");
		}
		else
		{
			if (mousePos.y > transform.position.y) Switcher("WalkUp");
			else Switcher("WalkDown");
		}





	}

	void FaceLeft()
	{
		transform.localScale = new Vector2(1, 1);
	}

	void FaceRight()
	{
		transform.localScale = new Vector2(-1, 1);
	}

	void Switcher(string name)
	{
		if (animator.GetBool(name) == true) return;

		foreach (string n in animParams)
		{
			animator.SetBool(n, false);
		}

		animator.SetBool(name, true);
	}
}
