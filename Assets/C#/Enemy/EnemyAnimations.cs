using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{
	static string baseLayer = "Base Layer";
	Animator animator;
	float xScale;
	float yScale;
	string lastSwitch = "";
	Enemy enemy;

	public bool autoSwitch = true;

	void Awake()
	{
		xScale = transform.localScale.x;
		yScale = transform.localScale.y;
		animator = GetComponent<Animator>();
		autoSwitch = true;
		enemy = GetComponent<Enemy>();
	}


	void Update()
	{
		AutoSwitcher();
	}

	public void FaceRight()
	{
		transform.localScale = new Vector2(xScale, yScale);
	}
	public void FaceLeft()
	{
		transform.localScale = new Vector2(-xScale, yScale);
	}

	public void Switcher(string name)
	{
		if (lastSwitch == name) return;
		animator.Play(name, 0);
		lastSwitch = name;
	}

	void AutoSwitcher()
	{
		if (!autoSwitch) return;
		if (enemy.movement.rb.velocity.x > 0)
		{
			FaceRight();
			if (enemy.movement.rb.velocity.y > 0)
			{
				Switcher("WalkUp");
			}
			else
			{
				if (-enemy.movement.rb.velocity.y > enemy.movement.rb.velocity.x)
				{
					Switcher("WalkFront");
				}
				else
				{
					Switcher("WalkSide");
				}
			}
		}
		else if (enemy.movement.rb.velocity.x < 0)
		{
			FaceLeft();
			if (enemy.movement.rb.velocity.y > 0)
			{
				Switcher("WalkUp");
			}
			else
			{
				if (-enemy.movement.rb.velocity.y > -enemy.movement.rb.velocity.x)
				{
					Switcher("WalkFront");
				}
				else
				{
					Switcher("WalkSide");
				}
			}

		}

		if (enemy.movement.rb.velocity.x == 0 && enemy.movement.rb.velocity.y == 0)
		{
			if (animator.GetCurrentAnimatorStateInfo(animator.GetLayerIndex(baseLayer)).IsName("WalkUp"))
			{
				Switcher("IdleUp");
			}
			else if (animator.GetCurrentAnimatorStateInfo(animator.GetLayerIndex(baseLayer)).IsName("IdleSide"))
			{
				Switcher("IdleSide");
			}
			else if (animator.GetCurrentAnimatorStateInfo(animator.GetLayerIndex(baseLayer)).IsName("IdleFront"))
			{
				Switcher("IdleFront");
			}
		}
	}

}
