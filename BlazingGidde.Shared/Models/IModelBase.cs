namespace BlazingGidde.Shared.Models;

public interface IModelBase<Tkey>
{
    public Tkey Id { get; set; }
}
