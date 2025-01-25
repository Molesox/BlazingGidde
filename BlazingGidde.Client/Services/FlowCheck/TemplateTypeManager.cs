using System.Net;
using System.Net.Http.Json;
using BlazingGidde.Shared.API;
using BlazingGidde.Shared.DTOs.FlowCheck;
using BlazingGidde.Shared.DTOs.FlowCheck.Request;
using BlazingGidde.Shared.Models.FlowCheck;
using DevExpress.Blazor;

namespace BlazingGidde.Client.Services.FlowCheck;

public class TemplateTypeManager : APIRepository<TemplateType, int, TemplateTypeDto, CreateTemplateTypeDto>
{
    public TemplateTypeManager(HttpClient _http, IToastNotificationService toastNotificationService)
        : base(_http, "TemplateType", toastNotificationService)
    {
    }

    public async Task<TemplateTypeDto?> GetByCode(int code)
    {
        try
        {
            var encodedId = WebUtility.HtmlEncode(code.ToString());
            var url = $"{controllerName}/getbycode/{encodedId}";
            var response = await http.GetFromJsonAsync<APIEntityResponse<TemplateTypeDto>>(url);
            if (response?.Success == true) return response.Items;
            return default;
        }
        catch (Exception)
        {
            // Optionally log the exception
            return default;
        }
    }
}