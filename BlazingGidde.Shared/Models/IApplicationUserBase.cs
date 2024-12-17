using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BlazingGidde.Shared.Models.PersonMain;
using Microsoft.AspNetCore.Identity;

namespace BlazingGidde.Shared.Models;

/// <summary>
/// This base class is not intended to be in db.
/// </summary>
public interface IApplicationUserBase : ISupportTimeStamp
{

    public Person Person { get; set; }

  
}
