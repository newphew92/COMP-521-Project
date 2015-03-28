using UnityEngine;
using System.Collections;

public class NaiveBot : MonoBehaviour {
	public Transform Waypoint;
	public NavMeshAgent Nav;
	public Vision Range;
	public Transform Target;
	public bool Seen;
	LineRenderer laser;
	// Use this for initialization
	void Start () {
		laser= GetComponent<LineRenderer> ();
		if (transform.tag == "Good") {
			laser.material.color = Color.blue;
		} else {
			laser.material.color = Color.yellow;
		}
		laser.enabled = false;
		Nav = gameObject.GetComponent<NavMeshAgent> ();
		Range = GetComponentInChildren<Vision> ();
		InvokeRepeating ("patrol", 5, 5);
	}
	
	// Update is called once per frame
	void Update () {
		Target = Range.Target;
		if (Range.Target != null&&Seen) {

			Shoot (Target);

			StopCoroutine("FireLaser");
			StartCoroutine("FireLaser");
			InvokeRepeating("Shooting",1,1);
		}
		else{
			CancelInvoke("Shooting");
			InvokeRepeating ("patrol", 5, 5);
			Target=null;
		}
	}

	void Shoot(Transform tar){
		RaycastHit hit;
		Ray ray = new Ray (transform.position, tar.position - transform.position);
		if (Physics.Raycast (ray, out hit, 400)) {
			Target = hit.transform;
			Seen=true;
			Debug.Log("bam");
//			Seen = (hit.transform.tag == "Player");
		} else {
			Target = null;
		}
	}

	void Shooting(){
//		InvokeRepeating("Shot",0.2f,1);	
		if (laser.enabled == true) {
			laser.enabled = false;
		} else {
			laser.enabled = true;
		}

	}

	IEnumerator FireLaser(){
		laser.SetPosition(0,transform.position+Vector3.up*5+Vector3.left*2.5f);
		laser.SetPosition (1, Target.position+Vector3.up*5+Vector3.right*1.5f);
		yield return null;
	}

		void patrol(){
		int x=Random.Range (-25, 25);
		int z=Random.Range (-25, 25);

		Waypoint.position = new Vector3 (transform.position.x+x, transform.position.y, transform.position.z+z);
		Nav.destination = Waypoint.position;
	}
}
