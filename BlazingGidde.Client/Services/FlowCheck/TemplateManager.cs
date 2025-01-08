using BlazingGidde.Shared.Models.FlowCheck;
using DevExpress.Blazor;

namespace BlazingGidde.Client.Services.FlowCheck
{
    public class TemplateManager : APIRepository<Template, int>
    {
        public TemplateManager(HttpClient _http, IToastNotificationService toastNotificationService)
            : base(_http, "Template", toastNotificationService)
        { }
    }
}
