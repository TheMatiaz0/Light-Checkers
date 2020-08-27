using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Cyberevolver
{
    public static class LinqExtension
    {
        /// <summary>
        /// Finding index of element, which fit to predicate.
        /// </summary>
     
        public static int? GetIndex<T>(this IEnumerable<T> collection, Func<T, bool> func)
        {
            int i = 0;
            foreach (T item in collection)
            {
                if (func(item))
                    return i;
                i++;
            }
            return null;

        }
        /// <summary>
        /// Finding index of element.
        /// </summary>

        public static int? GetIndex<T>(this IEnumerable<T> collection,T element)
        {
            return GetIndex(collection, element, EqualityComparer<T>.Default);   
        }
        /// <summary>
        /// Finding index of element using concrent comparer.
        /// </summary>
        public static int? GetIndex<T>(this IEnumerable<T> collection, T element, IEqualityComparer<T> comparer)
        {
            return GetIndex(collection, item =>comparer.Equals(item, element));
        }
        public static IEnumerable<T> Crosses<T>(this IEnumerable<T> a, IEnumerable<T> b)
        {
            T[] aR = a.ToArray();
            T[] bR = b.ToArray();
            int x;
            int min = aR.Length;
            T[] upper = new T[0];
            if (aR.Length != bR.Length)
            {
                min = Math.Min(aR.Length, bR.Length);
                upper = (min == aR.Length) ? bR : aR;
            }

            for (x = 0; x < min * 2; x++)
            {
                yield return (x % 2 == 0) ? aR[x / 2] : bR[x / 2];

            }
            for (x /= 2; x < upper.Length; x++)
            {
                yield return upper[x];
            }
        }
    }


   
}
