namespace BlazingGidde.Shared.Models;

public interface ISupportTimeStamp
{
    public DateTime? UpdateDate { get; set; }


    public DateTime CreateDate { get; set; }

    public string CreateUser { get; set; }

    public string? UpdateUser { get; set; }
}