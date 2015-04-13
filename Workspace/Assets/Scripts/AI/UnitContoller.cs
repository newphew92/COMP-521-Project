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

	public SquadManager Manager;

	public int Enemies;
	public int Allies;

	public Transform ClosestEnemy;
	// Use this for initialization
	void Start () {
		Manager = gameObject.GetComponentInParent<SquadManager> ();
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
		Scan ();
		checkSelfHeat ();
		See ();
		threatAssess ();
		Move ();	
	}
	void changeSign(){
		Sign *= -1;
	}

	void Move(){
		if (Enemies < Allies) {
			Nav.SetDestination(ClosestEnemy.position);
//			Scanning=false;
			Debug.Log("going");
		}
		else if (tileCheck.potentialTile != null && tileCheck.floorHeat <= curHeat) {
//			Scanning=false;
			Nav.SetDestination (tileCheck.potentialTile.position);
		} 
		else{
//			Debug.Log("resuming");
//			Scanning=true;
			Nav.SetDestination(transform.position+Vector3.forward);}
	}

	void checkSelfHeat(){
		RaycastHit hit;
		Ray ray = new Ray (transform.position+Vector3.up, Vector3.down);
		if (Physics.Raycast (ray, out hit, 3)) {
//					Debug.Log(hit.collider.gameObject.GetComponent<TileProperties>().BaseHeat+" "+hit.collider.name);
//				hit.collider.gameObject.GetComponent<Renderer>().material.color=Color.black;
			if (hit.transform!=null&&(hit.transform.tag!="Red"||hit.transform.tag!="Blue")){	
				curHeat=hit.collider.gameObject.GetComponent<TileProperties>().BaseHeat;}
		} 
	}

	void Scan(){
		float gradient= Sign*Time.deltaTime;
		transform.Rotate (Vector3.up * gradient*28);
	}
	void getClosestEnemy(Transform[] list){
		float min = 1000;
		for (int i=0; i<list.Length; i++) {
			if (list[i]!=null){
			float dist=Vector3.Distance(transform.position,list[i].position);
			if (dist<min){
				min=dist;
				ClosestEnemy=list[i];
//				Debug.Log(list[i]);
			}
			}
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
	void See(){
		RaycastHit hit;
//		LayerMask mask = ~(1 << LayerMask.NameToLayer ());
		Ray ray = new Ray (transform.position+0.5f*Vector3.forward, Vector3.forward);
		if (Physics.Raycast (ray, out hit, 1)) {
			if (hit.transform.tag=="Wall"||hit.transform.tag==transform.tag){
//				Debug.Log("walling"+transform.eulerAngles+" "+gameObject.name);
				transform.Rotate (180*Vector3.up-transform.eulerAngles);
//				Debug.Log(transform.eulerAngles);
			}
		}
		if (Physics.Raycast (ray, out hit, 5)) {
			if (hit.transform.tag=="Red"||hit.transform.tag=="Blue"&&hit.transform.tag!=transform.tag){
				Debug.Log ("target spotted");}
		}
	}
}
