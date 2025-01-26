namespace BlazingGidde.Client.Services;

public static class ThemeDictionary
{
    public static readonly Dictionary<string, string> Themes = new()
    {
        { "Dark", "blazing-dark.bs5.min.css" },
        { "Light", "office-white.bs5.min.css" },
        { "Purple", "purple.bs5.min.css" },
        { "Berry", "blazing-berry.bs5.min.css" },
        { "Index", "_content/DevExpress.Blazor.Themes/" }
    };
}