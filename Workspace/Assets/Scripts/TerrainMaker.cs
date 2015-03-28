using UnityEngine;
using System.Collections;

public class TerrainMaker
{
	public int[,] Level0 { get; private set; } // plane control level
	public int[,] Level1 { get; private set; } // two high areas
	public int[,] Level2 { get; private set; } // one high area

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
		Level0 = new int[10,10];
		FillFloor (Level0);
	}

	void InitializeLevel1()
	{
		Level1 = new int[10,10];
		FillFloor (Level1);

	}

	void InitializeLevel2()
	{
		Level2 = new int[11,11];
		FillFloor (Level2);
		make3x3Grid (6, 6, 2, Level2);

	}

	private void FillFloor(int[,] level)
	{
		for(int i = 0; i < level.GetLength(0); i++)
		{
			for(int j = 0; j < level.GetLength(1); j++)
			{
				level[i,j] = 0;
			}
		}
	}

	private void make3x3Grid(int i, int j, int height, int[,] g)
	{
		for(int x = i; x < i+3; i++)
		{
			for(int y = j; y < j+3; i++)
			{
				g[x,y] = height;
			}
		}
	}
}
