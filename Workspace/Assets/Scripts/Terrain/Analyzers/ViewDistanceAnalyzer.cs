using UnityEngine;
using System.Collections;

// analyzes based on view distance, not shooting distance
public class ViewDistanceAnalyzer : AbstractTerrainAnalyzer 
{
	public int UnitViewRadius;
	private int[,] ViewDistances; // num tiles seen from each given point
	private GameObject testUnit; // used for testing raycasts

	public override void AnalyzeTerrain()
	{
		GameObject prefab = Resources.Load ("TerrainGen/Floor") as GameObject;
		testUnit = Instantiate (prefab, Vector3.zero, Quaternion.identity) as GameObject;
		testUnit.name = "testUnit";

		int length = level.GetLength (0);
		int width = level.GetLength (1);
		ViewDistances = new int[length, width];

		FillInViewDistances ();
		FillInHeat ();

		Destroy (testUnit);
	}

	void FillInViewDistances ()
	{
		// for each tile
		for( int i = 0; i < level.GetLength(0); i++)
		{
			for( int j = 0; j < level.GetLength(1); j++)
			{
				testUnit.transform.position = level[i,j].transform.position + Vector3.up * 10;
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
					RaycastHit hit;
					Vector3 origin = currentlyObserving.position + Vector3.up;
					Vector3 direction = tile.position - origin;
					if( Physics.Raycast( origin, direction, out hit, UnitViewRadius))
					{
						Debug.Log(origin + "\n" + direction + "\n" + hit.transform.parent.name +" " + hit.transform.name);
						if(hit.transform.name == testUnit.name)
							canSee++;
					}
				}
			}
		}
		return canSee;
	}

	// fills in the heat based on view distances
	void FillInHeat()
	{
		// find the extremes
		int minVision = 1;
		int maxVision = 1;

		for( int i = 0; i < ViewDistances.GetLength(0); i++)
		{
			for( int j = 0; j < ViewDistances.GetLength(1); j++)
			{
				int currentValue = ViewDistances[i,j];

				if(currentValue > maxVision)
					maxVision = currentValue;
				if(currentValue < minVision)
					minVision = currentValue;
			}
		}

		// TODO: test and fix this heat calculation. I have a suspicion it's no good
		// now assign the heat values proportionately
		float heatDiff = maxVision - minVision;
		float heatIncrease = 0;
		if( heatDiff > 0 )
			heatIncrease = maxTerrainHeat / heatDiff;

		for( int i = 0; i < ViewDistances.GetLength(0); i++)
		{
			for( int j = 0; j < ViewDistances.GetLength(1); j++)
			{
				Transform tile = level[i,j];
				tile.GetComponent<TileProperties>().BaseHeat = heatIncrease * (ViewDistances[i, j]/heatDiff);
			}
		}
	}
}