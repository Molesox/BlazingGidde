using BlazingGidde.Shared.Models.Patois;

namespace BlazingGidde.Client.Services
{
	public class DictionaryManager : APIRepository<DictionaryEntry>
	{
		public DictionaryManager(HttpClient _http)
			: base(_http, "Patois", nameof(DictionaryEntry.Id))
		{
			
		}
	}
}
