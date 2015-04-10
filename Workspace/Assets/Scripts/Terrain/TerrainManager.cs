using UnityEngine;
using System.Collections;

public class TerrainManager : MonoBehaviour 
{
	public enum AnalysisType{ HardCodedValues, Height, ViewDistance, ShootingDistance };

	public AnalysisType TypeOfAnalysisToUse = AnalysisType.Height;

	// used in terrain analysis
	public float maxTerrainHeat; // must be > 0

	// used to render the colours properly on the tiles
	public bool RenderHeatOnTiles = true;
	public bool PutPlayerInfluenceInTiles = false; // allows for debugging and data collection -- may interfere with AI
	public float MaxHeat;
	public float PlayerCenterInfluence; // heat at center of influence
	public int PlayerInfluenceRadius; // 0 means the player only has influence on their square, 1 means 1 square away

	private HSBColor P1Color = HSBColor.FromColor(Color.red);
	private HSBColor P2Color = HSBColor.FromColor(Color.blue);

	public Transform[,] RawBoard;
	public PlayerInfluenceMap PlayerInfluence; // the full, complete representation, players and all

	// the bonuses must be >= 1
	private float HighGroundInfluenceBonus = 2f;

	private AbstractTerrainAnalyzer analyzer;
	private PlayerManager players;

	void Start()
	{
		int rows = transform.childCount;
		int cols = transform.GetChild (0).childCount;
		RawBoard = new Transform[rows, cols];
		UpdateRawBoardValues ();

		if( TypeOfAnalysisToUse != AnalysisType.HardCodedValues)
			AnalyzeTerrain ();

		PlayerInfluence = new PlayerInfluenceMap(rows, cols, PlayerCenterInfluence, PlayerInfluenceRadius, HighGroundInfluenceBonus, RawBoard);
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

	private void AnalyzeTerrain()
	{
		if (TypeOfAnalysisToUse == AnalysisType.Height)
			analyzer = gameObject.AddComponent<HeightAnalyzer>();
		else if (TypeOfAnalysisToUse == AnalysisType.ViewDistance || TypeOfAnalysisToUse == AnalysisType.ShootingDistance)
		{
			analyzer = gameObject.AddComponent<ViewDistanceAnalyzer>();
			if(TypeOfAnalysisToUse == AnalysisType.ViewDistance)
				GetComponent<ViewDistanceAnalyzer>().UnitViewRadius = 99999;
			else // shooting distance
				GetComponent<ViewDistanceAnalyzer>().UnitViewRadius = PlayerInfluenceRadius;
		}

		analyzer.level = RawBoard;
		analyzer.maxTerrainHeat = maxTerrainHeat; // must be > 0
		analyzer.AnalyzeTerrain ();
		RawBoard = analyzer.level;

		DetermineEdges ();
	}

	private void DetermineEdges()
	{
		for(int i = 0; i < RawBoard.GetLength(0); i++)
		{
			for(int j = 0; j < RawBoard.GetLength(1); j++)
			{
				TileProperties[] neighbours = GetBoardNeighbours(i,j);

				float height = RawBoard[i,j].position.y;

				bool isRamp = RawBoard[i,j].GetComponent<TileProperties>().Ramp;
				if(isRamp) continue;

				for(int x = 0; x < neighbours.Length; x++)
				{
					if(neighbours[x] == null) continue;

					Vector2 p = neighbours[x].Position;
					if(!neighbours[x].Ramp && RawBoard[(int) p.x, (int) p.y].position.y < height)
					{
						RawBoard[i,j].GetComponent<TileProperties>().Edge = true;
						break;
					}
				}
			}
		}
	}

	private TileProperties[] GetBoardNeighbours(int i, int j)
	{
		TileProperties[] ret = new TileProperties[8];
		int indexCounter = 0;
		for(int x = i-1; x <= i+1; x++)
		{
			for(int y = j-1; y <= j+1; y++)
			{
				if(x==i && y==j) continue;

				if(x >= 0 && x < RawBoard.GetLength(0))
					if(y >= 0 && y < RawBoard.GetLength(1))
						ret[indexCounter] = RawBoard[x,y].GetComponent<TileProperties>();
				indexCounter++;
			}
		}
		return ret;
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
