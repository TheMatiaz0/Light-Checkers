using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cyberevolver.Unity
{
  
    public static class TransformExtension
    {

        public static void LookAt2D(this Transform transform, Vector3 target)
        {
            Vector3 dir = target - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

		public static void LookAtNorth2D (this Transform transform, Vector3 target)
		{
			Vector3 dir = target - transform.position;
			float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
		}
        public static IEnumerable<Transform> GetAllChild(this Transform transform)
        {
            return transform.OfType<Transform>();
        }
        public static Vector2 Get2DPos(this Transform transform)
        {
            return (Vector2)transform.position;
        }
        public static Vector2 Get2DScale(this Transform transform)
        {
            return (Vector2)transform.localScale;
        }
        public static void KillAllChild(this Transform transform)
        {
            foreach (Transform item in transform)
                UnityEngine.Object.Destroy(item.gameObject);
        }

        public static void KillAllChildExcept(this Transform transform, Func<Transform, bool> func)
        {
            foreach (Transform item in transform)
                if (func(item) == false)
                    UnityEngine.Object.Destroy(item.gameObject);
        }

    }

}
