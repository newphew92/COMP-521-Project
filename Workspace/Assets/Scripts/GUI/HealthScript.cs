using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthScript : MonoBehaviour 
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
		float health = prop.HP * 100;
		text.text = "Power: " + health.ToString("0");
	}
}
