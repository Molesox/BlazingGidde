namespace BlazingGidde.Shared.Models;

public interface IModelBase<Tkey> where Tkey : IEquatable<Tkey>
{
    public Tkey Id { get; set; }
}
