using UnityEngine;
using System.Collections;

public class HumanFiringScript : MonoBehaviour 
{
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetMouseButtonDown(0))
		{
			UnitProperties p = gameObject.GetComponent<UnitProperties>();
			p.shoot(p.BulletSpawnPoint.forward);
		}
	}
}
