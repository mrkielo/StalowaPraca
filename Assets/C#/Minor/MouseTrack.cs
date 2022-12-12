using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTrack : MonoBehaviour
{
	private void Start()
	{
		Cursor.visible = false;
	}
	void Update()
	{
		Vector2 mousPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		transform.position = mousPos;


	}
}
