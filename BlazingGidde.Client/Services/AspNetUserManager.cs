using BlazingGidde.Shared.DTOs.FlowCheck;
using BlazingGidde.Shared.DTOs.FlowCheck.Request;
using BlazingGidde.Shared.Models.FlowCheck;
using DevExpress.Blazor;
using System.Net.Http.Json;

using static System.Net.WebRequestMethods;

namespace BlazingGidde.Client.Services
{
    public class AspNetUserManager : APIRepository<FlowUser, string, FlowUserDto, CreateFlowUserDto>
    {
        // Constructor que pasa las dependencias a la clase base
        public AspNetUserManager(HttpClient http, IToastNotificationService toastNotificationService)
            : base(http, "AppUsers", toastNotificationService)
        { }

    }
}
