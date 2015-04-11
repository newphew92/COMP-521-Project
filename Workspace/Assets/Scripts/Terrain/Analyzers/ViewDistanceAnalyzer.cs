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
		transform.parent = GameObject.Find ("Level").transform;

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
				testUnit.transform.position = level[i,j].transform.position + Vector3.up;
				ViewDistances[i,j] = NumberOfTileInVision(i,j);
			}
		}
	}

	int NumberOfTileInVision(int i, int j)
	{
		Transform tile = level [i, j].transform;
		int canSee = 0;

		for( int x = 0; x < level.GetLength(0); x++)
		{
			for( int y = 0; y < level.GetLength(1); y++)
			{
				Transform currentlyObserving = level[x,y].transform;

				if(currentlyObserving.position.y - tile.position.y < 1)
				{
					RaycastHit hit;
					Vector3 origin = currentlyObserving.position + Vector3.up;
					Vector3 direction = testUnit.transform.position - origin;

					LayerMask mask = (1 << LayerMask.NameToLayer ("Terrain"));
					if(Physics.Raycast(origin, direction, out hit, UnitViewRadius, mask))
					{

						if(hit.transform.name == testUnit.name)
						{
							canSee++;
						}
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
		int maxVision = 0;

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

		// now assign the heat values proportionately
		float heatDiff = maxVision - minVision;
		Debug.Log (maxVision);
		Debug.Log (minVision);
		float heatIncrease = 0;
		if( heatDiff > 0 )
			heatIncrease = 2*maxTerrainHeat / heatDiff;

		for( int i = 0; i < ViewDistances.GetLength(0); i++)
		{
			for( int j = 0; j < ViewDistances.GetLength(1); j++)
			{
				Transform tile = level[i,j];
				tile.GetComponent<TileProperties>().BaseHeat = maxTerrainHeat - heatIncrease * (ViewDistances[i,j] - minVision);
			}
		}
	}
}