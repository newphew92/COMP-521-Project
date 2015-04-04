using UnityEngine;
using System.Collections;

public class UnitProperties : MonoBehaviour 
{
	public float HP = 100f;
	public float FiringRate = 0.5f; // in shots per second
	
	public Transform Bullet;
	public Transform BulletSpawnPoint;
	
	private float BulletStrength = 10f;
	private float ShotCooldown = 0f;

	void Update()
	{
		if (ShotCooldown > 0)
			ShotCooldown -=Time.deltaTime;

		if (HP<=0)
			Destroy(gameObject);
	}
	
	public void shoot(Transform target)
	{
		if (ShotCooldown <= 0) 
		{
			Vector3 direction = target.position - transform.position;
			Transform shotFired = Instantiate (Bullet, BulletSpawnPoint.position, this.transform.rotation) as Transform;
			shotFired.Rotate(new Vector3(90,0,0));
			shotFired.GetComponent<Rigidbody> ().AddForce (direction * BulletStrength);
			
			ShotCooldown = FiringRate;
		}
	}	
}