using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cyberevolver.Unity
{
    [Serializable]
    public class BasSerializableHashSet { }
    [Serializable]
    public class SerializableHashSet<T> : BasSerializableHashSet, ISerializationCallbackReceiver
    {
        [SerializeField]
        private List<T> elements = new List<T>();
        public HashSet<T> HashSet { get; } = new HashSet<T>();
        public void OnAfterDeserialize()
        {
            HashSet.Clear();
            foreach(T element in elements)
            {
                if (HashSet.Contains(element) == false)
                    HashSet.Add(element);
            }     
        }
        public void OnBeforeSerialize()
        {
            foreach (T item in HashSet)
            {
                if(elements.Contains(item)==false)
                {
                    elements.Add(item);
                }
            }
        }
    }
}
