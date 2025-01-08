using BlazingGidde.Shared.Models.FlowCheck;
using DevExpress.Blazor;

namespace BlazingGidde.Client.Services.FlowCheck
{
    public class IncidencyManager : APIRepository<Incidency, int>
    {
        public IncidencyManager(HttpClient _http, IToastNotificationService toastNotificationService)
            :base (_http, "Incidency", toastNotificationService)
        { }
    }
}
