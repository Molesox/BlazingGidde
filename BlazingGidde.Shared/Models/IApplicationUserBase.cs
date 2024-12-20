using BlazingGidde.Shared.Models.PersonMain;

namespace BlazingGidde.Shared.Models;

/// <summary>
/// This base class is not intended to be in db.
/// </summary>
public interface IApplicationUserBase : ISupportTimeStamp
{

    public Person Person { get; set; }

  
}
