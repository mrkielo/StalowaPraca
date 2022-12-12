using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowFixer : MonoBehaviour
{
	// Start is called before the first frame update

	void LateUpdate()
	{
		transform.localScale = new Vector3(1, 1, 1);
	}
}
