using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Draw
{
	//public static Draw draw;
	public static void DrawBox(Vector2 origin, float x, float y, Color color)
	{
		Vector3 start;
		Vector3 end;
		//topline
		start = new Vector3(origin.x - x / 2, origin.y + y / 2);
		end = new Vector3(origin.x + x / 2, origin.y + y / 2);
		Debug.DrawLine(start, end, color);
		//right line
		start = end;
		end = new Vector3(origin.x + x / 2, origin.y - y / 2);
		Debug.DrawLine(start, end, color);
		//downline
		start = end;
		end = new Vector3(origin.x - x / 2, origin.y - y / 2);
		Debug.DrawLine(start, end, color);
		//leftline
		start = end;
		end = new Vector3(origin.x - x / 2, origin.y + y / 2);
		Debug.DrawLine(start, end, color);
	}

	public static void DrawCircle(Vector2 origin, float radius, Color color)
	{
		Vector2 dir = (Vector2.up * radius) - origin;

		for (int angle = 0; angle <= 360; angle += 10)
		{
			dir.Rotate(10);
			Debug.DrawLine(origin, dir);
		}


	}

}
