using BlazingGidde.Shared.Models.FlowCheck;

namespace BlazingGidde.Client.Services.FlowCheck
{
    public class IncidencyManager : APIRepository<Incidency>
    {
        public IncidencyManager(HttpClient _http)
            :base (_http, "Incidency", nameof(Incidency.Id))
        { }
    }
}
