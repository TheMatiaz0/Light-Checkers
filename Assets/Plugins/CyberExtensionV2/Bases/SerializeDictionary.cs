using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cyberevolver;
using Cyberevolver.Unity;
using UnityEngine;
using System.Collections;
namespace Cyberevolver.Unity
{
    
    [Serializable]
    public abstract class BaseSerializeDictionary { }
    [Serializable]
    public class SerializableDictionary<TKey,TValue> : BaseSerializeDictionary, ISerializationCallbackReceiver
    {
        [SerializeField]
        private List<TKey> keys = new List<TKey>();
        [SerializeField]
        private List<TValue> values = new List<TValue>();
        public Dictionary<TKey, TValue> Dictionary { get; } = new Dictionary<TKey, TValue>();
     
        public void OnAfterDeserialize()
        {
            Dictionary.Clear();
           
            for(int x=0;x<keys.Count&&x<values.Count;x++)
            { 
                if (Dictionary.ContainsKey(keys[x]) == false)
                    Dictionary.Add(keys[x], values[x]);
            }
        }

        public void OnBeforeSerialize()
        {         
            foreach(var item in Dictionary)
            {
                if(keys.Contains(item.Key)==false)
                {
                    keys.Add(item.Key);
                    values.Add(item.Value);
                }
               
               
            }
        }
    }
}
