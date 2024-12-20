using BlazingGidde.Shared.Models.FlowCheck;

namespace BlazingGidde.Client.Services.FlowCheck
{
    public class TemplateKindManager : APIRepository<TemplateKind>
    {
        public TemplateKindManager(HttpClient _http) 
            : base(_http, "TemplateKind", nameof(TemplateKind.Id)) 
        { }
    }
}
