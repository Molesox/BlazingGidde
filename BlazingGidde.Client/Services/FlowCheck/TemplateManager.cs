using BlazingGidde.Shared.DTOs.FlowCheck;
using BlazingGidde.Shared.Models.FlowCheck;
using DevExpress.Blazor;

namespace BlazingGidde.Client.Services.FlowCheck
{
    public class TemplateManager : APIRepository<TemplateDto, int>
    {
        public TemplateManager(HttpClient _http, string _controllerName, IToastNotificationService toastNotificationService) 
            : base(_http, _controllerName, toastNotificationService)
        {
        }
    }
}
