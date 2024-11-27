using System;
using BlazingGidde.Shared.DTOs.Patois;
using BlazingGidde.Shared.Models.Patois;

namespace BlazingGidde.Shared.Mappings;

public static class PatoisMappingExpressions 
{
    public static DictionaryEntryDTO MapToDictionaryEntryDTO(this DictionaryEntry entity)
    {
        return new DictionaryEntryDTO
        {
            Id = entity.Id,
            Parent = entity.Parent,
            FrenchWord = entity.FrenchWord,
            DialectWord = entity.DialectWord,
            AudioId = entity.AudioId,
            FrenchExample = entity.FrenchExample,
            DialectExample = entity.DialectExample
        };
    }
}
