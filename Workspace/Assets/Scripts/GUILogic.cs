using UnityEngine;
using System.Collections;

public class GUILogic : MonoBehaviour 
{
	public void StartGame()
	{
		Application.LoadLevel ("Level1");
	}

	public void QuitGame()
	{
		Application.Quit ();
	}
}
