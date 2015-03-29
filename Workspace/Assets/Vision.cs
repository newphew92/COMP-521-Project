using UnityEngine;
using System.Collections;

public class Vision : MonoBehaviour {
	public Transform Target;
	public int Friendlies;
	public int Ennemies;
	// Use this for initialization
	void Start () {
		Friendlies = 0;
		Ennemies = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col){
		if (col.tag == transform.parent.tag && (col.tag == "Ennemy" || col.tag == "Good")) {
			Friendlies++;
		} else if (col.tag != transform.parent.tag && (col.tag == "Ennemy" || col.tag == "Good")) {
			Ennemies++;
		}
	}

	void OnTriggerExit(Collider col){
		if (col.tag == transform.parent.tag && (col.tag == "Ennemy" || col.tag == "Good")) {
			Friendlies--;
		} else if (col.tag != transform.parent.tag && (col.tag == "Ennemy" || col.tag == "Good")) {
			Ennemies--;
		}
	}

	void OnTriggerStay(Collider col){
		if (col.tag != transform.parent.tag&&(col.tag=="Ennemy"||col.tag=="Good" )) {
//			Debug.Log("time to die");
			Target = col.transform;
		}
	}

}
