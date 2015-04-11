using UnityEngine;
using System.Collections;

public class SquadManager : MonoBehaviour {
	public Transform[] Reds;
	public Transform[] Blues;
	public int BlueCount;
	public int RedCount;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		SquadTrack ();
	}

	void SquadTrack(){
		BlueCount = 0;
		RedCount = 0;

		for (int i=0; i<gameObject.transform.GetChildCount(); i++) {
			if (gameObject.transform.GetChild(i).tag=="Red"){
				RedCount++;}
			else{
				BlueCount++;}
		}
		Reds= new Transform[RedCount];
		Blues = new Transform[BlueCount];
		int x = 0;
		int y = 0;
		for (int i=0; i<gameObject.transform.GetChildCount(); i++) {
			if (gameObject.transform.GetChild(i).tag=="Red"){
				Reds[x]=gameObject.transform.GetChild (i);
				x++;}
			else{
				Blues[y]=gameObject.transform.GetChild (i);
				y++;}
		}
	}

	void SetEnemies(){
		int j;
		if (transform.GetChild (0).tag == "Red") {
			j = 1;
		} else {
			j = 0;
		}
	}
}
