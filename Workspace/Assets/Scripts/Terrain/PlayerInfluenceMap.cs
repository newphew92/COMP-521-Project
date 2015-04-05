using UnityEngine;
using System.Collections;

public class PlayerInfluenceMap
{
	public float PlayerCenterInfluence; // heat at center of influence
	public float PlayerInfluenceRadius; // 0 means the player only has influence on their square, 1 means 1 square away
	public float[,] InfluenceMap { get; private set; }

	public PlayerInfluenceMap(int length, int width, float pCenterInfluence, float pInfluenceRadius)
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
	public float playerInfluence( float tilesFromPlayer )
	{
		float decayrate = PlayerCenterInfluence / PlayerInfluenceRadius;
		float ret = PlayerCenterInfluence - decayrate * tilesFromPlayer;

		if (ret > 0) return ret;
		else return 0;
	}
	

	// Does a square area of influence with linear decrease
	public void UpdatePlayerInfluenceMap(Transform[] playerTeam)
	{
		ResetPlayerInfluenceMap ();
		for( int i = 0; i < playerTeam.Length; i++)
		{
			Transform unit = playerTeam[i];
			
			if( unit != null )
			{
				UnitProperties prop = unit.GetComponent<UnitProperties>();
				Vector2 pos = prop.CurrentlyOnTile;


			}
		}
	}
}
