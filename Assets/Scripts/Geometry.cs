using UnityEngine;

public class Geometry
{
    public static Vector2Int GridPoint(int col, int row)
    {
        return new Vector2Int(col, row);
    }

    public static Vector3 PointFromGrid(Vector2Int gridPoint)
    {
        float x = -3.5f + 1.0f * gridPoint.x;
        float z = (-3.5f + 1.0f * gridPoint.y) + 0.028f;
        return new Vector3(x, 0, z);
    }

    public static Vector2Int GridFromPoint(Vector3 point)
    {
        int col = Mathf.FloorToInt(4.0f + point.x);
        int row = Mathf.FloorToInt(4.0f + point.z);
        return new Vector2Int(col, row);
    }
}
