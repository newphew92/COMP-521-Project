using UnityEngine;
using System.Collections;

public class UnitProperties : MonoBehaviour 
{
	public enum Team{NONE, Red, Blue};

	public Team PlayerSide;
	public Vector2 CurrentlyOnTile = Vector2.zero;

	public float HP = 100f;
	public float FiringRate = 0.5f; // in shots per second
	public float FiringRadius = 1f;
	
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

		if(ShotCooldown <= 0)
			Fire ();

		//UpdatePosition ();
	}

	private void Fire()
	{
		Transform[] enemies = GetEnemies();
		Transform closestEnemy = GetClosestUnit (enemies);

		if (closestEnemy != null)
			ShootAt (closestEnemy);
	}

	private Transform[] GetEnemies()
	{
		PlayerManager pm = GameObject.Find ("Level").GetComponent<PlayerManager> ();

		if (PlayerSide == Team.Blue)
			return pm.RedPlayer;

		return pm.BluePlayer;
	}

	private Transform GetClosestUnit(Transform[] enemies)
	{
		float closestDist = 99999f;
		Transform closestUnit = null;

		Vector3 pos = transform.position;
		for( int i = 0; i < enemies.Length; i++)
		{
			Team enemyTeam = enemies [i].GetComponent<UnitProperties> ().PlayerSide;
			if( enemies[i] != null )
			{
				Vector3 enemyPos = enemies[i].position;

				RaycastHit hit;
				LayerMask mask = ~(1 << LayerMask.NameToLayer (enemyTeam.ToString()));
				if(Physics.Raycast(enemyPos + Vector3.up, pos - enemyPos, out hit, FiringRadius, mask))
				{
					// raycast from enemy to unit successfull --> in firing range
					if( hit.transform.position == transform.position )
					{
						float dist = Vector3.Distance( pos, enemyPos );
						if( dist < closestDist )
						{
							closestDist = dist;
							closestUnit = enemies[i];
						}
					}
				}
			}
		}

		return closestUnit;
	}

	private void ShootAt(Transform target)
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