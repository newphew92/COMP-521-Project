using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
public class Level : MonoBehaviour {
	public int TileSize;
	public int GridSize;
	public int SquareHeight;
	public int[,] Grid;
	public Transform Floor;
	public Transform Wall;
	public ArrayList coords=new ArrayList();
//	public List<Tuple<int,int>> coords=new List<Tuple<int,int>>();
	// Use this for initialization
	void Start () {
		TileSize = 1;
		GridSize = 10;
		coords.Add((0,0));
		coords.Add (new Tuple (1,1));
		initialize ();
		render ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void initialize(){
		Grid = new int[GridSize, GridSize];
		for (int i=0; i<GridSize; i++) {
			for (int j=0;j<GridSize; j++){
				Grid[i,j]=0;
			}
		}
	}

	void obstaclize(){
		for (int i=0; i<coords.Count; i++) {
			Grid[coords[i](0),coords[i](1)]=1;
		}
	}
	void render(){
		for (int i=0; i<GridSize; i++) {
			for (int j=0;j<GridSize; j++){
				Instantiate(Floor, new Vector3(i*TileSize,0,j*TileSize), Quaternion.identity);
			}
		}

	}
}
