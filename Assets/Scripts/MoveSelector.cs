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
		if (GameManager.Instance.ShouldMove == false)
		{
			foreach (var item in GameManager.Instance.WarPieces)
			{
				item.MeshRenderer.material = materialToChange;
			}
			// rozjaśnij pionka, który musi wykonać atak.
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
