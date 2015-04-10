using UnityEngine;
using System.Collections;
	
public class UnitContoller : MonoBehaviour {
	public float Speed;
	public NavMeshAgent Nav;
	public Transform Destination;

	public Transform[] Reds;
	public Transform[] Blues;

	public float curHeat;
	public Vector3 bestPos = new Vector3 ();

	public float Sign = -1;
	public CastDown tileCheck;
	public bool Scanning;
	// Use this for initialization
	void Start () {
		Scanning = true;
		tileCheck = gameObject.GetComponentInChildren<CastDown> ();
		Nav = gameObject.GetComponent<NavMeshAgent> ();
		Nav.acceleration = 9999999f;
		Destination= transform;
		bestPos = transform.position + 10000 * Vector3.forward +1000*Vector3.left;
		InvokeRepeating ("changeSign", 0, 5);
		transform.Rotate (Vector3.up * -70);
//		Debug.Log (bestPos);
//		See ();
//		InvokeRepeating ("Move", 0, 1);


	}
	
	// Update is called once per frame
	void Update () {

		if (Scanning) {
			Scan ();
		}
		checkSelfHeat ();
		Move ();
		See ();
	}
	void changeSign(){
		Sign *= -1;
	}

	void Move(){
		if (tileCheck.potentialTile != null && tileCheck.floorHeat <= curHeat) {
//			Scanning=false;
			Nav.SetDestination (tileCheck.potentialTile.position);
		} 
//		else{
//			Debug.Log("resuming");
//			Scanning=true;}
	}

	void checkSelfHeat(){
		RaycastHit hit;
		Ray ray = new Ray (transform.position+Vector3.up, Vector3.down);
		if (Physics.Raycast (ray, out hit, 3)) {
//					Debug.Log(hit.collider.gameObject.GetComponent<TileProperties>().BaseHeat+" "+hit.collider.name);
//				hit.collider.gameObject.GetComponent<Renderer>().material.color=Color.black;
				curHeat=hit.collider.gameObject.GetComponent<TileProperties>().BaseHeat;
		} 
	}

	void Scan(){
		float gradient= Sign*Time.deltaTime;
		transform.Rotate (Vector3.up * gradient*28);
	}

	void See(){
		RaycastHit hit;
//		LayerMask mask = ~(1 << LayerMask.NameToLayer ());
		Ray ray = new Ray (transform.position, Vector3.forward);
		if (Physics.Raycast (ray, out hit, 3)) {
			if (hit.collider.tag=="Wall"){
				Debug.Log("walling"+transform.eulerAngles);
				transform.Rotate (180*Vector3.up);
				Debug.Log(transform.eulerAngles);
			}
		}
	}
}
