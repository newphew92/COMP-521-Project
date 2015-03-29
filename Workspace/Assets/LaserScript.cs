using UnityEngine;
using System.Collections;

public class LaserScript : MonoBehaviour 
{
	public AudioClip[] laserFire;
	public float damage = 20f;
	private float timeout = 5f;
	private AudioSource source;
	void Start()
	{
		source = GetComponent<AudioSource> ();
		source.PlayOneShot (laserFire [Random.Range (0, laserFire.Length)]);
	}

	void Update()
	{
		if(timeout <= 0) Destroy (gameObject);
		timeout -= Time.deltaTime;
	}

	void OnCollisionEnter(Collision collision)	
	{
		if (collision.transform.tag == "Good" || collision.transform.tag == "Enemy")
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
		GetComponent<CapsuleCollider> ().enabled = false;
		GetComponent<MeshRenderer> ().enabled = false;
	}
}
