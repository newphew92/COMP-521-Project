using UnityEngine;
using System.Collections;

public class LaserScript : MonoBehaviour 
{
	public float damage = 20f;

	private float timeout = 5f;

	void Update()
	{
		if(timeout <= 0) Destroy (gameObject);
		timeout -= Time.deltaTime;
	}

	void OnCollisionEnter(Collision collision)	
	{
		if (collision.transform.tag == "Robot" || collision.transform.tag == "Enemy")
		{
			Transform t = collision.transform;
			UnitProperties prop = t.GetComponent<UnitProperties>();
			while( prop == null )
			{
				t = t.parent;
				prop = t.GetComponent<UnitProperties>();
			}
			collision.transform.GetComponent<UnitProperties>().HP -= damage;
		}
		Destroy (gameObject);
	}
}
