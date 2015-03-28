using UnityEngine;
using System.Collections;

public class NaiveBot : MonoBehaviour {
	public Transform Waypoint;
	public NavMeshAgent Nav;
	public Vision Range;
	public Transform Target;
	// Use this for initialization
	void Start () {
		Nav = gameObject.GetComponent<NavMeshAgent> ();
		Range = GetComponentInChildren<Vision> ();
		InvokeRepeating ("patrol", 5, 5);
	}
	
	// Update is called once per frame
	void Update () {
		if (Range.Target != null) {
			Target = Range.Target;
		}
	}

	void Shoot(){
		RaycastHit hit;
		Ray ray = new Ray (transform.position, Target.position - transform.position);
		if (Physics.Raycast (ray, out hit, 400)) {
			Debug.Log("bam");
//			Seen = (hit.transform.tag == "Player");
		}
	}
	void patrol(){
		int x=Random.Range (-25, 25);
		int z=Random.Range (-25, 25);

		Waypoint.position = new Vector3 (transform.position.x+x, transform.position.y, transform.position.z+z);
		Nav.destination = Waypoint.position;
	}
}
