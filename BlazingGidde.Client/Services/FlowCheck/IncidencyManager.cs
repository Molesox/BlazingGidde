using BlazingGidde.Shared.Models.FlowCheck;

namespace BlazingGidde.Client.Services.FlowCheck
{
    public class IncidencyManager : APIRepository<Incidency, int>
    {
        public IncidencyManager(HttpClient _http)
            :base (_http, "Incidency")
        { }
    }
}
