using UnityEngine;
using System.Collections;

// analyzes based on view distance, not shooting distance
public class ViewDistanceAnalyzer : AbstractTerrainAnalyzer 
{
	public int UnitViewRadius;
	private int[,] ViewDistances; // num tiles seen from each given point

	public override void AnalyzeTerrain()
	{
		int length = level.GetLength (0);
		int width = level.GetLength (1);
		ViewDistances = new int[length, width];

		FillInViewDistances ();
		// TODO: fill in heat
	}

	void FillInViewDistances ()
	{
		// for each tile
		for( int i = 0; i < level.GetLength(0); i++)
		{
			for( int j = 0; j < level.GetLength(1); j++)
			{
				ViewDistances[i,j] = NumberOfTileInVision(i,j);
			}
		}
	}

	int NumberOfTileInVision(int i, int j)
	{
		Transform tile = level [i, j].transform;
		int canSee = 1; // because the tile itself can be seen

		for( int x = 0; i < level.GetLength(0); i++)
		{
			for( int y = 0; j < level.GetLength(1); j++)
			{
				if(x == i && y == j)
					continue;

				Transform currentlyObserving = level[x,y].transform;

				if(currentlyObserving.position.y - tile.position.y < 1)
				{
					// TODO raycast
				}
			}
		}
		return canSee;
	}
}
