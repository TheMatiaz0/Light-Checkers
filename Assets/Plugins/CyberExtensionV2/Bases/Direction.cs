
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace Cyberevolver.Unity
{

    /// <summary>
    /// Represent direction. Is better than <see cref="Vector2"/>, because it is always normlized.
    /// It can be convert to <see cref="SimpleDirection"/>.
    /// </summary>
    [Serializable]

    public struct Direction : IEquatable<Direction>
    {

      

        /// <summary>
        /// Empty <see cref="Direction"/> value. You can use this as default parametr.
        /// It is more readable than creating new instance by constructor.
        /// </summary>
        public static readonly Direction Empty = new Direction();
        private const string BadValueException = "Argument cannot be different than -1 to 1, if you want to set normalized value, you should use constructor";
        private const string OutOfRangeException = "This array size is range of 0-1";

        [SerializeField]

        private Vector2 _Value;
        public float X
        {
            get => _Value.x;
            set
            {
                if (Math.Abs(value) <= 1.0)
                    _Value.x = value;
                else throw new ArgumentException(BadValueException);
            }
        }
        public float Y
        {
            get => _Value.y;
            set
            {
                if (Math.Abs(value) <= 1.0)
                    _Value.y = value;
                else throw new ArgumentException(BadValueException);
            }
        }
        /// <summary>
        /// If you give here 0, you will get <see cref="X"/> value.
        /// If you give here 1, you will get <see cref="Y"/> value.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public float this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return X;
                    case 1: return Y;
                    default:
                        throw new IndexOutOfRangeException(OutOfRangeException);
                }          
            }                  
            set
            {
                switch (index)
                {
                    case 0:
                        X = value;
                        break;
                    case 1:
                        Y = value;
                        break;
                    default: throw new IndexOutOfRangeException(OutOfRangeException);
                }
            }

        }

        public static readonly Direction
            Up = new Direction(0, 1),
            Down = new Direction(0, -1),
            Left = new Direction(-1, 0),
            Right = new Direction(1, 0),
            LeftUp = Up + Left,
            LeftDown = Down + Left,
            RightUp = Up + Right,
            RightDown = Down + Right;
      
        
        public Direction(float x, float y)
        {
            var vect = new Vector2(x, y);

            vect.Normalize();
            _Value.x = vect.x;
            _Value.y = vect.y;
        }
        /// <summary>
        /// Vector will be normalized
        /// </summary>
        /// <param name="v"></param>
        public Direction(Vector2 v) : this(v.x, v.y)
        {

        }

        public Vector2 ToVector2()
            => new Vector2(X, Y);
        /// <summary>
        /// If converting fail, you will get <see cref="null"/>
        /// </summary>
        /// <param name="minimalRound"></param>
        /// <returns></returns>
        public SimpleDirection? TryToSimpleDirection()
        {

            if (_Value == Vector2.zero)
                return SimpleDirection.Empty;
           



            SimpleDirection? xOptions = null;
            SimpleDirection? yOptions = null;

            if (X > 0)
                xOptions = SimpleDirection.Right;
            else if (X < 0)
                xOptions = SimpleDirection.Left;
            if (Y > 0)
                yOptions = SimpleDirection.Up;
            else if (Y < 0)
                yOptions = SimpleDirection.Down;
        
            
            if(xOptions==null&&yOptions==null)
                return null;

            if (yOptions == null)
                return xOptions;
            else if (xOptions == null)
                return yOptions;
            else
                return ((int)xOptions + yOptions);
            
        }
        /// <summary>
        /// Converting to <see cref="SimpleDirection"/>.
        /// </summary>
        /// <exception cref="FormatException"/>
        /// <param name="minimalRound"></param>
        /// <returns></returns>
        public SimpleDirection ToSimpleDirection()
        {
            return TryToSimpleDirection() ?? throw new FormatException("Failed convert to SimpleDirection");
        }
      
        public Direction Rotate(float angle)
        {
            return ToVector2().Rotate(angle).ToDirection();
        }

        public void Deconstruct(out float x, out float y)
        {
            x = X;
            y = Y;
        }
        public override bool Equals(object obj)
        {
            if (obj is Direction dir)
                return this == dir;
            else
                return false;
        }
        private static Direction Operator(Direction a, Direction b, Func<Vector2, Vector2, Vector2> func)
            => func(a, b);

        public override int GetHashCode()
        {
            return (int)(X + Y);
        }
        public override string ToString()
            => $"X: {X},Y: {Y}";
        bool IEquatable<Direction>.Equals(Direction other)
        {
            return this == other;
        }
        public IEnumerator<float> GetEnumerator()
        {
            yield return X;
            yield return Y;
        }
        public static Direction operator +(Direction a, Direction b)
            => Operator(a, b, (x, y) => x + y);
        public static Direction operator -(Direction a, Direction b)
            => Operator(a, b, (x, y) => x - y);
        public static Direction operator /(Direction a, Direction b)
            => Operator(a, b, (x, y) => x / y);
        public static Direction operator *(Direction a, Direction b)
           => Operator(a, b, (x, y) => x * y);
        public static Direction operator -(Direction a)
            => -a.ToVector2();
        public static Vector2 operator *(Direction a, float val)
            => a.ToVector2() * val;
        public static Vector2 operator *(float val, Direction a)
            => a * val;
        public static Vector2 operator /(Direction a, float val)
            => a.ToVector2() / val;
        public static Vector2 operator *(Direction a, Vector2 b)
            => a.ToVector2() * b;
        public static bool operator ==(Direction a, Direction b)
            => a.X == b.X && a.Y == b.Y;
        public static bool operator !=(Direction a, Direction b)
            => !(a == b);
        public static implicit operator Vector2(Direction dir)
            => dir.ToVector2();
        public static implicit operator Direction(Vector2 vect)
            => vect.ToDirection();
        public static implicit operator Direction(SimpleDirection dir)
            => dir.ToDirection();
        public static explicit operator SimpleDirection(Direction dic)
            => dic.ToSimpleDirection();


    }
}
