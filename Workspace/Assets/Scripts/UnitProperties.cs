using UnityEngine;
using System.Collections;

public class UnitProperties : MonoBehaviour 
{
	public float HP = 100f;
	public float FiringRate = 3f; // in shots per second
	private float BulletStrength = 10000f;

	private float ShotCooldown = 0f;

	public Transform Bullet;
	public Transform BulletSpawnPoint;

	void Update()
	{
		if (ShotCooldown > 0)
			ShotCooldown -= Time.deltaTime;
	}

	public void shoot(Vector3 direction)
	{
		if (ShotCooldown <= 0) 
		{
//			Quaternion rotation = Quaternion.FromToRotation(Bullet.forward, BulletSpawnPoint.position.forward);
//			Transform shotFired = Instantiate (Bullet, BulletSpawnPoint.position, rotation) as Transform;

//			shotFired.GetComponent<Rigidbody> ().AddForce (direction * BulletStrength);

			ShotCooldown = FiringRate;
		}
	}
}
