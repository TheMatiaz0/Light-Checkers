using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.IO;
namespace Cyberevolver.Unity
{
    /// <summary>
    /// Sprite load interface.
    /// </summary>
    public static class SpriteLoad
    {
        public static Sprite LoadSprite(string path, out byte[] bytesValue)
        {
            bytesValue = File.ReadAllBytes(path);
            return LoadSprite(bytesValue);
        }
        public static Sprite LoadSprite(byte[] value)
        {
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(value);
            return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0, 0), 100);
        }
    }
}
