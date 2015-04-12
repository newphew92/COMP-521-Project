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

	private Transform[,] board;
	private Transform players;

	void Start()
	{
		board = GameObject.Find ("Level").GetComponent<TerrainManager> ().RawBoard;
		players = GameObject.Find ("Players").transform;
	}

	void Update()
	{
		if (HP <= 0)
			Destroy(gameObject);

		if (ShotCooldown > 0)
			ShotCooldown -=Time.deltaTime;

		Fire ();

		UpdatePosition ();
	}

	private void Fire()
	{
		Transform closestEnemy = GetClosestUnit ();
		if(closestEnemy != null)
			ShootAt (closestEnemy);
	}

	private Transform GetClosestUnit()
	{
		float closestDist = 99999f;
		Transform closestUnit = null;

		Vector3 pos = transform.position;
		for( int i = 0; i < players.childCount; i++)
		{
			Transform unit = players.GetChild(i);
			if( unit != null )
			{
				UnitProperties enemyProp = unit.GetComponent<UnitProperties>();
				if(enemyProp.PlayerSide != PlayerSide)
				{
					Vector3 flattenedpos = new Vector3(transform.position.x, 0, transform.position.z);
					Vector3 flattenedunit = new Vector3(unit.position.x, 0, unit.position.z);
					float dist = Vector3.Distance(flattenedpos, flattenedunit);

					float height = board[(int)CurrentlyOnTile.x, (int)CurrentlyOnTile.y].localPosition.y;
					float enemyheight = board[(int)enemyProp.CurrentlyOnTile.x, (int) enemyProp.CurrentlyOnTile.y].localPosition.y;
					float heightDiff = Mathf.Abs(height - enemyheight);

					if( height + 0.5f >= enemyheight)
					{
						if(dist < closestDist && dist < FiringRadius)
						{
							closestDist = dist;
							closestUnit = unit;
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
		LayerMask mask = 1 << LayerMask.NameToLayer ("Terrain");
		if(Physics.Raycast (transform.position + Vector3.up * 2, Vector3.down, out hit, 10f, mask))
			CurrentlyOnTile = hit.collider.transform.GetComponent<TileProperties> ().Position;
		else Debug.Log("WE HAVE A PROBLEM CAPN: BOT POS WON UPDATE");
	}
}