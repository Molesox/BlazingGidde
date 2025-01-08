using BlazingGidde.Shared.Models.Patois;
using DevExpress.Blazor;

namespace BlazingGidde.Client.Services
{
	public class DictionaryManager : APIRepository<DictionaryEntry, int>
	{
		public DictionaryManager(HttpClient _http, IToastNotificationService toastNotificationService)
			: base(_http, "Patois", toastNotificationService)
		{
			
		}
	}
}
