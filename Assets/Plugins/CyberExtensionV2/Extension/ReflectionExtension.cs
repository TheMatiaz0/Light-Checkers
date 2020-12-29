using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
namespace Cyberevolver
{
    public static class ReflectionExtension
    {

        /// <summary>
        ///  Geting all <see cref="Type"/>, which is subclass of T, and is not abstract,
        ///  and creating non paremetr instance. If you want only get type array, you should use
        ///  <see cref="GetAllNonAbstractInheritesType"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T[] GetAllNonAbstractInheritesInstance<T>()
            where T : class
        {
            List<T> all = new List<T>();
            foreach (var item in Assembly.GetAssembly(typeof(T)).GetTypes().Where(type => type.IsAbstract == false && type.IsSubclassOf(typeof(T))))
            {
                all.Add(Activator.CreateInstance(item) as T);
            }
            return all.ToArray();
        }
        /// <summary>
        /// Geting all <see cref="Type"/>, which is subclass of T, and is not abstract.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<Type> GetAllNonAbstractInheritesType<T>()
            where T : class
        {
            return AppDomain.CurrentDomain.GetAssemblies().Select(item => item.GetExportedTypes()).SelectMany(item => item).Where(item => item.IsSubclassOf(typeof(T)) && item.IsAbstract == false);
        }
        /// <summary>
        /// Geting all <see cref="Type"/>, which implement T interface, and is not abstract.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<Type> GetAllTypesOfInterface<T>()
            where T : class
        {
            return AppDomain.CurrentDomain.GetAssemblies().Select(item => item.GetExportedTypes()).SelectMany(item => item).Where(item => item.GetInterfaces().Any(type => type == typeof(T)));
        }



    }
}
