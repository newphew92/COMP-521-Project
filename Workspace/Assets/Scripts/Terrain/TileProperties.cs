using UnityEngine;
using System.Collections;

public class TileProperties : MonoBehaviour 
{
	public enum RampCardinal {NONE, North, South, East, West}
	public float BaseHeat;
	public Vector2 Position;

	public bool Ramp = false;
	public RampCardinal RampBottom = RampCardinal.NONE;
	public RampCardinal RampTop = RampCardinal.NONE;
}
