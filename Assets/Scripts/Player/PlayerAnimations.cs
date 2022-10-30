using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
	Vector3 mousePos;
	PlayerMovement m;
	[SerializeField] string[] animParams;
	Animator animator;
	PlayerShooting playerShooting;
	void Start()
	{
		animator = GetComponent<Animator>();
		m = GetComponent<PlayerMovement>();
		playerShooting = GetComponent<PlayerShooting>();
	}

	// Update is called once per frame
	void Update()
	{
		if (mousePos.x > transform.position.x) FaceRight();
		else FaceLeft();


		mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		if (m.mx == 0 && m.my == 0)
		{
			if (mousePos.y > transform.position.y)
			{
				Switcher("IdleUp");
				SortingLay(2);
			}
			else
			{
				Switcher("IdleDown");
				SortingLay(0);
			}

		}
		else
		{
			if (mousePos.y > transform.position.y)
			{
				Switcher("WalkUp");
				SortingLay(2);
			}
			else
			{
				Switcher("WalkDown");
				SortingLay(0);
			}
		}





	}

	void FaceLeft()
	{
		transform.localScale = new Vector2(1, 1);
		playerShooting.bow.localScale = new Vector2(-1, 1);
	}

	void FaceRight()
	{
		transform.localScale = new Vector2(-1, 1);
		playerShooting.bow.localScale = new Vector2(1, -1);

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

	void SortingLay(int layer)
	{
		GetComponent<SpriteRenderer>().sortingOrder = layer;
	}
}
