using System;

namespace ModMenu.Utils
{
    public static class TypeExtensions
    {
        public static bool IsIntegral(this Type type) {
            return type == typeof(sbyte) || type == typeof(byte) || type == typeof(short) || type == typeof(ushort)
                   || type == typeof(int) || type == typeof(uint) || type == typeof(long) || type == typeof(ulong);
                   // *technically* integral types but bepinex config doesn't support them
                   // || type == typeof(nint) || type == typeof(nuint);
        }

        public static bool IsFloating(this Type type) {
            return type == typeof(float) || type == typeof(double) || type == typeof(decimal);
        }
    }
}