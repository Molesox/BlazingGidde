using System.Text.Json;
using BlazingGidde.Shared.API;
using BlazingGidde.Shared.DTOs.Person;
using BlazingGidde.Shared.DTOs.Person.Request;
using BlazingGidde.Shared.Models.PersonMain;

namespace BlazingGidde.Client.Services;

public class PersonMainManager : APIRepository<Person, int, PersonDto, CreatePersonDto>
{
    public PersonMainManager(HttpClient _http) 
        : base(_http, "Person")
    {
    }

}
