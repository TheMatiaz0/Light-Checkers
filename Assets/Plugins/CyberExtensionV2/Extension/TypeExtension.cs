using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Cyberevolver
{
    public static class TypeExtension
    {
        /// <summary>
        /// Returns true if type is just number.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsNumber(this Type type)
        {
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.SByte:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Byte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:          
                case TypeCode.Single:
                case TypeCode.Double:
                case TypeCode.Decimal:
                case TypeCode.Char:
                    return true;
               
                default:return false;
               
            }

        }
    }

}
