namespace BlazingGidde.Shared.DTOs.Person;

public record PersonDto : IReadDto<int>
{
    public int PersonId { get; set; }

    public int PersonTypeId { get; set; }

    public string Culture { get; set; }

    public string Title { get; set; }

    public string LastName { get; set; }

    public string FirstName { get; set; }

    public string FullName => $"{LastName}, {FirstName}";

    public string DefaultEmail { get; set; }

    public string DefaultAddress { get; set; }

    public int DefaultPhone { get; set; }

    public int Id
    {
        get => PersonId;
        set => PersonId = value;
    }
}