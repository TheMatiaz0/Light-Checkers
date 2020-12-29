using System;
using System.Collections.Generic;
using UnityEngine;


namespace Cyberevolver.Unity
{


	public sealed class GlobalTime : MonoBehaviour
	{
		private bool isGoodInit;
		private float rememberedScale = 0;
		private HashSet<object> lockers = new HashSet<object>();
		public event EventHandler<SimpleArgs<float>> OnTimeScaleChanged = delegate { };
		public static GlobalTime Current { get; private set; }
		[RuntimeInitializeOnLoadMethod]
		public static void Init()
		{
			GameObject g = new GameObject();
			var global = g.gameObject.AddComponent<GlobalTime>();
			global.isGoodInit = true;
			global.rememberedScale = Time.timeScale;
			g.hideFlags = HideFlags.HideInHierarchy | HideFlags.HideInInspector;
			Current = global;
			UnityEngine.Object.DontDestroyOnLoad(g);

		}
		public void AddLockers(object first, params object[] other)
		{


			lockers.Add(first);
			if (other != null)
				foreach (var item in other)
					lockers.Add(item);
		}
		public bool RemoveLocker(object item)
		{

			bool res = lockers.Remove(item);

			if (lockers.Count == 0)
				Time.timeScale = 1;
			return res;
		}
		public bool HasLocker(object item)
		{
			return lockers.Contains(item);
		}
		private void Update()
		{
			if (lockers.Count > 0 && Time.timeScale != 0)
				Time.timeScale = 0;

			if (rememberedScale != Time.timeScale)
				OnTimeScaleChanged(this, Time.timeScale);
			rememberedScale = Time.timeScale;


		}
		private void Start()
		{
			if (isGoodInit == false)
				throw new Exception("Manual adding Global time is illegal");
		}
	}
}
