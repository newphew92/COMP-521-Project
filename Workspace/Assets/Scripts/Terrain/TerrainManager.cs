using UnityEngine;
using System.Collections;

public class TerrainManager : MonoBehaviour 
{
	public enum AnalysisType{ Height, ViewDistance };

	public AnalysisType TypeOfAnalysisToUse = AnalysisType.Height;

	// used to render the colours properly on the tiles
	public bool RenderHeatOnTiles = true;
	public float MaxHeat;
	public float PlayerCenterInfluence; // heat at center of influence
	public int PlayerInfluenceRadius; // 0 means the player only has influence on their square, 1 means 1 square away

	private HSBColor P1Color = HSBColor.FromColor(Color.red);
	private HSBColor P2Color = HSBColor.FromColor(Color.blue);

	public float[,] RawBoard;
	public TileProperties[,] AnalyzedBoard;
	public PlayerInfluenceMap PlayerInfluence; // no board influence, just the players

	private float RampInfluenceBonus = 1.5f; // for being on a ramp going up above
	private float HighGroundInfluenceBonus = 2f; // for being on the floor above that ramp

	private AbstractTerrainAnalyzer analyzer;
	private PlayerManager players;

	void Start()
	{
		InitializeAnalyzer ();
		// TODO: step 1: analyze the board and put in the raw values

		// initialization
		int rows = transform.childCount;
		int cols = transform.GetChild (0).childCount;

		RawBoard = new float[rows, cols];
		PlayerInfluence = new PlayerInfluenceMap(rows, cols, PlayerCenterInfluence, PlayerInfluenceRadius);

		UpdateRawBoardValues ();
		players = GetComponent<PlayerManager> ();
	}
	
	void Update()
	{
		PlayerInfluence.UpdatePlayerInfluenceMap(players.RedPlayer, players.BluePlayer);
		if( RenderHeatOnTiles )
		{
			RenderRawHeat();
		}
	}

	private void InitializeAnalyzer()
	{
		if (TypeOfAnalysisToUse == AnalysisType.Height)
			analyzer = GetComponent<HeightAnalyzer> ();
		else if (TypeOfAnalysisToUse == AnalysisType.ViewDistance )
			analyzer = GetComponent<ViewDistanceAnalyzer> ();
	}

	// each index has a bunch of tiles which form the choke point
	public Transform[][] GetChokePoints()
	{
		return analyzer.GetChokePoints ();
	}

	private void UpdateRawBoardValues()
	{
		for(int i = 0; i < transform.childCount; i++)
		{
			Transform row = transform.GetChild(i);
			for( int j = 0; j < row.childCount; j++)
			{
				Transform tile = row.GetChild(j);
				RawBoard[i,j] = tile.GetComponent<TileProperties>().BaseHeat;
			}
		}
	}

	// Renders the heat without player influence
	private void RenderRawHeat()
	{
		for(int i = 0; i < transform.childCount; i++)
		{
			Transform row = transform.GetChild(i);
			for( int j = 0; j < row.childCount; j++)
			{
				Transform tile = row.GetChild(j);
				TileProperties tProp = tile.GetComponent<TileProperties>();

				Renderer rend = tile.GetComponent<Renderer>();
				float heat = PlayerInfluence.InfluenceMap[i,j];
				if(heat >= 0)
				{
					P1Color.s = (heat/MaxHeat);
					rend.material.color = P1Color.ToColor();
				}
				else if(heat < 0)
				{
					P2Color.s = Mathf.Abs(heat/MaxHeat);
					rend.material.color = P2Color.ToColor();
				}
			}
		}
	}
}
