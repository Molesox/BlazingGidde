using BlazingGidde.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Logging.Abstractions;

namespace BlazingGidde.Server.Services;

public class TemplateRenderer
{
    private readonly IServiceProvider _serviceProvider;

    public TemplateRenderer(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<string> RenderTemplateAsync<TComponent>(EmailViewModel model)

    {
        var htmlRenderer = new HtmlRenderer(_serviceProvider, NullLoggerFactory.Instance);

        using var stringWriter = new StringWriter();

        var parameters = new Dictionary<string, object>
        {
            { "Model", model }
        };

        var component = typeof(TComponent);

        var html = await htmlRenderer.Dispatcher.InvokeAsync(async () =>
        {
            var renderTask = htmlRenderer.RenderComponentAsync(component, ParameterView.FromDictionary(parameters));
            var result = await renderTask;
            return result.ToString() ?? string.Empty;
        });

        stringWriter.Write(html);
        return stringWriter.ToString();
    }
}