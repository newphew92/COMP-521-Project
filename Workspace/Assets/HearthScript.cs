using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HearthScript : MonoBehaviour 
{
	public Transform Master;
	private UnitProperties prop;
	private Text text;

	// Use this for initialization
	void Start () {
		prop = Master.GetComponent<UnitProperties> ();
		text = gameObject.GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		text.text = "Health: " + prop.HP;
	}
}
