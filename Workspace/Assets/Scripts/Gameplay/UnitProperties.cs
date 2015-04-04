using UnityEngine;
using System.Collections;

public class UnitProperties : MonoBehaviour 
{
	public enum Team{NONE, Red, Blue};

	public Team PlayerSide;
	public Vector2 CurrentlyOnTile = Vector2.zero;

	public float HP = 100f;
	public float FiringRate = 0.5f; // in shots per second
	
	public Transform Bullet;
	public Transform BulletSpawnPoint;
	
	private float BulletStrength = 10f;
	private float ShotCooldown = 0f;

	void Update()
	{
		if (HP<=0)
			Destroy(gameObject);

		if (ShotCooldown > 0)
			ShotCooldown -=Time.deltaTime;
		
		UpdatePosition ();
	}
	
	public void shoot(Transform target)
	{
		if (ShotCooldown <= 0) 
		{
			Vector3 direction = target.position - transform.position;
			Transform shotFired = Instantiate (Bullet, BulletSpawnPoint.position, this.transform.rotation) as Transform;
			shotFired.Rotate(new Vector3(90,0,0));
			shotFired.GetComponent<Rigidbody> ().AddForce (direction * BulletStrength);
			shotFired.GetComponent<LaserScript>().Destination = target;
			
			ShotCooldown = FiringRate;
		}
	}

	// Updates the tile that the unit is currently above
	private void UpdatePosition()
	{
		RaycastHit hit;
		Physics.Raycast (transform.position, transform.up * -1, out hit, 10f);

		CurrentlyOnTile = hit.collider.transform.GetComponent<TileProperties>().Position;
	}
}