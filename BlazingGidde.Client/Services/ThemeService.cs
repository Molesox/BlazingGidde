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
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "selectedTheme", themeUrl);
            await _jsRuntime.InvokeVoidAsync("setTheme", themeUrl);
        }

        public async Task<string> GetSavedThemeAsync()
        {
            var theme = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "selectedTheme");
            return theme ?? "blazing-dark.bs5.min.css";
        }
    }
}
