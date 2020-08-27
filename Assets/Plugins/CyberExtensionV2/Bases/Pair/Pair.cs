
#if UNITY_EDITOR || UNITY_STANDALONE ||UNITY_WII|| UNITY_IOS || UNITY_IOS || UNITY_IPHONE || UNITY_ANDROID || UNITY_PS4 || UNITY_XBOXONE || UNITY_LUMIN || UNITY_TIZEN ||UNITY_TVOS || UNITY_WSA || UNITY_WSA_10_0 || UNITY_WINRT || UNITY_WINRT_10_0 ||UNITY_WEBGL ||UNITY_FACEBOOK||UNITY_FACEBOOK||UNITY_ADS || UNITY_ANALYTICS ||UNITY_ASSERTIONS ||UNITY_64
#define ANY_UNITY
#endif
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#pragma warning disable IDE0066

namespace Cyberevolver
{
    public enum PairElement
    {
        First = 0,
        Second = 1
    }
    public static class PairElementExtension
    {
        public static PairElement Reverse(this PairElement element)
        {
            return (element == PairElement.First) ? PairElement.Second : PairElement.First;
        }
    }

    /// <summary>
    /// Generic collection for 2 value .It implement IEnumerator and classic value getting ("first","second") 
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    [Serializable]
    public class Pair<T1, T2> : IEnumerable<object>,IPair<T1,T2>      
    {
        protected const string OutOfArgumentException = "Value must be in range of 0-1";
        public T1 First
        {
            get => _First;
            set
            {
                _First = value;
                OnFirstValueChanged(this, value);
            }

        }
#if ANY_UNITY
        [UnityEngine.SerializeField]
#endif
        private T1 _First;
#if ANY_UNITY
        [UnityEngine.SerializeField]
#endif
        private T2 _Second;

        public T2 Second
        {
            get => _Second;
            set
            {
                _Second = value;
                OnSecondValueChanged(this, value);
            }
        }
       
        public event EventHandler<SimpleArgs<T1>> OnFirstValueChanged = delegate { };
        public event EventHandler<SimpleArgs<T2>> OnSecondValueChanged = delegate { };
        public Pair(T1 first = default, T2 second = default)
        {
            First = first;
            Second = second;
        }

        public Pair(T2 b, T1 a = default) : this(a, b) { }
        public object this[int index]
        {
            get
            {
                switch(index)
                {
                    case 0: return First;
                    case 1: return Second;
                    default: throw new ArgumentOutOfRangeException(OutOfArgumentException);
                }
            }
            set
            {
                switch (index)
                {
                    case 0: First = (T1)value; break;
                    case 1: Second = (T2)value; break;
                }
            }
        }

        public IEnumerator<object> GetEnumerator()
        {
            yield return First;
            yield return Second;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public static implicit operator Pair<T1>(Pair<T1,T2> pair)
        {
            if (typeof(T1) != typeof(T2))
                return null;
            return new Pair<T1>((T1)pair.First, (T1)(object)pair.Second);
        }
        public class Comparer : IComparer<Pair<T1, T2>>
        {
            public PairElement Element { get; }
            public Comparer(PairElement byElement)
            {
                Element = byElement;
            }
            public int Compare(Pair<T1, T2> x, Pair<T1, T2> y)
            {

                return ((x[(int)Element])as IComparable).CompareTo(x[(int)Element]);
            }
        }
    }   
    public class Pair<T> : Pair<T, T>, IEnumerable<T>    
    {
        public Pair(T a = default, T b = default) : base(first: a, second: b) { }
        public new T this[int index]
        {
            get => (T)base[index];
            set => base[index] = value;
        }

        public new IEnumerator<T> GetEnumerator()
        {
            yield return First;
            yield return Second;
        }
    }
}
