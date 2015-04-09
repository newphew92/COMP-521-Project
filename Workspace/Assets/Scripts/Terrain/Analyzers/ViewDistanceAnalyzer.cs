using UnityEngine;
using System.Collections;

// TODO
public class ViewDistanceAnalyzer : AbstractTerrainAnalyzer 
{
	public int UnitViewRadius;
	private float[,] ViewDistances;
	private float[,] SmallestViewDistance; // used tofr choke points

	// TODO: look at lower ground
	public override void AnalyzeTerrain()
	{
		int length = level.GetLength (0);
		int width = level.GetLength (1);
		ViewDistances = new float[length, width];
		SmallestViewDistance = new float[length, width];

		FillInViewDistances ();
	}

	// TODO
	private void FillInViewDistances ()
	{
		// Algorithm:
			// Raycast in all directions
				// if terrain is hit, track which block it is ( bool[,] )
			// Do the same for looking at lower ground
			// compute heat value based on this

		// for each tile
		for( int i = 0; i < level.GetLength(0); i++)
		{
			for( int j = 0; j < level.GetLength(1); j++)
			{
				// 360 degrees of raycast
				for( int x = 0; x < 360; x++)
				{
					// figure out distance of nearest walls


					// now figure out if there's a vantage point
						// perhaps look at surrounding tiles that weren't raycast hit and see if they're lower?
							// then what about larger planes that are below?
				}
			}
		}
	}

	public override Transform[][] GetChokePoints()
	{

		return null;
	}
}
