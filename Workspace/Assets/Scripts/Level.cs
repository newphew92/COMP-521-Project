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
	public Queue UpCoords=new Queue();
	public GridPoint Point;
	public Transform Block;
//	public List<Tuple<int,int>> coords=new List<Tuple<int,int>>();
	// Use this for initialization
	void Start () {
		TileSize = 1;
		GridSize = 10;
		UpCoords.Enqueue(new GridPoint(0,0));
		UpCoords.Enqueue (new GridPoint (1,1));
//		Debug.Log (UpCoords [0]);
		initialize ();
		obstaclize ();
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
		for (int i=0; i<=UpCoords.Count; i++) {
			Debug.Log("what");
			Point=(GridPoint)UpCoords.Dequeue();
			Grid[Point.i,Point.j]=1;
		}
	}
	void render(){
		for (int i=0; i<GridSize; i++) {
			for (int j=0;j<GridSize; j++){
				Instantiate(Floor, new Vector3(i*TileSize,0,j*TileSize), Quaternion.identity);
				if (Grid[i,j]==1){
					Debug.Log("hue");
					Instantiate (Block, new Vector3(i*TileSize,TileSize*0.5f,j*TileSize), Quaternion.identity);
				}
			}
		}

	}
}
