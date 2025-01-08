using BlazingGidde.Shared.Models.FlowCheck;
using DevExpress.Blazor;

namespace BlazingGidde.Client.Services.FlowCheck
{
    public class TemplateTypeManager : APIRepository<TemplateType, int>
    {
        public TemplateTypeManager(HttpClient _http, IToastNotificationService toastNotificationService)
            : base(_http, "TemplateType", toastNotificationService)
        { }
    }
}
