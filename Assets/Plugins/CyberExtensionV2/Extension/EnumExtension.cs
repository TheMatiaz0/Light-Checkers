using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cyberevolver;
using Cyberevolver.Unity;
using UnityEngine;
using System.Collections;
namespace Cyberevolver
{
    public static class EnumExtension
    {
        public static IEnumerable<Enum> GetAllFlags(this Enum @enum)
        {
            if (@enum is null)
            {
                throw new ArgumentNullException(nameof(@enum));
            }

            return Enum.GetValues(@enum.GetType()).OfType<Enum>()
                .Where(item => @enum.HasFlag(item));
        }
    }
}

