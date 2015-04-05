using UnityEngine;
using System.Collections;

public class PlayerInfluenceMap
{
	public float PlayerCenterInfluence; // heat at center of influence
	public int PlayerInfluenceRadius; // 0 means the player only has influence on their square, 1 means 1 square away
	public float[,] InfluenceMap { get; private set; }

	public PlayerInfluenceMap(int length, int width, float pCenterInfluence, int pInfluenceRadius)
	{
		PlayerCenterInfluence = pCenterInfluence;
		PlayerInfluenceRadius = pInfluenceRadius;

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
	public void UpdatePlayerInfluenceMap(Transform[] playerTeam)
	{
		ResetPlayerInfluenceMap ();
		for( int q = 0; q < playerTeam.Length; q++)
		{
			Transform unit = playerTeam[q];
			
			if( unit != null )
			{
				UnitProperties prop = unit.GetComponent<UnitProperties>();
				Vector2 pos = prop.CurrentlyOnTile;

				// TODO: figure out how to do it with different teams
				// TODO: need to factor in terrain bonuses

				// actual update of influence
				int x = (int) pos.x;
				int y = (int) pos.y;

				for( int i = x - PlayerInfluenceRadius; i <= x + PlayerInfluenceRadius; i++ )
				{
					if( i < 0 || i >= InfluenceMap.GetLength(0)) continue;
				
					for( int j = y - PlayerInfluenceRadius; j <= y + PlayerInfluenceRadius; j++ )
					{
						if( j < 0 || j >= InfluenceMap.GetLength(1)) continue;

						int xDist = Mathf.Abs(i - x);
						int yDist = Mathf.Abs(j - y);
						int distFromPlayer =  0;
						
						if( xDist < yDist ) distFromPlayer = yDist;
						else distFromPlayer = xDist;
						float heat = 0f;

						// check if in circle
						int a = xDist;
						int b = yDist;						
						int radius = PlayerInfluenceRadius+1;

						if( a*a + b*b < radius * radius )
							heat = CalculatePlayerInfluence(distFromPlayer);
						InfluenceMap[i,j] += heat;
					}
				}
			}
		}
	}
}
