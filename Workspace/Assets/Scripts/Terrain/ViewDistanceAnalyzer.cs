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
	}

	public override Transform[][] GetChokePoints()
	{

		return null;
	}
}
