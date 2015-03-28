using UnityEngine;
using System.Collections;

public class TerrainMaker
{
	public float[,] Level0 { get; private set; } // plane control level
	public float[,] Level1 { get; private set; } // two high areas
	public float[,] Level2 { get; private set; } // one high area

	public TerrainMaker()
	{ 
		Initialize ();
	}

	// Use this for initialization
	void Initialize () 
	{
		InitializeLevel0 ();
		InitializeLevel1 ();
		InitializeLevel2 ();
	}

	void InitializeLevel0()
	{
		Level0 = new float[10,10];
		FillFloor (Level0);
	}

	void InitializeLevel1()
	{
		Level1 = new float[11,11];
		FillFloor (Level1);
		make3x3Grid (1, 5, 2f, Level1);
		make3x3Grid (9, 5, 2f, Level1);
		Level1 [1, 3] = 1f;
		Level1 [1, 7] = 1f;
		Level1 [9, 3] = 1f;
		Level1 [9, 7] = 1f;
	}

	void InitializeLevel2()
	{
		Level2 = new float[11,11];
		FillFloor (Level2);
		make3x3Grid (5, 5, 2f, Level2);
		Level2 [3, 5] = 1f;
		Level2 [7, 5] = 1f;
	}

	private void FillFloor(float[,] level)
	{
		for(int i = 0; i < level.GetLength(0); i++)
		{
			for(int j = 0; j < level.GetLength(1); j++)
			{
				level[i,j] = 0f;
			}
		}
	}

	private void make3x3Grid(int i, int j, float height, float[,] g)
	{
		for(int x = i-1; x < i+2; x++)
		{
			for(int y = j-1; y < j+2; y++)
			{
				g[x,y] = height;
			}
		}
	}
}
