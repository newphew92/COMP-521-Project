using UnityEngine;
using System.Collections;

public class LaserScript : MonoBehaviour 
{
	public float damage = 20f;

	void OnCollisionEnter(Collision collision)	
	{
		if (collision.transform.tag == "Robot")
		{
			collision.transform.GetComponent<UnitProperties>().HP -= damage;
		}
		Destroy (gameObject);
	}
}
