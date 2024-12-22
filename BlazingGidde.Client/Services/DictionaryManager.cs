using BlazingGidde.Shared.Models.Patois;

namespace BlazingGidde.Client.Services
{
	public class DictionaryManager : APIRepository<DictionaryEntry, int>
	{
		public DictionaryManager(HttpClient _http)
			: base(_http, "Patois")
		{
			
		}
	}
}
