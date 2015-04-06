using UnityEngine;
using System.Collections;

public class TerrainManager : MonoBehaviour 
{
	public enum AnalysisType{ Height, ViewDistance };

	public AnalysisType TypeOfAnalysisToUse = AnalysisType.Height;

	// used to render the colours properly on the tiles
	public bool RenderHeatOnTiles = true;
	public bool PutPlayerInfluenceInTiles = false; // allows for debugging and data collection -- may interfere with AI
	public float MaxHeat;
	public float PlayerCenterInfluence; // heat at center of influence
	public int PlayerInfluenceRadius; // 0 means the player only has influence on their square, 1 means 1 square away

	private HSBColor P1Color = HSBColor.FromColor(Color.red);
	private HSBColor P2Color = HSBColor.FromColor(Color.blue);

	public Transform[,] RawBoard;
	public TileProperties[,] AnalyzedBoard;
	public PlayerInfluenceMap PlayerInfluence; // no board influence, just the players

	// the bonuses must be >= 1
	private float HighGroundInfluenceBonus = 2f;

	private AbstractTerrainAnalyzer analyzer;
	private PlayerManager players;

	void Start()
	{
		InitializeAnalyzer ();
		// TODO: step 1: analyze the board and put in the raw values

		// initialization
		int rows = transform.childCount;
		int cols = transform.GetChild (0).childCount;

		RawBoard = new Transform[rows, cols];
		PlayerInfluence = new PlayerInfluenceMap(rows, cols, PlayerCenterInfluence, PlayerInfluenceRadius, HighGroundInfluenceBonus, RawBoard);

		UpdateRawBoardValues ();
		players = GetComponent<PlayerManager> ();
	}
	
	void Update()
	{
		PlayerInfluence.UpdatePlayerInfluenceMap(players.RedPlayer, players.BluePlayer);
		if( RenderHeatOnTiles )
		{
			RenderInfluenceMap();
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
				RawBoard[i,j] = row.GetChild(j);
			}
		}
	}

	// Renders the heat without player influence
	private void RenderInfluenceMap()
	{
		for(int i = 0; i < transform.childCount; i++)
		{
			Transform row = transform.GetChild(i);
			for( int j = 0; j < row.childCount; j++)
			{
				Transform tile = row.GetChild(j);

				Renderer rend = tile.GetComponent<Renderer>();
				float heat = PlayerInfluence.InfluenceMap[i,j];

				if(PutPlayerInfluenceInTiles)
					tile.GetComponent<TileProperties>().BaseHeat = heat;

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
