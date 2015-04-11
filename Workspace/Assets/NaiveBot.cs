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
			Nav.SetDestination (Vector3.forward);
		}

	}
	void threatAssess(){
		if (transform.tag == "Red") {
			Enemies = Manager.BlueCount;
			Allies=Manager.RedCount-1;
			getClosestEnemy(Manager.Blues);
		} else {
			Enemies = Manager.RedCount;
			Allies=Manager.BlueCount-1;
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
					Debug.Log(list[i]);
				}
			}
		}
		
	}

}
