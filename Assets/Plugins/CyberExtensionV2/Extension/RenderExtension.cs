using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace Cyberevolver.Unity
{
    public static class RenderCompatibile
    {
        /// <summary>
        /// Just returning color.
        /// </summary>
        /// <param name="render"></param>
        /// <returns></returns>
        public static Color GetColor(this Renderer render)
        {
            if (render is SpriteRenderer)
                return (render as SpriteRenderer).color;
            else if (render is MeshRenderer)
                return (render as MeshRenderer).material.color;
            throw new ArgumentException("Not supported type");
        }
        /// <summary>
        /// Just seting color.
        /// </summary>
        /// <param name="render"></param>
        /// <param name="value"></param>
        public static void SetColor(this Renderer render, Color value)
        {
            if (render is SpriteRenderer)
            {
                (render as SpriteRenderer).color = value;

            }
            else if (render is MeshRenderer)
                (render as MeshRenderer).material.color = value;
        }

    }
}
