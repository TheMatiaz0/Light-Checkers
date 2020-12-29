using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class DictionaryExtension
{
	public static void RemoveAll<K, V> (this IDictionary<K, V> dictionary, Func<K, V, bool> match)
	{
		foreach (K key in dictionary.Keys.ToArray().Where(key => match(key, dictionary[key])))
		{
			dictionary.Remove(key);
		}
	}
}
