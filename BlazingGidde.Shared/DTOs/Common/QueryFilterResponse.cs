using System;

namespace BlazingGidde.Shared.DTOs.Common;

public record QueryFilterResponse<TEntity>
{
    public IEnumerable<TEntity> Items { get; set; }
    public int TotalCount { get; set; }
}
