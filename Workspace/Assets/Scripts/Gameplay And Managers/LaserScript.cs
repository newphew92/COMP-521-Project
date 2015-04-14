using UnityEngine;
using System.Collections;

public class LaserScript : MonoBehaviour 
{
	public AudioClip[] laserFire;
	public Transform Destination;
	public float damage = 20f;
	private float timeout = 5f;
	private bool hit = false;

	void Start()
	{
		AudioSource source = GetComponent<AudioSource> ();
		source.PlayOneShot (laserFire [Random.Range (0, laserFire.Length)]);
	}
	
	void Update()
	{
		if(timeout <= 0) Destroy (gameObject);
		timeout -= Time.deltaTime;
		if(Destination != null)
			Destination.GetComponent<UnitProperties> ().HP -= damage;
	}
	
	void OnCollisionEnter(Collision collision)	
	{
		UnitProperties p = Destination.GetComponent<UnitProperties> ();
		GetComponent<MeshRenderer> ().enabled = false;
		GetComponent<CapsuleCollider> ().enabled = false;
	}
}