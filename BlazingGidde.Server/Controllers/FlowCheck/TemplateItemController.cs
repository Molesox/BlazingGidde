﻿using BlazingGidde.Server.Data;
using FC = BlazingGidde.Shared.Models.FlowCheck;
using BlazingGidde.Shared.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlazingGidde.Server.Controllers.FlowCheck
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class TemplateItemController : BlazingGiddeBaseController<FC.TemplateItem, int, ApplicationDbContext>
    {

        public TemplateItemController(IRepository<FC.TemplateItem> repository,
            ILogger<BlazingGiddeBaseController<FC.TemplateItem, int, ApplicationDbContext>> logger)
            : base(repository, logger) { }
    }
}