using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BepInEx.Bootstrap;
using ModMenu.Options;
using Newtonsoft.Json;
using UnityEngine;

namespace ModMenu.Mods
{
    public class Mod
    {
        public readonly List<Option> config = new();
        public ModInfo info;
        public readonly BepInEx.PluginInfo pluginInfo;
        public bool HasAnyConfigs => config.Count > 0;
        public bool HasAdvancedMetadata { get; private set; } = false;

        public Mod(ModInfo info) {
            this.info = info;
            if (!Chainloader.PluginInfos.TryGetValue(info.guid, out pluginInfo))
                return;
            
            LoadMetadata();

            LoadConfigEntries();   
        }

        private void LoadConfigEntries() {
            foreach (var entry in pluginInfo.Instance.Config) {
                try {
                    config.Add(Option.CreateForEntry(entry.Value));
                }
                catch (NotSupportedException e) {
                    Plugin.Logger.LogWarning($"Error generating config entry \"{entry.Key}\" for \"{info.name}\": {e.Message}");
                }
            }
        }

        private void LoadMetadata() {
            if (pluginInfo is null) {
                return;
            }

            var searchDir = Path.GetFullPath(pluginInfo.Location);
            var parent = Directory.GetParent(searchDir);

            while (parent is not null && !string.Equals(parent.Name, "plugins", StringComparison.OrdinalIgnoreCase)) {
                searchDir = parent.FullName;
                parent = Directory.GetParent(searchDir);
            }

            if (searchDir.EndsWith(".dll"))
                return;
            
            var iconPath = Directory.EnumerateFiles(searchDir, "icon.png", SearchOption.AllDirectories).FirstOrDefault();
            if (!string.IsNullOrEmpty(iconPath)) {
                LoadIcon(iconPath);
            }

            var manifestPath = Directory.EnumerateFiles(searchDir, "manifest.json", SearchOption.AllDirectories).FirstOrDefault();
            if (!string.IsNullOrEmpty(iconPath)) {
                LoadManifest(manifestPath);
            }
        }

        private void LoadIcon(string path) {
            var tex = new Texture2D(256, 256);
            if (!tex.LoadImage(File.ReadAllBytes(path))) return;
            
            info.icon = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100);
            HasAdvancedMetadata = true;
        }

        private void LoadManifest(string path) {
            var manifest = JsonConvert.DeserializeObject<ThunderstoreManifest>(File.ReadAllText(path));

            info.description = manifest.Description;
            
            // probably better than the name from the mod attribute
            // (will fall back to other name if manifest is not available)
            info.name = manifest.Name.Replace('_', ' ');
            HasAdvancedMetadata = true;
        }
    }
}