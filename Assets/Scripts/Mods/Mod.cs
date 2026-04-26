using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BepInEx.Bootstrap;
using BepInEx.Configuration;
using ModMenu.Options;
using ModMenu.Utils;
using Newtonsoft.Json;
using UnityEngine;

#nullable enable
namespace ModMenu.Mods
{
    internal class Mod
    {
        public readonly List<Option> config = new();
        public ModInfo info;
        public readonly BepInEx.PluginInfo pluginInfo;
        public bool HasAnyConfigs => config.Count > 0;
        public bool HasAdvancedMetadata { get; private set; } = false;
        public readonly Action<Api.OptionListContext>? customContentBuilder;

        public Mod(ModInfo info) {
            this.info = info;
            if (!Chainloader.PluginInfos.TryGetValue(info.guid, out pluginInfo))
                return;

            Api.ModMenuCustomisation.Builders.TryGetValue(info.guid, out customContentBuilder);
           
            LoadMetadata();

            LoadConfigEntries();   
        }
        
        private void LoadMetadata() {
            info.icon = Assets.DefaultModIcon;
            info.description = "No description found.";

            var searchDir = Path.GetFullPath(pluginInfo.Location);
            var parent = Directory.GetParent(searchDir);

            while (parent is not null && !string.Equals(parent.Name, "plugins", StringComparison.OrdinalIgnoreCase)) {
                searchDir = parent.FullName;
                parent = Directory.GetParent(searchDir);
            }

            if (searchDir.EndsWith(".dll")) {
                return;
            }

            if (Api.ModMenuCustomisation.Icons.TryGetValue(info.guid, out var icon)) {
                info.icon = icon;
                HasAdvancedMetadata = true;
            } 
            else {
                // try load from thunderstore metadata
                var iconPath = Directory.EnumerateFiles(searchDir, "icon.png", SearchOption.AllDirectories).FirstOrDefault();
                if (!string.IsNullOrEmpty(iconPath)) {
                    TryLoadIcon(iconPath);
                }
            }
            
            // we let TryLoadManifest run even if we find a description set through the API later
            // as it also gets the plugin name from the thunderstores manifest
            var manifestPath = Directory.EnumerateFiles(searchDir, "manifest.json", SearchOption.AllDirectories).FirstOrDefault();
            if (!string.IsNullOrEmpty(manifestPath)) {
                TryLoadManifest(manifestPath);
            }
            
            if (Api.ModMenuCustomisation.Descriptions.TryGetValue(info.guid, out var description)) {
                info.description = description;
                HasAdvancedMetadata = true;
            }
        }

        private void LoadConfigEntries() {
            // this can happen! if a plugin's static ctor throws, its BaseUnityPlugin() ctor will never
            // run, leaving a bunch of stuff in a half-initialised state. fun!
            if (pluginInfo.Instance.Config is not {} configFile) { 
                return;
            }
            
            foreach (var kv in configFile) {
                try {
                    if (Api.ModMenuCustomisation.HiddenConfigEntries.Contains(kv.Value)) {
                        continue;
                    }
                    
                    config.Add(Option.CreateForEntry(kv.Value));
                }
                catch (NotSupportedException e) {
                    Plugin.Logger.LogWarning($"Error generating config entry \"{kv.Key}\" for \"{info.name}\": {e.Message}");
                }
            }
        }

        private bool TryLoadIcon(string path) {
            var tex = new Texture2D(256, 256);
            if (!tex.LoadImage(File.ReadAllBytes(path))) return false;
            
            info.icon = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100);
            HasAdvancedMetadata = true;
            return true;
        }

        private bool TryLoadManifest(string path) {
            try {
                var manifest = JsonConvert.DeserializeObject<ThunderstoreManifest>(File.ReadAllText(path));
                info.description = manifest?.Description;
            
                // probably better than the name from the mod attribute
                // (will fall back to other name if manifest is not available)
                info.name = manifest?.Name.Replace('_', ' ');
                HasAdvancedMetadata = true;
            }
            catch (JsonException e) {
                Plugin.Logger.LogWarning($"Error trying to load manifest for \"{info.name}\": {e.Message}");
                return false;
                
            }

            return true;
        }
    }
}
#nullable restore