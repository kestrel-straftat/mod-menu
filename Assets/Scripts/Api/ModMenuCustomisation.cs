using System;
using System.Collections.Generic;
using System.Reflection;
using BepInEx.Configuration;
using ModMenu.Options;
using ModMenu.Utils;
using UnityEngine;

namespace ModMenu.Api
{
    public static class ModMenuCustomisation
    {
        // keyed by plugin guid
        internal static Dictionary<string, Action<OptionListContext>> Builders { get; private set; } = new();
        internal static Dictionary<string, Sprite> Icons { get; private set; } = new();
        internal static Dictionary<string, string> Descriptions { get; private set; } = new();
        
        internal static HashSet<ConfigEntryBase> HiddenConfigEntries { get; private set; } = new();

        /// <summary>
        /// Registers a content builder that will be run while ModMenu is generating the option list
        /// for the current plugin's config file
        /// </summary>
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

        /// <summary>Prevents ModMenu from generating a config item for the specified <see cref="ConfigEntryBase"/></summary>
        /// <param name="entry">The entry to hide</param>
        public static void HideEntry(ConfigEntryBase entry) => HiddenConfigEntries.Add(entry);
        
        /// <summary>Prevents ModMenu from generating a config item for the specified <see cref="ConfigEntryBase"/>s</summary>
        /// <param name="entries">An enumerable of entries to hide</param>
        public static void HideEntries(IEnumerable<ConfigEntryBase> entries) {
            foreach (var entry in entries) {
                HiddenConfigEntries.Add(entry);
            }
        }

        /// <summary>Sets the icon of the current plugin, overriding any detected thunderstore metadata</summary>
        /// <param name="icon">The sprite to use for the plugin icon</param>
        public static void SetPluginIcon(Sprite icon) {
            try {
                var info = BepInExUtils.GetPluginInfoFromAssembly(Assembly.GetCallingAssembly());
                if (info != null) {
                    Icons.Add(info.Metadata.GUID, icon);
                }
                else {
                    throw new InvalidOperationException("SetPluginIcon must be called from an assembly containing a BepInEx plugin.");
                }
            }
            catch (ArgumentException) {
                throw new InvalidOperationException("SetPluginIcon can only be called once per assembly.");
            }
        }
        
        /// <summary>Sets the description of the current plugin, overriding any detected thunderstore metadata</summary>
        /// <param name="description">The description to use</param>
        public static void SetPluginDescription(string description) {
            try {
                var info = BepInExUtils.GetPluginInfoFromAssembly(Assembly.GetCallingAssembly());
                if (info != null) {
                    Descriptions.Add(info.Metadata.GUID, description);
                }
                else {
                    throw new InvalidOperationException("SetPluginDescription must be called from an assembly containing a BepInEx plugin.");
                }
            }
            catch (ArgumentException) {
                throw new InvalidOperationException("SetPluginDescription can only be called once per assembly.");
            }
        }
    }
}