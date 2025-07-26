using Newtonsoft.Json;

namespace ModMenu.Mods
{
    public class ThunderstoreManifest
    {
        [JsonProperty("name")] public string Name { get; set; }
        [JsonProperty("version_number")] public string Version { get; set; }
        [JsonProperty("website_url")] public string WebsiteUrl { get; set; }
        [JsonProperty("description")] public string Description { get; set; }
        [JsonProperty("dependencies")] public string[] Dependencies { get; set; }
    }
}