using System;

namespace ThemeImagesSample;

public sealed class Theme
{
    public Theme(string key, ResourceDictionary resources)
    {
        Key = key;
        Resources = resources;
    }

    public string Key { get; internal set; }
    public ResourceDictionary Resources { get; internal set; }
}