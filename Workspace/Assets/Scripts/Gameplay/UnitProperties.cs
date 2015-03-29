using UnityEngine;
using System.Collections;

public class UnitProperties : MonoBehaviour 
{
	public float HP = 100f;
	public float FiringRate = 0.5f; // in shots per second
	
	public Transform Bullet;
	public Transform BulletSpawnPoint;
	
	private float BulletStrength = 10000f;
	private float ShotCooldown = 0f;
	public bool Lit;
	void Update()
	{
		checkLight ();
		FiringRate = 0.5f;
		if (ShotCooldown > 0)
			ShotCooldown -=Time.deltaTime;
		if (!Lit) {
			HP -= Time.deltaTime;
		}
		if (HP<=0){Destroy(gameObject);}
	}
	
	public void shoot(Vector3 direction)
	{
		if (ShotCooldown <= 0) 
		{
			Transform shotFired = Instantiate (Bullet, BulletSpawnPoint.position, this.transform.rotation) as Transform;
			shotFired.Rotate(new Vector3(90,0,0));
			shotFired.GetComponent<Rigidbody> ().AddForce (direction * BulletStrength);
			
			ShotCooldown = FiringRate;
			AudioSource a = gameObject.GetComponent<AudioSource>();
//			a.PlayOneShot(a.clip);
			Debug.Log(Random.Range(0,100));
		}
	}

	void checkLight(){
		RaycastHit hit;
		Ray ray = new Ray (transform.position, Vector3.down);
		if (Physics.Raycast (ray, out hit, 400)) {
			Lit=hit.transform.GetComponent<Floor>().Lit;
			//			Seen = (hit.transform.tag == "Player");
		} else {
			Lit = false;
		}
	}
}
