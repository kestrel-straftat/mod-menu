using System;
using System.Collections;
using System.Linq;
using BepInEx.Configuration;

namespace ModMenu.Extensions
{
    public static class AcceptableValueBaseExtensions
    {
        public static string HumanizedString(this AcceptableValueBase avb) {
            var type = avb.GetType();
            if (avb.IsAcceptableValueRange()) {
                var minValue = type.GetProperty("MinValue")!.GetValue(avb);
                var maxValue = type.GetProperty("MaxValue")!.GetValue(avb);
                
                return "Accepts " + minValue + " to " + maxValue;
            }
            if (avb.IsAcceptableValueList()) {
                var acceptableValues = ((IEnumerable)type.GetProperty("AcceptableValues")!.GetValue(avb)).Cast<object>().ToArray();
                
                return "Accepts " + string.Join(", ", acceptableValues.SkipLast(1).Select(v => v.ToString())) + " and " + acceptableValues[^1];
            }

            throw new NotSupportedException($"Type \"{type}\" is not supported.");
        }

        public static bool IsAcceptableValueList(this AcceptableValueBase avb) {
            return avb.GetType().GetGenericTypeDefinition() == typeof(AcceptableValueList<>);
        }

        public static bool IsAcceptableValueRange(this AcceptableValueBase avb) {
            return avb.GetType().GetGenericTypeDefinition() == typeof(AcceptableValueRange<>);
        }
    }
}