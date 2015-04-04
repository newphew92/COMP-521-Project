using UnityEngine;
using System.Collections;

public class TerrainManager : MonoBehaviour 
{
	// used to render the colours properly on the tiles
	public bool RenderHeatOnTiles = true;
	public float MaxHeat;
	public float PlayerCenterInfluence;

	private HSBColor P1Color = HSBColor.FromColor(Color.red);
	private HSBColor P2Color = HSBColor.FromColor(Color.blue);

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

	// Calculates the value of influence the player exerts assuming level terrain.
	// Values are always positive.
	public float playerInfluence( float tilesFromPlayer )
	{
		float ret = PlayerCenterInfluence - tilesFromPlayer;
		if (ret > 0) return ret;

		return 0;
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
					P1Color.s = (heat/MaxHeat);
					rend.material.color = P1Color.ToColor();
				}
				else if(heat < 0)
				{
					P2Color.s = Mathf.Abs(heat/MaxHeat);
					rend.material.color = P2Color.ToColor();
				}
			}
		}
	}
}
