using UnityEngine;
using System.Collections;

// analyzes based on view distance, not shooting distance
public class ViewDistanceAnalyzer : AbstractTerrainAnalyzer 
{
	public int UnitViewRadius;
	private int[,] ViewDistances; // num tiles seen from each given point
	private Transform testUnit; // used for testing raycasts

	public override void AnalyzeTerrain()
	{
		Transform prefab = Resources.Load ("TerrainGen/Floor") as Transform;
		testUnit = Instantiate (testUnit, Vector3.zero, Quaternion.identity) as Transform;

		int length = level.GetLength (0);
		int width = level.GetLength (1);
		ViewDistances = new int[length, width];

		FillInViewDistances ();
		// TODO: fill in heat
		Destroy (testUnit.gameObject);
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

		testUnit.transform.position = tile.position + Vector3.up;

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
					Vector3 origin = currentlyObserving.position + Vector3.up * 0.5f;
					Vector3 direction = tile.position - origin;
					if( Physics.Raycast( origin, direction, out hit, UnitViewRadius))
					{
						if(hit.transform.name == "Floor");
							canSee++;
					}
				}
			}
		}
		return canSee;
	}
}
