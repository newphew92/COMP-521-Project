using UnityEngine;
using System.Collections;

// TODO
public class HeightAnalyzer : AbstractTerrainAnalyzer 
{
	public override void AnalyzeTerrain()
	{
		float maxHeight = GetHeightExtreme ();
		float heatIncrease = 0;
		if( maxHeight > 0 )
			heatIncrease = maxTerrainHeat / maxHeight;

		for( int i = 0; i < level.GetLength(0); i++)
		{
			for( int j = 0; j < level.GetLength(1); j++)
			{
				Transform tile = level[i,j];
				tile.GetComponent<TileProperties>().BaseHeat = heatIncrease * (tile.localPosition.y * -1);
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
				int currentHeight = (int) (Mathf.Abs(level[i,j].transform.localPosition.y));
				if(currentHeight > max)
					max = currentHeight;
			}
		}
		return max;
	}
}
