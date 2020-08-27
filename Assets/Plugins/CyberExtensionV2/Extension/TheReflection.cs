using Cyberevolver;
using Cyberevolver.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cyberevolver
{
	public static class TheReflection
	{
		public static IEnumerable<Type> GetAllNonAbstractSubclassOf(Type mainClass, params Type[] reqAttributes)
		{
			return GetAllType(item => item.IsSubclassOf(mainClass) && item.IsAbstract == false, reqAttributes);
		}
		public static bool Is(this Type a, Type b)
			=> (!(a == null ^ b == null))
			&& (a == b
			|| a.IsSubclassOf(b)
			|| a.GetInterfaces().Contains(b));
		public static bool Is<T1, T2>()
			=> Is(typeof(T1), typeof(T2));
		public static IEnumerable<MethodInfo> GetAllMethod(Predicate<MethodInfo> predicate = null, params Type[] reqAttributes)
		{
			if (reqAttributes == null)
				reqAttributes = new Type[0];
			if (predicate == null)
				predicate = m => true;
			return (
				from type in GetAllType()
				from method in type.GetMethods()
				where predicate(method)
				&& reqAttributes.Any(atr => method.GetCustomAttribute(atr) == null) == false
				select method);
		}
		public static IEnumerable<MethodInfo> GetAllStaticMethod(Predicate<MethodInfo> predicate = null, params Type[] reqAttributes)
		{
			Predicate<MethodInfo> newPredicate;
			if (predicate == null)
				newPredicate = m => m.IsStatic;
			else
				newPredicate = m => m.IsStatic && predicate(m);
			return GetAllMethod(newPredicate, reqAttributes);
		}

		public static IEnumerable<Type> GetAllType(Predicate<Type> predicate = null, params Type[] reqAttributes)
		{
			if (reqAttributes == null)
				reqAttributes = new Type[0];
			var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(item =>
			{
				if (item.FullName != "SharpDX.MediaFoundation, Version=4.0.1.0, Culture=neutral, PublicKeyToken=b4dcf0f35e5521f1")//this type is curse
					return item.GetTypes();
				else
					return new Type[0];
			}
			)
				.Where(type => reqAttributes.Any(atr => type.GetCustomAttribute(atr) == null) == false);
			if (predicate != null)
				types = types.Where(item => predicate(item));
			return types;
		}
	}
}
