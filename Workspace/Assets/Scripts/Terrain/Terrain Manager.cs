using UnityEngine;
using System.Collections;

public class TerrainManager : MonoBehaviour 
{
	// used to render the colours properly on the tiles
	public float MaxHeat;
	public bool RenderHeatOnTiles = true;

	public TileProperties[,] getBoard()
	{
		int rows = transform.childCount;
		int cols = transform.GetChild (0).childCount;

		TileProperties[,] ret = new TileProperties[rows, cols];
		for(int i = 0; i < transform.childCount; i++)
		{
			Transform row = transform.GetChild(i);
			for( int j = 0; j < row.childCount; j++)
			{
				Transform tile = row.GetChild(j);
				ret[i,j] = tile.GetComponent<TileProperties>();
			}
		}
		return ret;
	}


	void Update()
	{
		if( RenderHeatOnTiles )
		{
			RenderHeat();
		}
	}

	private void RenderHeat()
	{
		for(int i = 0; i < transform.childCount; i++)
		{
			Transform row = transform.GetChild(i);
			for( int j = 0; j < row.childCount; j++)
			{
				Transform tile = row.GetChild(j);
				TileProperties tProp = tile.GetComponent<TileProperties>();

				Renderer rend = tile.GetComponent<Renderer>();
				float heat = tProp.heat;
				if(heat >= 0)
				{
					rend.material.color = new Color( heat/MaxHeat, 0, 0);
				}
				else if(heat < 0)
				{
					float b = Mathf.Abs( heat/MaxHeat);
					rend.material.color = new Color( 0, 0, b);
				}
			}
		}
	}
}
