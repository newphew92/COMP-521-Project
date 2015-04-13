using UnityEngine;
using System.Collections;

public class NaiveBot : MonoBehaviour {
	public NavMeshAgent Nav;
	public SquadManager Manager;
	
	public int Enemies;
	public int Allies;
	public Transform ClosestEnemy;

	public bool Cheat;
	// Use this for initialization
	void Start () {
		Cheat = false;
		Nav = gameObject.GetComponent<NavMeshAgent> ();
		Manager = gameObject.GetComponentInParent<SquadManager> ();
		Nav.acceleration = 9999999f;
	}
	
	// Update is called once per frame
	void Update () {
		threatAssess ();
		if (Cheat) {
			Nav.SetDestination (ClosestEnemy.position);
		} else {

			Move ();
//			Debug.Log("no cheat");
		}

	}
	void Move(){
		if (Enemies < Allies) {
			Nav.SetDestination(ClosestEnemy.position);
			//			Scanning=false;
//			Debug.Log("going");
		}

		else{
			//			Debug.Log("resuming");
			//			Scanning=true;
//			Debug.Log("onward");
			Nav.SetDestination(transform.position + transform.forward);
//			Nav.SetDestination(transform.position+3*Vector3.forward);
		}
	}
	void threatAssess(){
		if (transform.tag == "Red") {
			Enemies = Manager.BlueCount;
			Allies=Manager.RedCount;
			getClosestEnemy(Manager.Blues);
		} else {
			Enemies = Manager.RedCount;
			Allies=Manager.BlueCount;
			getClosestEnemy(Manager.Reds);
		}
	}
	void getClosestEnemy(Transform[] list){
		float min = 1000;
		for (int i=0; i<list.Length; i++) {
			if (list[i]!=null){
				float dist=Vector3.Distance(transform.position,list[i].position);
				if (dist<min){
					min=dist;
					ClosestEnemy=list[i];
//					Debug.Log(list[i]);
				}
			}
		}
		
	}

}
