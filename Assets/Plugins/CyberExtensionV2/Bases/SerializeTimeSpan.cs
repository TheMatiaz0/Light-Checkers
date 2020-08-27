using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cyberevolver.Unity
{
	/// <summary>
	/// Serializing <see cref="System.TimeSpan"/> version.
	/// You can get original <see cref="System.TimeSpan" /> value from this.
	/// </summary>
	[Serializable]
	public struct SerializeTimeSpan : ISerializationCallbackReceiver
	{
		[SerializeField]
		[Range(0, 999)]
		private int miliseconds;
		[SerializeField]
		[Range(0, 59)]
		private int seconds, minutes;
		[SerializeField]
		[Range(0, 23)]
		private int hour;
		[SerializeField]
		[Range(0, 999)]
		private int day;
		[SerializeField]

		private int precision;

		private TimeSpan _TimeSpan;
		/// <summary>
		/// Original <see cref="TimeSpan"/>.
		/// </summary>
		public TimeSpan TimeSpan
		{
			get => _TimeSpan;
			set
			{
				_TimeSpan = value;
			}
		}
		public SerializeTimeSpan(TimeSpan span)
		{
			_TimeSpan = span;
			miliseconds = span.Milliseconds;
			seconds = span.Seconds;
			minutes = span.Minutes;
			hour = span.Hours;
			day = span.Days;
			precision = 2;
		}
		public static implicit operator SerializeTimeSpan(TimeSpan span)
		{
			return new SerializeTimeSpan(span);
		}
		void ISerializationCallbackReceiver.OnAfterDeserialize()
		{
			_TimeSpan = new TimeSpan((precision >= 5) ? day : 0, (precision >= 4) ? hour : 0, (precision >= 3) ? minutes : 0, (precision >= 2) ? seconds : 0, miliseconds);
		}
		void ISerializationCallbackReceiver.OnBeforeSerialize()
		{
			miliseconds = _TimeSpan.Milliseconds;
			seconds = _TimeSpan.Seconds;
			minutes = _TimeSpan.Minutes;
			hour = _TimeSpan.Hours;
			day = _TimeSpan.Days;
		}
	}
}
