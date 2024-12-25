using BlazingGidde.Shared.Models;

namespace BlazingGidde.Shared.DTOs;

public interface IUpdateDto<Tkey> : IModelBase<Tkey>
where Tkey : IEquatable<Tkey>
{
}
