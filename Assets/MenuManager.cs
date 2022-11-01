using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
	[SerializeField] GameObject readme;
	public void First()
	{
		readme.SetActive(true);
	}

	public void Second()
	{
		SceneManager.LoadScene(1);
	}

	public void Last()
	{
		Application.Quit();
	}
	public void Menu()
	{
		SceneManager.LoadScene(0);
	}
}
