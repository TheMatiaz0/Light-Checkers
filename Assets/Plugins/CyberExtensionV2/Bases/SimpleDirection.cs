

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static Cyberevolver.Unity.SimpleDirection;
#pragma warning disable IDE0066
namespace Cyberevolver.Unity
{
	/// <summary>
	/// Represents straight direction. It can be converted to <see cref="Direction"/>
	/// </summary>
	[Serializable]
	public enum SimpleDirection
	{
		Empty = 0,
		Up = 1 << 0,
		Down = 1 << 1,
		Left = 1 << 2,
		Right = 1 << 3,
		LeftUp = Left + Up,
		LeftDown = Left + Down,
		RightUp = Right + Up,
		RightDown = Right + Down,

	}
	public static class SimpleDirectionExtension
	{
		/// <summary>
		/// Convert to <see cref="Direction"/>.
		/// </summary>
		/// <param name="simpleDirection"></param>
		/// <returns></returns>
		public static Direction ToDirection(this SimpleDirection simpleDirection)
		{
			switch (simpleDirection)
			{
				case Empty: return Direction.Empty;
				case Up: return Direction.Up;
				case Down: return Direction.Down;
				case Left: return Direction.Left;
				case Right: return Direction.Right;
				case LeftUp: return Direction.LeftUp;
				case LeftDown: return Direction.LeftDown;
				case RightUp: return Direction.RightUp;
				case RightDown: return Direction.RightDown;
				default: throw new ArgumentException("uknown argument", nameof(simpleDirection));
			};
		}
		/// <summary>
		/// It returns true if argument is <see cref="SimpleDirection.Down"/> or <see cref="SimpleDirection.Up"/>.
		/// </summary>
		/// <param name="dir"></param>
		/// <returns></returns>
		public static bool IsConnectedWithY(this SimpleDirection dir)
		{
			switch (dir)
			{
				case Up: case Down: return true;
				default: return false;
			}
		}
		/// <summary>
		/// It returns true if argument is <see cref="SimpleDirection.Left"/> or <see cref="SimpleDirection.Right"/>
		/// </summary>
		/// <param name="dir"></param>
		/// <returns></returns>
		public static bool IsConnectedWithX(this SimpleDirection dir)
		{
			return !IsConnectedWithY(dir);
		}
	}
}
