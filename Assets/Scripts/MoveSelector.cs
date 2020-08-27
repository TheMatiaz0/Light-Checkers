using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSelector : MonoBehaviour
{
	public Piece Piece { get; set; }
	public Vector2Int PointPosition { get; set; }

	protected virtual void OnMouseDown()
	{
		if (GameManager.Instance.ShouldMove == false)
		{
			return;
		}

		Move();
		ChangeTurn();
	}

	protected void Move ()
	{
		GameManager.Instance.Move(Piece, PointPosition);
	}

	protected void ChangeTurn ()
	{
		GameManager.Instance.ChangeTurn();
	}
}
