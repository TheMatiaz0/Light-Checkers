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


	private Piece lastPiece = null;

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
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out RaycastHit hitMark))
		{
			Vector3 point = hitMark.point;
			Vector2Int gridPoint = Geometry.GridFromPoint(point);
			highlightGeneralObj.SetActive(true);
			highlightGeneralObj.transform.position = Geometry.PointFromGrid(gridPoint);

			if (Input.GetMouseButtonDown(0))
			{
				Piece selectedPiece = GameManager.Instance.GetFriendlyPieceAtGrid(gridPoint);
				if (selectedPiece != null)
				{
					SelectPieceAndMoveOrKill(selectedPiece);
				}
			}
		}

		else
		{
			highlightGeneralObj.SetActive(false);
		}
	}

	public void DeactiveMoveHighlight ()
	{
		childForHighlightBlue.KillAllChild();
		childForHighlightRed.KillAllChild();
		GameManager.Instance.DeselectPiece(lastPiece);
	}

	private void SelectPieceAndMoveOrKill (Piece selectedPiece)
	{
		if (lastPiece != null)
		{
			DeactiveMoveHighlight();
		}

		GameManager.Instance.SelectPiece(selectedPiece);

		foreach (KeyValuePair<Vector2Int, Piece> pieceWithGrid in selectedPiece.GetAllCorrectDirections())
		{
			if (pieceWithGrid.Value != null)
			{
				CreateKillSelect(selectedPiece, pieceWithGrid.Key, pieceWithGrid.Value);
			}

			else
			{
				CreateMoveSelect(selectedPiece, pieceWithGrid.Key);
			}
		}

		lastPiece = selectedPiece;
	}

	public MoveSelector CreateMoveSelect (Piece selectedPiece, Vector2Int pointPosition)
	{
		MoveSelector move = Instantiate(highlightBluePrefab, Geometry.PointFromGrid(pointPosition), Quaternion.identity, childForHighlightBlue).GetComponent<MoveSelector>();
		move.Piece = selectedPiece;
		move.PointPosition = pointPosition;
		return move;
	}

	public KillSelector CreateKillSelect (Piece selectedPiece, Vector2Int pointPosition, Piece enemyPiece)
	{
		KillSelector kill = Instantiate(highlightRedPrefab, Geometry.PointFromGrid(pointPosition), Quaternion.identity, childForHighlightRed).GetComponent<KillSelector>();
		kill.Piece = selectedPiece;
		kill.PointPosition = pointPosition;
		kill.PieceToKill = enemyPiece;
		return kill;
	}
}
