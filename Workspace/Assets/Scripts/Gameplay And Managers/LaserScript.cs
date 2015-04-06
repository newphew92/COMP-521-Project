using UnityEngine;
using System.Collections;

public class LaserScript : MonoBehaviour 
{
	public AudioClip[] laserFire;
	public Transform Destination;
	public float damage = 20f;
	private float timeout = 5f;

	void Start()
	{
		AudioSource source = GetComponent<AudioSource> ();
		source.PlayOneShot (laserFire [Random.Range (0, laserFire.Length)]);
	}
	
	void Update()
	{
		if(timeout <= 0) Destroy (gameObject);
		timeout -= Time.deltaTime;
	}
	
	void OnCollisionEnter(Collision collision)	
	{
		UnitProperties p = Destination.GetComponent<UnitProperties> ();
		if (p != null)
			p.HP -= damage;

		GetComponent<CapsuleCollider> ().enabled = false;
		GetComponent<MeshRenderer> ().enabled = false;
	}
}