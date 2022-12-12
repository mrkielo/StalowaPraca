using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField] float m_Speed;

	Rigidbody2D rb;
	Vector2 mousePos;
	public float mx;
	public float my;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		my = Input.GetAxisRaw("Vertical");
		mx = Input.GetAxisRaw("Horizontal");
	}

	void FixedUpdate()
	{
		rb.velocity = new Vector2(mx, my).normalized * m_Speed;
	}
}
