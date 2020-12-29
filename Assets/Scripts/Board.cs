using UnityEngine;

public class Board : MonoBehaviour
{
	[SerializeField]
	private Material selectedMaterial = null;
	private Material oldMaterial = null;

	[SerializeField]
	private Material darkMaterial = null;
	[SerializeField]
	private Material lightMaterial = null;

	[SerializeField]
	private GameObject boardCubePrefab = null;

	protected void Awake()
	{
		GenerateBoard();
	}

	private void GenerateBoard()
	{
		for (int i = 0; i < GameManager.Height; i++)
		{
			GenerateLine(i);
		}
	}

	private void GenerateLine(int row)
	{
		for (int col = 0; col < GameManager.Width; col++)
		{
			if ((col + row) % 2 == 1)
			{
				SpawnField(Geometry.PointFromGrid(new Vector2Int(col, row)), lightMaterial);

			}

			else
			{
				SpawnField(Geometry.PointFromGrid(new Vector2Int(col, row)), darkMaterial);
			}
		}
	}

	private GameObject SpawnField(Vector3 position, Material mat)
	{
		position = new Vector3(position.x, -0.029f, position.z);

		GameObject gObj = Instantiate(boardCubePrefab, position, Quaternion.identity, this.transform);

		gObj.GetComponent<MeshRenderer>().material = mat;

		return gObj;
	}

	public Piece AddPiece(Piece piecePrefab, int col, int row)
	{
		Vector2Int gridPoint = Geometry.GridPoint(col, row);
		Piece newPiece = Instantiate(piecePrefab, Geometry.PointFromGrid(gridPoint),
			piecePrefab.transform.rotation, transform);
		return newPiece;
	}

	public void MovePiece(Piece piece, Vector2Int gridPoint)
	{
		piece.transform.position = Geometry.PointFromGrid(gridPoint);
	}

	public void SelectPiece(Piece piece)
	{
		MeshRenderer renderers = piece.GetComponentInChildren<MeshRenderer>();
		oldMaterial = renderers.material;
		renderers.material = selectedMaterial;
	}

	public void DeselectPiece(Piece piece)
	{
		MeshRenderer renderers = piece?.GetComponentInChildren<MeshRenderer>();

		if (renderers != null)
		{
			renderers.material = oldMaterial;
		}
	}
}
