using BlazingGidde.Shared.DTOs.FlowCheck;
using BlazingGidde.Shared.DTOs.FlowCheck.Request;
using BlazingGidde.Shared.Models.FlowCheck;
using DevExpress.Blazor;

namespace BlazingGidde.Client.Services.FlowCheck;

public class TemplateKindManager : APIRepository<TemplateKind, int, TemplateKindDto, CreateTemplateKindDto>
{
    public TemplateKindManager(HttpClient _http, IToastNotificationService toastNotificationService)
        : base(_http, "TemplateKind", toastNotificationService)
    {
    }
}