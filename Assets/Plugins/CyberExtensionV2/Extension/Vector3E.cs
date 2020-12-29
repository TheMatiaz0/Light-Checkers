using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace Cyberevolver.Unity
{
    public static class Vector3E
    {
        public static Vector3 Make(float value)
            => new Vector3(value, value, value);
    }
}
