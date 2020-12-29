using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Cyberevolver.Unity
{
    public static class GraphicsExtension
    {
        /// <summary>
        /// Just setting alpha value.
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="alphaValue"></param>
        public static void SetAlpha(this Graphic graphics, float alphaValue)
        {
            Color color = graphics.color;
            color.a = alphaValue;
            graphics.color = color;
        }
    }
}
