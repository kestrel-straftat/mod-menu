using System.Collections.Generic;
using System.Linq;
using BepInEx.Bootstrap;
using ModMenu.Mods;
using ModMenu.Utils;

namespace ModMenu
{
    public static class ModMenuManager
    {
        public static Mod[] mods;

        public static void Init() {
            var pluginInfos = Chainloader.PluginInfos.Values;
            
            List<Mod> modsToAdd = new();
            
            foreach (var pi in pluginInfos) {
                var info = new ModInfo {
                    guid = pi.Metadata.GUID,
                    name = pi.Metadata.Name,
                    version = pi.Metadata.Version.ToString(),
                    description = "No description found.",
                    icon = Assets.DefaultModIcon
                };
                modsToAdd.Add(new Mod(info));
            }
            
            mods = modsToAdd.ToArray();
        }
    }
}