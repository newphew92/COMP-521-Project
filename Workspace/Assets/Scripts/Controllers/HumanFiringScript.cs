using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HumanFiringScript : MonoBehaviour 
{
	public Transform textTransform;
	private Text t;
	private bool alive = true;

	void Start()
	{
		t = textTransform.GetComponent<Text> ();
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
		}
	}
}
