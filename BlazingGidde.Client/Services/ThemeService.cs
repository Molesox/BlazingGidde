using Microsoft.JSInterop;

namespace BlazingGidde.Client.Services
{
    public class ThemeService
    {
        private readonly IJSRuntime _jsRuntime;

        public ThemeService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task SetThemeAsync(string themeUrl)
        {
            if (themeUrl.StartsWith("_content/DevExpress.Blazor.Themes/_content/DevExpress.Blazor.Themes/"))
            {
                themeUrl = themeUrl.Replace("_content/DevExpress.Blazor.Themes/_content/DevExpress.Blazor.Themes/", "_content/DevExpress.Blazor.Themes/");
            }

            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "selectedTheme", themeUrl);
            await _jsRuntime.InvokeVoidAsync("setTheme", themeUrl);
        }

        public async Task<string> GetSavedThemeAsync()
        {
            var theme = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "selectedTheme");

            if (theme?.StartsWith("_content/DevExpress.Blazor.Themes/_content/DevExpress.Blazor.Themes/") == true)
            {
                theme = theme.Replace("_content/DevExpress.Blazor.Themes/_content/DevExpress.Blazor.Themes/", "_content/DevExpress.Blazor.Themes/");
                await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "selectedTheme", theme);
            }
            return theme ?? "_content/DevExpress.Blazor.Themes/blazing-dark.bs5.min.css";
        }
    }
}
