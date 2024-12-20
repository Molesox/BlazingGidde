using BlazingGidde.Shared.Models.FlowCheck;

namespace BlazingGidde.Client.Services.FlowCheck
{
    public class TemplateTypeManager : APIRepository<TemplateType>
    {
        public TemplateTypeManager(HttpClient _http)
            : base(_http, "TemplateType", nameof(TemplateType.Id))
        { }
    }
}
