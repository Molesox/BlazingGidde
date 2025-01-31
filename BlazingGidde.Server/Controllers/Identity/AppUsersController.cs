﻿using BlazingGidde.Server.Data;
using BlazingGidde.Shared.DTOs.FlowCheck;
using BlazingGidde.Shared.DTOs.FlowCheck.Request;
using BlazingGidde.Shared.Models.FlowCheck;
using BlazingGidde.Shared.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlazingGidde.Server.Controllers.Identity;

[ApiController]
[Route("[controller]")]
[Authorize]
public class
    AppUsersController : OnaBaseController<FlowUser, string, ApplicationDbContext, FlowUserDto, CreateFlowUserDto>
{
    public AppUsersController(IUserRepository<FlowUser> repository,
        ILogger<OnaBaseController<FlowUser, string, ApplicationDbContext, FlowUserDto, CreateFlowUserDto>> logger
    )
        : base(repository, logger)
    {
    }
}