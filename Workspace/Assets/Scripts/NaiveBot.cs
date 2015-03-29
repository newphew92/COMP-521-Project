using UnityEngine;
using System.Collections;

public class NaiveBot : MonoBehaviour {
	public Transform Waypoint;
	public NavMeshAgent Nav;
	public NaiveManager Manager;
	public Vision Range;
	public Transform Target;
	public bool Seen;
	LineRenderer laser;
	public Level Field;
	float [,] Grid;
	public int Size;
	public int delay;
	public bool Lit;
	public int Friends;
	public int Ennemies;
	public UnitProperties Prop;
	// Use this for initialization
	void Start () {
		Prop = GetComponent<UnitProperties> ();
		delay=5;
		TerrainMaker terrain = new TerrainMaker ();
		Grid = terrain.Level1;
		Manager = GetComponentInParent<NaiveManager> ();
		laser= GetComponent<LineRenderer> ();
		if (transform.tag == "Good") {
			laser.material.color = Color.blue;
		} else {
			laser.material.color = Color.yellow;
		}
		laser.enabled = false;
		Nav = gameObject.GetComponent<NavMeshAgent> ();
		Range = GetComponentInChildren<Vision> ();
		Friends = Range.Friendlies;
		Ennemies = Range.Ennemies;
		InvokeRepeating ("patrol", 5, delay);
	}
	
	// Update is called once per frame
	void Update () {
		Target = Range.Target;
//		Debug.Log (Range.Target);
		Friends = Range.Friendlies;
		Ennemies = Range.Ennemies;
		if (Range.Target != null) {

			Shoot (Target);

			StopCoroutine("FireLaser");
			StartCoroutine("FireLaser");
			dummy ();
		}
		else{
//			CancelInvoke("dummy");
			Target=null;
		}
	}
	void dummy(){
//		Debug.Log (Range.TargPos);
		Prop.shoot (Vector3.Normalize(Range.TargPos-transform.position));
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
		int x=Random.Range (0, 11);
		int z=Random.Range (0, 11);
		float y=0;
		int heat=	(int)Grid[x,z];
		Vector3 temp;
		switch (heat) {
		case 0:
			y=0;
			heat=2;
			break;

		case 1:
			y=0.302f;
			heat=1;
			break;
		case 2:
			y=5f;
			heat=0;
			break;
		default:
			break;
		}
//		Debug.Log ("heatafter" + heat);
		int m=Random.Range (0, 11);
		if (m>=3&&m<5){m-=3;}
		else if (m>=5&&m<8){m+=3;}
		int n=Random.Range (4, 7);
		float h=0;
		int heaty=	(int)Grid[m,n];
		Debug.Log ("m" + m + "n" + n);
		Vector3 tempy;
		switch (heaty) {
		case 0:
			y=0;
			heaty=2;
			break;
			
		case 1:
			y=0.302f;
			heaty=1;
			break;
		case 2:
			y=5f;
			heaty=0;
			break;
		default:
			break;
		}

		tempy = new Vector3 (5*m, 5*h, 5*n);
		temp = new Vector3 (5*x, 5*y,5*z);
		if (destHeur (temp, heat) >= destHeur (tempy, heaty)) {
			if (friendHeur () < destHeur (temp, heat)) {
				Waypoint.position = temp;
				Nav.destination = Waypoint.position;
			}
			else{
				Nav.destination=Waypoint.position;
			}
		} else{
			Debug.Log("going to high");
			if (friendHeur () < destHeur (tempy, heaty)) {
				Waypoint.position = tempy;
				Nav.destination = Waypoint.position;
			}else{
				Nav.destination=Waypoint.position;}
		}
		Debug.Log ("friend" + friendHeur () + "temp"+destHeur (temp, heat)+"heaty"+ destHeur (tempy, heaty));

	}





	float friendHeur(){
		float min=100;
		int hold = 0;
		int i = 0;

		for ( i=0; i<Manager.Squad.Length; i++) {
			if (Manager.Squad[i]!=null){
			float dist = Mathf.Sqrt (Mathf.Pow ((transform.position.x - Manager.Squad [i].position.x), 2) +
			                         Mathf.Pow ((transform.position.x - Manager.Squad [i].position.x), 2));
			if (dist<min&&dist!=0){
				min=dist;
					hold=i;}}
		}
		Waypoint.position = Manager.Squad [hold].position;
		if (transform.position == Manager.Squad [hold].position) {
			return 0;
		}
			return min/300;
	}
	float destHeur(Vector3 pos, int hot){

		float dist = Mathf.Sqrt (Mathf.Pow ((transform.position.x - pos.x), 2) +
			Mathf.Pow ((transform.position.x - pos.x), 2));
		return (1/(dist +1/(hot+0.1f)));
		}
}
