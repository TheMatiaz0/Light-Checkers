using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.Globalization;
namespace Cyberevolver.Unity
{
    public static class Vector2Extension
    {
        /// <summary>
        /// Rotating vector.
        /// </summary>
        /// <param name="vect"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static Vector2 Rotate(this Vector2 vect, float angle)
        {
            float radianAngle = angle * Mathf.Deg2Rad;
            float x = vect.x * Mathf.Cos(radianAngle) - vect.y * Mathf.Sin(radianAngle);
            float y = vect.x * Mathf.Sin(radianAngle) + vect.y * Mathf.Cos(radianAngle);
            return new Vector2((float)x, (float)y);
        }
        public static Direction ToDirection(this Vector2 vect)
        {
            return new Direction(vect);
        }
        /// <summary>
        /// Parsing from text.
        /// </summary>
        /// <exception cref="FormatException"></exception>
        /// <param name="text"></param>
        /// <returns></returns>
        public static Vector2 Parse(string text)
        {
            text = text.Replace("(", "").Replace(")", "").Replace(" ", "");
            string[] split = text.Split(',');
            return new Vector2(float.Parse(split[0], CultureInfo.InvariantCulture),
                float.Parse(split[1], CultureInfo.InvariantCulture));
        }
    }
}
