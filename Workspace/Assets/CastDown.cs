using UnityEngine;
using System.Collections;
using System;

public class CastDown : MonoBehaviour {
	public float floorHeat;
	public Transform potentialTile;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		down ();
	}

	void down(){
		RaycastHit hit;
		Ray ray = new Ray (transform.position, Vector3.down);
		if (Physics.Raycast (ray, out hit, 10)) {
			 if(hit.collider.gameObject.layer == 8){
				try{floorHeat=hit.collider.gameObject.GetComponent<TileProperties>().BaseHeat;
					potentialTile=hit.transform;}
				catch(Exception e){}
			}	
		} 
	}
}
