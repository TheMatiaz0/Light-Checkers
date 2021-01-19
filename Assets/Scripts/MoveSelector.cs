using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSelector : MonoBehaviour
{
	[SerializeField]
	private Material materialToChange = null; 

	public Piece Piece { get; set; }
	public Vector2Int PointPosition { get; set; }

	protected virtual void OnMouseDown()
	{
		if (GameManager.Instance.CurrentPlayer is NotPlayer)
		{
			return;
		}

		ClickSelection();
	}

	public virtual MoveSelector[] ClickSelection ()
	{
		if (GameManager.Instance.ShouldMove == false)
		{
			foreach (Piece item in GameManager.Instance.WarPieces)
			{
				if (item.MeshRenderer != null)
				{
					item.MeshRenderer.material = materialToChange;
				}
			}

			return null;
		}

		Move();
		ChangeTurn();
		return null;
	}

	protected void Move ()
	{
		GameManager.Instance.MovePiece(Piece, PointPosition);
	}

	protected void ChangeTurn ()
	{
		GameManager.Instance.ChangeTurn();
	}
}
