using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Cyberevolver.Unity
{
    public static class DictionaryExtension
    {
        public static void AddOrSet<TKey,TValue>(
            this Dictionary<TKey,TValue> dictioniary,
            TKey key,
            TValue value)
        {
            if (dictioniary.ContainsKey(key))
                dictioniary[key] = value;
            else
                dictioniary.Add(key, value);

        }
        public static TValue GetOrSetDefualt<TKey,TValue>(
             this Dictionary<TKey, TValue> dictioniary,
            TKey key,
            TValue defal=default)
        {
            if(dictioniary.TryGetValue(key,out TValue getted))
            {
                return getted;
            }
            else
            {
                dictioniary.Add(key, defal);
                return defal;
            }
        }
        public static Dictionary<TKey, IGrouping<TKey,TValue>> ToDictionary<TKey,TValue>(this IEnumerable<IGrouping<TKey,TValue>> groups)
        {
            return groups.ToDictionary(item => item.Key, item => item);
        }
        public static ReadOnlyDictionary<TKey,TValue> AsReadOnly<TKey,TValue>(this Dictionary<TKey,TValue> dictioniary)
        {
            return new ReadOnlyDictionary<TKey, TValue>(dictioniary);
        }
    }
}
