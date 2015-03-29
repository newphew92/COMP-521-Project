using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HumanFiringScript : MonoBehaviour 
{
	public Transform textTransform;
	private Text t;
	private bool alive = true;
	private Transform Robots;

	void Start()
	{
		t = textTransform.GetComponent<Text> ();
		Robots = GameObject.Find ("Robots").transform;
	}

	// Update is called once per frame
	void Update () 
	{
		if(Input.GetMouseButtonDown(0) && alive)
		{
			UnitProperties p = gameObject.GetComponent<UnitProperties>();
			p.shoot(p.BulletSpawnPoint.forward);
		}

		if( gameObject.GetComponent<UnitProperties>().HP <= 0 )
		{
			t.color = Color.red;
			t.text = "YOU LOSE";
			alive = false;
		}
		else if( alive && Robots.childCount == 0 )
		{
			t.text = "YOU WIN";
			t.color = Color.green;
			Invoke("NextLevel", 3);
		}
	}

	private void NextLevel()
	{
		int level = Application.loadedLevel;
		Application.LoadLevel (level + 1);
	}
}
