using System;
using System.Text.Json;
using BlazingGidde.Shared.API;
using BlazingGidde.Shared.Models.PersonMain;

namespace BlazingGidde.Client.Services;

public class PersonMainManager : APIRepository<Person>
{
    public PersonMainManager(HttpClient _http) 
        : base(_http, "Person", nameof(Person.PersonID))
    {
    }

    public async Task<IEnumerable<Phone>> GetPhonesByPersonId(int personId)
    {
        try
        {
            var url = $"{controllerName}/{personId}/phones";
            var result = await http.GetAsync(url);
            result.EnsureSuccessStatusCode();

            var responseBody = await result.Content.ReadAsStringAsync();
            var response = JsonSerializer.Deserialize<APIListOfEntityResponse<Phone>>(responseBody, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (response != null && response.Success)
            {
                return response.Items;
            }
            else
            {
                return new List<Phone>();
            }
        }
        catch (Exception)
        {
            // Handle exceptions or log errors as needed
            return new List<Phone>();
        }
    }

    public async Task<IEnumerable<Email>> GetEmailsByPersonId(int personId)
    {
        try
        {
            var url = $"{controllerName}/{personId}/emails";
            var result = await http.GetAsync(url);
            result.EnsureSuccessStatusCode();

            var responseBody = await result.Content.ReadAsStringAsync();
            var response = JsonSerializer.Deserialize<APIListOfEntityResponse<Email>>(responseBody, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (response != null && response.Success)
            {
                return response.Items;
            }
            else
            {
                return new List<Email>();
            }
        }
        catch (Exception)
        {
            // Handle exceptions or log errors as needed
            return new List<Email>();
        }
    }
}
