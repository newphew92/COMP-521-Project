using UnityEngine;
using System.Collections;

public class PlayerInfluenceMap
{
	public float[,] InfluenceMap { get; private set; }

	public float PlayerCenterInfluence; // heat at center of influence
	public int PlayerInfluenceRadius;

	private float HighGroundBonus;

	private Transform[,] board;

	public PlayerInfluenceMap(int length, int width, float pCenterInfluence, int pInfluenceRadius, float hGroundBonus, Transform[,] brd)
	{
		PlayerCenterInfluence = pCenterInfluence;
		PlayerInfluenceRadius = pInfluenceRadius;

		HighGroundBonus = hGroundBonus;

		board = brd;

		InfluenceMap = new float[length,width];
		ResetPlayerInfluenceMap ();
	}

	// puts all values to 0
	private void ResetPlayerInfluenceMap()
	{
		for( int i = 0; i < InfluenceMap.GetLength(0); i++ )
		{
			for( int j = 0; j < InfluenceMap.GetLength(1); j++ )
			{
				InfluenceMap[i,j] = 0;
			}
		}
	}

	// Calculates the value of influence the player exerts assuming level terrain.
	// Values are always positive.
	public float CalculatePlayerInfluence( float tilesFromPlayer )
	{
		float decayRate = 0;
		decayRate = PlayerCenterInfluence / (PlayerInfluenceRadius+1);


		float ret = PlayerCenterInfluence - decayRate * tilesFromPlayer;

		if (ret > 0) return ret;
		else return 0;
	}
	

	// Does a square area of influence with linear decrease
	public void UpdatePlayerInfluenceMap(Transform[] redTeam, Transform[] blueTeam)
	{
		ResetPlayerInfluenceMap ();
		AddTeamInfluence (redTeam, false);
		AddTeamInfluence (blueTeam, true);
	}

	private void AddTeamInfluence(Transform[] playerTeam, bool isBlueTeam)
	{
		for (int q = 0; q < playerTeam.Length; q++) 
		{
			Transform unit = playerTeam [q];
			
			if (unit != null) 
			{
				UnitProperties prop = unit.GetComponent<UnitProperties> ();
				Vector2 pos = prop.CurrentlyOnTile;
				
				// actual update of influence
				int x = (int)pos.x;
				int y = (int)pos.y;
				
				for (int i = x - PlayerInfluenceRadius; i <= x + PlayerInfluenceRadius; i++) 
				{
					if (i < 0 || i >= InfluenceMap.GetLength (0))
						continue;
					
					for (int j = y - PlayerInfluenceRadius; j <= y + PlayerInfluenceRadius; j++) 
					{
						if (j < 0 || j >= InfluenceMap.GetLength (1))
							continue;
						
						int xDist = Mathf.Abs (i - x);
						int yDist = Mathf.Abs (j - y);
						int distFromPlayer = 0;
						
						if (xDist < yDist)
							distFromPlayer = yDist;
						else
							distFromPlayer = xDist;
						float heat = 0f;
						
						// check if in circle
						int a = xDist;
						int b = yDist;						
						int radius = PlayerInfluenceRadius + 1;
						
						if (a * a + b * b < radius * radius)
							heat = CalculatePlayerInfluence (distFromPlayer);

						// apply team and height modifiers
						if(isBlueTeam) heat *= -1f;
						heat = ApplyTerrainModifiers(pos, i, j, heat);
						InfluenceMap [i, j] += heat;
					}
				}
			}
		}
	}


	// TODO: debug
	private float ApplyTerrainModifiers(Vector2 playerPos, int i, int j, float heat)
	{

		int x = (int) playerPos.x;
		int y = (int) playerPos.y;

		// you control the space you're on no matter what
		if (x == i && y == j)
			return heat;

		bool posIsRamp = board [x, y].GetComponent<TileProperties> ().Ramp;
		bool onRamp = board [x, y].GetComponent<TileProperties> ().Ramp;

		float posHeight = board [x, y].position.y;
		float spotHeight = board [i, j].position.y;

		float heightDiff = Mathf.Abs(spotHeight - posHeight);

		if (spotHeight == posHeight || heightDiff < 1)
			return heat;

		// player is on heigher ground than the spot
		else if( posHeight > spotHeight)
		{
			int flooredHeight = (int) heightDiff; // rounds down
			float modifiedHeat = flooredHeight * HighGroundBonus * heat;

			return modifiedHeat;
		}

		// player is on lower ground than the spot
		else
		{
			// can't fire -- no influence
			if( !posIsRamp && !onRamp || heightDiff >= 1f)
				return 0;

			//TODO: shouldn't happen remove once test
			Debug.Log("You missed a case");
			return heat;
		}
	}
}
