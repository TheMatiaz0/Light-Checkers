using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.Globalization;
namespace Cyberevolver.Unity
{
    public static class ColorExtension
    {
        /// <summary>
        /// Parse color from text. Text should be like from <see cref="Color.ToString"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Color Parse(string value)
        {
            value = value.Replace("RGBA", "");
            value = value.Replace(" ", "");
            value = value.Replace("(", "");
            value = value.Replace(")", "");
            string[] split = value.Split(',');
            float r = ParseInvariant(split[0]);
            float g = ParseInvariant(split[1]);
            float b = ParseInvariant(split[2]);
            float a = ParseInvariant(split[3]);
            return new Color(r, g, b, a);
        }
        /// <summary>
        /// Parsing class rgb text, for example :"255 255 255 255".
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Color ParseClasic(string value)
        {
            List<float> splited = value.Split().Select(float.Parse).ToList();
            while(splited.Count<4)
            {
                splited.Add(1);
            }
            return new Color(splited[0]*255, splited[1]*255, splited[2]*255, splited[3]*255);
        }
        private static float ParseInvariant(string text)
        {
            return float.Parse(text, CultureInfo.InvariantCulture);
        }

    }
}
