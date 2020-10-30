using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField]
    private Material blackMaterial;
    [SerializeField]
    private Material whiteMaterial;
    [SerializeField]
    private Material selectedMaterial;

    public GameObject AddPiece(GameObject piecePrefab, int col, int row)
    {
        Vector2Int gridPoint = Geometry.GridPoint(col, row);
        GameObject newPiece = Instantiate(piecePrefab, Geometry.PointFromGrid(gridPoint),
            piecePrefab.transform.rotation, transform);
        return newPiece;
    }

    public void MovePiece(GameObject piece, Vector2Int gridPoint)
    {
        piece.transform.position = Geometry.PointFromGrid(gridPoint);
    }

    public void SelectPiece(GameObject piece)
    {
        MeshRenderer renderers = piece.GetComponentInChildren<MeshRenderer>();
        renderers.material = selectedMaterial;
    }

    public void DeselectPiece(GameObject piece)
    {
        MeshRenderer renderers = piece.GetComponentInChildren<MeshRenderer>();

        renderers.material = GetMaterialForTeam(piece.GetComponent<Piece>().CurrentTeam);
    }

    private Material GetMaterialForTeam (Team pieceTeam)
	{
        return (pieceTeam == Team.Black) ? blackMaterial : whiteMaterial;
    }
}
