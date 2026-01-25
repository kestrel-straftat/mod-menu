using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BepInEx;
using BepInEx.Bootstrap;
using HarmonyLib;

namespace ModMenu.Utils
{
    // more or less entirely ripped off from the ever delightful JotunnLib of valheim modding fame
    internal static class BepInExUtils
    {
        internal static Dictionary<Assembly, BepInEx.PluginInfo> PluginInfoCache { get; } = new();
        internal static Dictionary<BepInEx.PluginInfo, string> PluginTypeNameCache { get; } = new();

        internal static string GetPluginInfoTypeName(BepInEx.PluginInfo pluginInfo) {
            if (PluginTypeNameCache.TryGetValue(pluginInfo, out var typeName)) {
                return typeName;
            }
            
            typeName = AccessTools.Property(typeof(BepInEx.PluginInfo), "TypeName").GetValue(pluginInfo) as string;
            PluginTypeNameCache.Add(pluginInfo, typeName);
            return typeName;
        }
        
        internal static BepInEx.PluginInfo GetPluginInfoFromAssembly(Assembly assembly) {
            if (PluginInfoCache.TryGetValue(assembly, out var pluginInfo)) {
                return pluginInfo;
            }

            foreach (var info in Chainloader.PluginInfos.Values.Where(info => assembly.GetType(GetPluginInfoTypeName(info)) != null)) {
                PluginInfoCache.Add(assembly, info);
                return info;
            }
            
            PluginInfoCache.Add(assembly, null);
            return null;
        }
    }
}