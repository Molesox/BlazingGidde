using System;
using BlazingGidde.Shared.Models.PersonMain;

namespace BlazingGidde.Client.Services;

public class PersonMainManager : APIRepository<Person>
{
    public PersonMainManager(HttpClient _http) 
    : base(_http, "Person", nameof(Person.PersonID))
    {
    }
}
