using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
public class Level : MonoBehaviour 
{
	public float[,] Grid;
	public Transform Block;
	public float Scale = 1;

	// Use this for initialization
	void Start () 
	{
		TerrainMaker terrain = new TerrainMaker ();
		Grid = terrain.Level1;
		GenerateTerrain();
	}

	public void GenerateTerrain ()
	{
		for (int i = 0; i < Grid.GetLength(0); i++) 
		{
			for (int j = 0;j < Grid.GetLength(1); j++)
			{
				if( Grid[i,j] >= 0)
				{
					Transform floor = Instantiate( Block, new Vector3(i, Grid[i,j], j), Quaternion.identity ) as Transform;
					floor.parent = gameObject.transform;
				}
				else SpawnRamp(i,j);
			}
		}
	}

	public void SpawnRamp( int i, int j)
	{
		Transform ramp = Instantiate( Block, new Vector3(i * Scale, -1*Grid[i,j] * Scale, j * Scale), Quaternion.identity ) as Transform;

		int modifier = 1;

		if (LeftToRight (i, j))
		{
			if( Grid[i-1, j] > Grid[i+1, j]) modifier = -1;
			ramp.Rotate (new Vector3 (0, 0, 30 * modifier));
		}
		else
		{
			if( Grid[i, j-1] > Grid[i, j+1]) modifier = -1;
			ramp.Rotate (new Vector3 (30 * modifier, 0, 0));
		}

	}

	private bool LeftToRight(int i, int j)
	{
		if (i - 1 < 0 || i + 1 >= Grid.GetLength (0))
			return false;
		if (Grid [i - 1, j] != Grid [i + 1, j])
			return true;
		return false;
	}
}
