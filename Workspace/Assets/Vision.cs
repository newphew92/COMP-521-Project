using UnityEngine;
using System.Collections;

public class Vision : MonoBehaviour {
	public Transform Target;
	public int Friendlies;
	public int Ennemies;
	public Vector3 TargPos;
	// Use this for initialization
	void Start () {
		Friendlies = 0;
		Ennemies = 0;
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter(Collider col){
		if (col.tag == transform.parent.tag && (col.tag == "Enemy" || col.tag == "Good")) {

			Friendlies++;
		} else if (col.tag != transform.parent.tag && (col.tag == "Enemy" || col.tag == "Good")) {
			Target = col.transform;
			Ennemies++;
		}
	}

	void OnTriggerExit(Collider col){
		if (col.tag == transform.parent.tag && (col.tag == "Enemy" || col.tag == "Good")) {
			Friendlies--;
		} else if (col.tag != transform.parent.tag && (col.tag == "Enemy" || col.tag == "Good")) {
			Ennemies--;
		}
	}

	void OnTriggerStay(Collider col){
		if (col.tag != transform.parent.tag&&(col.tag=="Enemy"||col.tag=="Good" )) {

			Target = col.transform;
			Debug.Log(Target.position);
		}
	}

}
