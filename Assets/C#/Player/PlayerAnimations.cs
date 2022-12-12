using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
	Vector3 mousePos;
	PlayerMovement m;
	Animator animator;
	PlayerShooting playerShooting;
	string lastSwitch = "";
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
		if (lastSwitch == name) return;
		animator.Play(name, 0);
		lastSwitch = name;
	}

	void SortingLay(int layer)
	{
		GetComponent<SpriteRenderer>().sortingOrder = layer;
	}
}
