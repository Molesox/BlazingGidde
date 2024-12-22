using BlazingGidde.Shared.Models.FlowCheck;

namespace BlazingGidde.Client.Services.FlowCheck
{
    public class TemplateKindManager : APIRepository<TemplateKind, int>
    {
        public TemplateKindManager(HttpClient _http) 
            : base(_http, "TemplateKind") 
        { }
    }
}
