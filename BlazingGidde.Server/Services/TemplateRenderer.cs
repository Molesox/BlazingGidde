using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.Extensions.Logging.Abstractions;
using System.Text.Encodings.Web;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components.Web;


namespace BlazingGidde.Server.Services
{
    public class TemplateRenderer
    {
       private readonly IServiceProvider _serviceProvider;

        public TemplateRenderer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task <string> RenderTemplateAsync<TComponent,TModel>(TModel model)
            where TComponent : IComponent
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
}
