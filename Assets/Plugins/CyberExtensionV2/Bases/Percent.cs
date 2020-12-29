#if UNITY_EDITOR || UNITY_STANDALONE ||UNITY_WII|| UNITY_IOS || UNITY_IOS || UNITY_IPHONE || UNITY_ANDROID || UNITY_PS4 || UNITY_XBOXONE || UNITY_LUMIN || UNITY_TIZEN ||UNITY_TVOS || UNITY_WSA || UNITY_WSA_10_0 || UNITY_WINRT || UNITY_WINRT_10_0 ||UNITY_WEBGL ||UNITY_FACEBOOK||UNITY_FACEBOOK||UNITY_ADS || UNITY_ANALYTICS ||UNITY_ASSERTIONS ||UNITY_64
#define ANY_UNITY
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyberevolver
{
  
   
    /// <summary>
    /// Represent percent. It is better than <see cref="float"/> and <see cref="double"/>, because
    /// it cannot be different than 0.0-1.0.
    /// </summary>
    [Serializable]
    public struct Percent
    {
  

        public const double MaxValue = 1.0;
        /// <summary>
        /// Default value (0), which you can use as default parametr.
        /// </summary>
        public static readonly Percent Zero = new Percent();
        /// <summary>
        /// 50%
        /// </summary>
        public static readonly Percent Half = new Percent(0.5f);
        /// <summary>
        /// 100%
        /// </summary>
        public static readonly Percent Full = new Percent(1);
#if ANY_UNITY
        [UnityEngine.SerializeField]
        [UnityEngine.Range(0.0f,1.0f)]
#endif
        private double _Value;
        /// <summary>
        /// Get double value (native). It is always in range 0.0-1.0
        /// </summary>
        public double AsDoubleValue
        {
            get => _Value;
            set
            {

                if (value > 1 || value < 0)
                    if(value!=_Value)
                    {
                        _Value = value;
                       
                    }
                   
                else
                    throw new ArgumentException("Argument can not be different that 0.0-1.0", nameof(value));
            }
        }
        /// <summary>
        /// Get float value. It is always in range 0.0-1.0. Value will be converted from float.
        /// </summary>
        public float AsFloatValue => (float)AsDoubleValue;
        /// <summary>
        /// Convert to classic percent values. That means in range 0-100.
        /// Example, 0,5 will be convert to 50.
        /// </summary>
        public byte AsProcentValue => (byte)(_Value * 100);
      
        /// <summary>
        /// Given value must be in range 0.0-1.0.
        /// </summary>
        /// <param name="decimal"></param>
        public Percent(double @decimal)
        {
            _Value = @decimal;
        }
        /// <summary>
        /// Creating percent equal value/full
        /// </summary>
        /// <param name="value"></param>
        /// <param name="full"></param>
        public Percent(double value, double full)
        {
            if (value > full)
                throw new ArgumentException("value cannot be greater that full", nameof(value));
            _Value = value / full;
        }
        /// <summary>
        /// Parsing from string.
        /// </summary>
        /// <exception cref="FormatException"></exception>
        /// <param name="text"></param>
        /// <returns></returns>
        public static Percent Parse(string text)
        {
            byte value = byte.Parse(text.Replace("%", ""));
            return Percent.FromPercent(value);
        }
        /// <summary>
        /// Try parsing from string. If it failed, you will not get <see cref="FormatException"/>.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static bool TryParse(string text, out Percent result)
        {
            bool isGood;
            if (isGood = byte.TryParse(text.Replace("%", ""),out byte r))
            {
                result = r;
            }
            else
                result= Percent.Zero;
            return isGood;
        }
        /// <summary>
        /// Identical to <see cref="TryParse(string, out Percent)"/> but ignoring result.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool TryParse(string text)
            => TryParse(text, out _);
        /// <summary>
        /// Returned value can be converted to <see cref="Percent"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool CanConvert(double value)
            => value >= 0 && value <= 1.0;
        /// <summary>
        /// Value must be in range 0-100.
        /// </summary>
        /// <param name="val"></param>
        public static Percent FromPercent(byte val)
        {
            return new Percent(val / 100.0);
        }
        /// <summary>
        /// Value must be in range 0.0-1.0.
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static Percent FromDecimal(double val)
        {
            return new Percent(val);
        }
        /// <summary>
        /// Identical to delete one from other
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public double Different(Percent other)
           => this.AsDoubleValue - other.AsDoubleValue;
        public static bool operator ==(Percent a, Percent b)
            => a.AsDoubleValue == b.AsDoubleValue;
        public static bool operator !=(Percent a, Percent b)
            => !(a == b);
        public static bool operator >(Percent a, Percent b)
            => a.AsDoubleValue > b.AsDoubleValue;
        public static bool operator <(Percent a, Percent b)
            => a.AsDoubleValue < b.AsDoubleValue;
        public static bool operator <=(Percent a, Percent b)
            => a == b || a < b;
        public static bool operator >=(Percent a, Percent b)
          => a == b || a > b;
        public static implicit operator double(Percent procent)
            => procent.AsDoubleValue;
        public static explicit operator float(Percent procent)
            => procent.AsFloatValue;
        public static implicit operator Percent(float value)
            => new Percent(value);
        public static implicit operator Percent(double value)
            => new Percent(value);
        public static Percent operator +(Percent a, double b)
         => new Percent(Math.Min(1, a.AsDoubleValue + b));
        public static Percent operator -(Percent a, double b)
        => new Percent(Math.Max(0, a.AsDoubleValue - b));
        public static Percent operator *(Percent a, double b)
            => new Percent(Math.Min(1, a.AsDoubleValue * b));
        public static Percent operator /(Percent a, double b)
            => new Percent(Math.Min(1, a.AsDoubleValue / b));
        public static Percent operator +(Percent a, Percent b)
            => a + b.AsDoubleValue;
        public static Percent operator -(Percent a, Percent b)
            => a - b.AsDoubleValue;
        public static Percent operator *(Percent a, Percent b)
            => a * b.AsDoubleValue;
        public static Percent operator /(Percent a, Percent b)
            => a / b.AsDoubleValue;
        public override bool Equals(object obj)
        {
            if (obj is Percent p)
                return this == p;
            else return false;
        }
        public override string ToString()
        {
            return $"{AsProcentValue}%";
        }
        public override int GetHashCode()
        {
            return AsDoubleValue.GetHashCode();
        }
    }
}
