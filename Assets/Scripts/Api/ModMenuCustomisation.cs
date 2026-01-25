using System;
using System.Collections.Generic;
using System.Reflection;
using ModMenu.Utils;

namespace ModMenu.Api
{
    public static class ModMenuCustomisation
    {
        internal static Dictionary<string, Action<OptionListContext>> Builders { get; private set; } = new();

        public static void RegisterContentBuilder(Action<OptionListContext> builder) {
            try {
                var info = BepInExUtils.GetPluginInfoFromAssembly(Assembly.GetCallingAssembly());
                if (info != null) {
                    Builders.Add(info.Metadata.GUID, builder);
                }
                else {
                    throw new InvalidOperationException("RegisterContentBuilder must be called from an assembly containing a BepInEx plugin.");
                }
            }
            catch (ArgumentException) {
                throw new InvalidOperationException("RegisterContentBuilder can only be called once per assembly.");
            }
        }
    }
}