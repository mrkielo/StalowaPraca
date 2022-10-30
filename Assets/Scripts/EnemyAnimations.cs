using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{
	Rigidbody2D rb;
	Animator animator;
	float xScale;
	float yScale;
	[SerializeField] string[] animParams;

	void Start()
	{
		xScale = transform.localScale.x;
		yScale = transform.localScale.y;
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
	}


	void Update()
	{
		if (rb.velocity.x > 0)
		{
			FaceRight();
			if (rb.velocity.y > 0)
			{
				Switcher("WalkUp");
			}
			else
			{
				if (-rb.velocity.y > rb.velocity.x)
				{
					Switcher("WalkFront");
				}
				else
				{
					Switcher("WalkSide");
				}
			}
		}
		else if (rb.velocity.x <= 0)
		{
			FaceLeft();
			if (rb.velocity.y > 0)
			{
				Switcher("WalkUp");
			}
			else
			{
				if (-rb.velocity.y > -rb.velocity.x)
				{
					Switcher("WalkFront");
				}
				else
				{
					Switcher("WalkSide");
				}
			}

		}

		if (rb.velocity.x == 0 && rb.velocity.y == 0)
		{
			Switcher("Idle");
		}
	}

	void FaceRight()
	{
		transform.localScale = new Vector2(xScale, yScale);
	}
	void FaceLeft()
	{
		transform.localScale = new Vector2(-xScale, yScale);
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
