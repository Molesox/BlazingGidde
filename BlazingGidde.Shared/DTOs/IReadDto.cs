using BlazingGidde.Shared.Models;

namespace BlazingGidde.Shared.DTOs;

public interface IReadDto<Tkey> : IModelBase<Tkey>
    where Tkey : IEquatable<Tkey>
{
}