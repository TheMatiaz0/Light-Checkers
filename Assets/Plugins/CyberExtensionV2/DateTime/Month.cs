﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyberevolver
{
    /// <summary>
    /// Include all month in Gregorian.
    /// It has not None value.
    /// </summary>
    public enum Month
    {       
        January = 0,
        February = 1,
        March = 2,
        April = 3,
        May = 4,
        June = 5,
        July = 6,
        August = 7,
        September = 8,
        October = 9,
        November = 10,
        December = 11
    }
    public static class DateTimeExtension
    {
        public static Month GetMonth(this DateTime dateTime)
        {
           return (Month) dateTime.Month + 1;
        }
    }
}
