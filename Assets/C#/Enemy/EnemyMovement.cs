using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
	[HideInInspector] public Rigidbody2D rb;

	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	public void Chase(Vector3 target, float speed)
	{
		rb.velocity = (target - transform.position).normalized * speed * Time.deltaTime;
	}

	public void Escape(Vector3 target, float speed)
	{
		rb.velocity = (transform.position - target).normalized * 2 * speed * Time.deltaTime;
	}

	public void Stop()
	{
		rb.velocity = Vector2.zero;
	}

	public void Patrol(Vector3 target, float speed)
	{
		Stop();
	}

	public IEnumerator Turn(Vector3 target)
	{
		Chase(-target, 0.1f);
		yield return new WaitForSeconds(0.05f);
		Chase(target, 0.05f);
		yield return new WaitForSeconds(0.05f);
		Stop();
	}


}
