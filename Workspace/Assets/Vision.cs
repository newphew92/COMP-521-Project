using UnityEngine;
using System.Collections;

public class Vision : MonoBehaviour {
	public Transform Target;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}



	void OnTriggerStay(Collider col){
		if (col.tag == "Ennemy") {
			Debug.Log("time to die");
			Target = col.transform;
		}
	}

}
