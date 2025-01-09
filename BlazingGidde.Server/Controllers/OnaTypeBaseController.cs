
using BlazingGidde.Server.Data;
using BlazingGidde.Shared.API;
using BlazingGidde.Shared.DTOs;
using BlazingGidde.Shared.Models;
using BlazingGidde.Shared.Models.FlowCheck;
using BlazingGidde.Shared.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlazingGidde.Server.Controllers;


public class OnaTypeBaseController<TEntity, TReadDto, TCreateDto> : OnaBaseController<TEntity, int, ApplicationDbContext, TReadDto, TCreateDto>
where TEntity : class, IModelBase<int>, ISupportType
where TCreateDto : class, ICreateDto<int>
where TReadDto : class, IReadDto<int>
{
    protected ITypeRepositoryEF<TEntity> typeRepository;
    public OnaTypeBaseController(ITypeRepositoryEF<TEntity> repository,
     ILogger<OnaBaseController<TEntity, int, ApplicationDbContext, TReadDto, TCreateDto, TCreateDto, TReadDto, TReadDto>> logger)
     : base(repository, logger)
    {
        typeRepository = repository;
    }

    [HttpGet("getbycode/{code}")]
    public virtual Task<ActionResult<APIEntityResponse<TReadDto>>> GetByCode([FromRoute] int Code)
    {
        var correlationId = HttpContext.TraceIdentifier;

        return ExecuteAsync(async () =>
        {
            //  ValidateId(Id, correlationId);
            // LogFetchByIdStart(Id, correlationId);
            var entity = await typeRepository.GetByCode(Code);
            if (entity == null)
            {
                LogEntityNotFound(Code, correlationId);
                throw new KeyNotFoundException($"Entity with Code {Code} was not found.");
            }

            var readDto = MapEntityToReadDto(entity);

            // LogFetchByIdSuccess(Id, correlationId);

            return new APIEntityResponse<TReadDto> { Success = true, Items = readDto };
        },
        $"Request {correlationId}: Fetched {typeof(TEntity).Name} by Code {Code}",
        $"Request {correlationId}: Failed to fetch {typeof(TEntity).Name} by Code {Code}");
    }
}