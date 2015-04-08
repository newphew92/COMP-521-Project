using UnityEngine;
using System.Collections;

// TODO
public class HeightAnalyzer : AbstractTerrainAnalyzer 
{
	public override void AnalyzeTerrain()
	{
		float maxHeight = GetHeightExtreme ();
		float heatIncrease = maxTerrainHeat / maxHeight;

		for( int i = 0; i < level.GetLength(0); i++)
		{
			for( int j = 0; j < level.GetLength(1); j++)
			{
				Transform tile = level[i,j];
				tile.GetComponent<TileProperties>().BaseHeat = heatIncrease * tile.pos.y;
			}
		}
	}

	private float GetHeightExtreme()
	{
		float max = 0;
		for( int i = 0; i < level.GetLength(0); i++)
		{
			for( int j = 0; j < level.GetLength(1); j++)
			{
				int currentHeight = Mathf.Abs(level[i,j].transform.position.y);
				if(currentHeight > max)
					max = currentHeight;
			}
		}
	}

	// TODO
	public override Transform[][] GetChokePoints(){return null;}
}
