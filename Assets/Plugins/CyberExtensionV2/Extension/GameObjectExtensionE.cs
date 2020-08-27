using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace Cyberevolver.Unity
{    public static class GameObjectExtensionE
    {

        /// <summary>
        /// If active is true, it will be false and conversely.
        /// </summary>
        /// <param name="gm"></param>
        public static void SwithActive(this GameObject gm)
        {
            gm.SetActive(!gm.activeSelf);
        }
        /// <summary>
        /// If geting failed, component will be added
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="gm"></param>
        /// <returns></returns>
        public static T TryGetElseAdd<T>(this GameObject gm)
            where T:MonoBehaviour
        {
            T result = gm.GetComponent<T>();
            if (result != null)
                return result;
            else
                return gm.AddComponent<T>();
        }
        /// <summary>
        /// If geting failed, component will be added
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="gm"></param>
        /// <returns></returns>
        public static Component TryGetElseAdd(this GameObject gm, Type type)
        {            
            Component result = gm.GetComponent(type);
            if (result != null)
                return result;
            else if (type.IsAbstract == false)
                return gm.AddComponent(type);
            else
                return null;
        }



    }


}

