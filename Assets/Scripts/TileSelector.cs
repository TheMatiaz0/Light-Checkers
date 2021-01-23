using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cyberevolver;
using Cyberevolver.Unity;
using System.Linq;

public class TileSelector : MonoBehaviour
{
	[SerializeField]
	private GameObject highlightYellowPrefab = null;

	[SerializeField]
	private GameObject highlightRedPrefab = null;

	[SerializeField]
	private GameObject highlightBluePrefab = null;

	private GameObject highlightGeneralObj = null;

	[SerializeField]
	private Transform childForHighlightBlue = null;
	[SerializeField]
	private Transform childForHighlightRed = null;

	[SerializeField]
	private Camera cam = null;


	private Piece lastPiece = null;

	public bool InputActive { get; set; } = true;

	public static TileSelector Instance { get; private set; }

	protected void Awake()
	{
		Instance = this;
	}

	protected void Start()
	{
		Vector2Int gridPoint = Geometry.GridPoint(0, 0);
		Vector3 point = Geometry.PointFromGrid(gridPoint);
		highlightGeneralObj = Instantiate(highlightYellowPrefab, point, Quaternion.identity, transform);
		highlightGeneralObj.SetActive(false);
	}

	protected void Update()
	{
		Selection();
	}

	private void Selection ()
	{
		if (!InputActive)
		{
			return;
		}

		Ray ray = cam.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out RaycastHit hitMark))
		{
			Vector3 point = hitMark.point;
			Vector2Int gridPoint = Geometry.GridFromPoint(point);
			highlightGeneralObj.SetActive(true);
			highlightGeneralObj.transform.position = Geometry.PointFromGrid(gridPoint);

			// Leave the AI choice
			if (!(GameManager.Instance.CurrentPlayer is NotPlayer))
			{
				// Select your piece, if it isn't null then move or kill.
				if (Input.GetMouseButtonDown(0))
				{
					Piece selectedPiece = GameManager.Instance.GetFriendlyPieceAtGrid(gridPoint);
					if (selectedPiece != null)
					{
						SelectPieceAndMoveOrKill(selectedPiece);
					}
				}
			}
		}


		else
		{
			highlightGeneralObj.SetActive(false);       // outside of board
		}
	}

	public void DeactiveAnyHighlight ()
	{
		childForHighlightBlue.KillAllChild();
		childForHighlightRed.KillAllChild();
		GameManager.Instance.DeselectPiece(lastPiece);
	}

	/// <summary>
	/// After selected piece...
	/// </summary>
	/// <param name="selectedPiece"></param>
	public MoveSelector[] SelectPieceAndMoveOrKill (Piece selectedPiece, bool shouldMove = true)
	{
		if (lastPiece != null)
		{
			DeactiveAnyHighlight();
		}

		GameManager.Instance.SelectPiece(selectedPiece);
		List<MoveSelector> allSelectors = new List<MoveSelector>();
		// Create move or kill selection grid:
		foreach (KeyValuePair<Vector2Int, Piece> pieceWithGrid in selectedPiece.GetAllCorrectDirections())
		{
			if (pieceWithGrid.Value != null)
			{				
				allSelectors.Add(CreateKillSelect(selectedPiece, pieceWithGrid.Key, pieceWithGrid.Value));
			}

			else
			{
				if (shouldMove)
				{
					allSelectors.Add(CreateMoveSelect(selectedPiece, pieceWithGrid.Key));
				}
			}
		}

		lastPiece = selectedPiece;
		return allSelectors.ToArray();
	}

	private MoveSelector CreateMoveSelect (Piece selectedPiece, Vector2Int pointPosition)
	{
		MoveSelector move = Instantiate(highlightBluePrefab, Geometry.PointFromGrid(pointPosition), Quaternion.identity, childForHighlightBlue).GetComponent<MoveSelector>();
		move.Piece = selectedPiece;
		move.PointPosition = pointPosition;
		return move;
	}

	private KillSelector CreateKillSelect (Piece selectedPiece, Vector2Int pointPosition, Piece enemyPiece)
	{
		KillSelector kill = Instantiate(highlightRedPrefab, Geometry.PointFromGrid(pointPosition), Quaternion.identity, childForHighlightRed).GetComponent<KillSelector>();
		kill.Piece = selectedPiece;
		kill.PointPosition = pointPosition;
		kill.PieceToKill = enemyPiece;
		return kill;
	}
}
