using BlazingGidde.Shared.Models;

namespace BlazingGidde.Shared.DTOs;

public interface ICreateDto<Tkey> : IModelBase<Tkey>
    where Tkey : IEquatable<Tkey>
{
}