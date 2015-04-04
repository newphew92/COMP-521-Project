using UnityEngine;
using System.Collections;

public class TerrainGenerator : MonoBehaviour 
{
	public float MaxHeight;
	public float GridLength;
	public float GridHeight;

	public Transform empty;
	public Transform floor;

	// Use this for initialization
	void Start () 
	{
		for(int i = 0; i < GridLength; i++)
		{
			Transform row = Instantiate(empty, new Vector3(0,0, i), Quaternion.identity) as Transform;
			row.name = ""+i;
			row.parent = transform;

			for(int j = 0; j < GridHeight; j++)
			{
				Vector3 pos = row.position + new Vector3(j, 0, 0);
				Transform tile = Instantiate( floor, pos, Quaternion.identity) as Transform;

				tile.name = ""+j;
				tile.parent = row;

				TileProperties tileProp = tile.gameObject.AddComponent<TileProperties>();
				tileProp.heat = MaxHeight;
				tileProp.position = new Vector2(i,j);
			}
		}
	}	
}
