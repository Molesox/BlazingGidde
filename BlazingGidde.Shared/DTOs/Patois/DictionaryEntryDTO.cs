using BlazingGidde.Shared.Mappings;

namespace BlazingGidde.Shared.DTOs.Patois;

public class DictionaryEntryDTO : ISupportMapping
{
    public int Id { get; set; }

    public char Parent { get; set; }

    public string? FrenchWord { get; set; }

    public string? DialectWord { get; set; }

    public string? FrenchExample { get; set; }

    public string? DialectExample { get; set; }

    public string? AudioId { get; set; }
}