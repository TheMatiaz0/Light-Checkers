using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cyberevolver.Unity
{
    /// Serializing <see cref="System.DateTime"/> version.
    /// You can get orignal <see cref="System.DateTime" /> value from this.
    /// </summary>
    [Serializable]
    public class SerializeDataTime:ISerializationCallbackReceiver
    {
        [SerializeField] private uint milisecond;
        [SerializeField] private uint second;
        [SerializeField] private uint minute;
        [SerializeField] private uint hour;
        [SerializeField] private uint day;
        [SerializeField] private Month month;
        [SerializeField] private uint year;
        private DateTime _Date;
        /// <summary>
        /// Orginal <see cref="DateTime"/>.
        /// </summary>
        public DateTime Date
        {
            get => _Date;
            set
            {
                _Date = value;
            }
        }
        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            try
            {
                _Date = new DateTime((int)year, (int)month, (int)day, (int)hour, (int)minute, (int)second, (int)milisecond);
            }
            catch
            {
                ((ISerializationCallbackReceiver)this).OnBeforeSerialize();
            }         
        }
        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            milisecond =(uint) _Date.Millisecond;
            second = (uint)_Date.Second;
            minute = (uint)_Date.Minute;
            hour = (uint)_Date.Hour;
            day = (uint)_Date.Day;
            month = (Month)_Date.Month;
            year = (uint)_Date.Year;          
        }
    }
}
