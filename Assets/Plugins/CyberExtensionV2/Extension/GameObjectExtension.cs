using Cyberevolver;
using Cyberevolver.Unity;
using System.Linq;
using UnityEngine;

public static class GameObjectExtension
{
    public static RaycastHit2D Ray2DWithoutThis(this GameObject g, Vector2 from, Direction dir, float distance)
    {
        return Physics2D.RaycastAll(from, dir, distance)
            .FirstOrDefault(item => item.collider != null && item.collider.gameObject != g);
    }

    public static RaycastHit2D CircleCastWithoutThis(this GameObject g, Vector2 from, float radius, Direction dir, float distance)
    {
        return Physics2D.CircleCastAll(from, radius, dir, distance).FirstOrDefault(item => item.collider != null && item.collider.gameObject != g);
    }

    public static RaycastHit Ray3DWithoutThis(this GameObject g, Vector3 from, Vector3 dir, float? distance = null)
	{
        return Physics.RaycastAll(from, dir, distance.Value)
            .FirstOrDefault(item => item.collider != null && item.collider.gameObject != g);
	}

    public static RaycastHit Ray3DWithoutThis(this GameObject g, Ray ray)
	{
        return Physics.RaycastAll(ray)
            .FirstOrDefault(item => item.collider != null && item.collider.gameObject != g);
	}
}