using AgileObjects.AgileMapper;
using AgileObjects.AgileMapper.Extensions;
using BlazingGidde.Shared.API;
using BlazingGidde.Shared.DTOs;
using BlazingGidde.Shared.DTOs.Common;
using BlazingGidde.Shared.Models;
using BlazingGidde.Shared.Repository;
using DevExtreme.AspNet.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazingGidde.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class OnaBaseController<TEntity, Tkey, TDataContext, TReadDto, TCreateDto, TUpdateDto, TCreateDtoResponse,
    TUpdateDtoReponse>
    : ControllerBase,
        IOnaBaseController<TEntity, Tkey, TDataContext, TReadDto, TCreateDto, TUpdateDto, TCreateDtoResponse,
            TUpdateDtoReponse>
    where TEntity : class, IModelBase<Tkey>
    where TReadDto : class
    where TCreateDto : class, IModelBase<Tkey>
    where Tkey : IEquatable<Tkey>
    where TUpdateDto : class, IModelBase<Tkey>
    where TCreateDtoResponse : class
    where TUpdateDtoReponse : class
    where TDataContext : DbContext
{
    protected readonly ILogger<OnaBaseController<TEntity, Tkey, TDataContext, TReadDto, TCreateDto, TUpdateDto,
        TCreateDtoResponse, TUpdateDtoReponse>> _logger;

    protected readonly IRepository<TEntity> _repository;

    public OnaBaseController(IRepository<TEntity> repository,
        ILogger<OnaBaseController<TEntity, Tkey, TDataContext, TReadDto, TCreateDto, TUpdateDto, TCreateDtoResponse,
            TUpdateDtoReponse>> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    [HttpGet]
    public virtual async Task<object> Get(DataSourceLoadOptions loadOptions)
    {
        var correlationId = HttpContext.TraceIdentifier;

        return await ExecuteAsync(async () =>
            {
                var query = PrepareQuery(_repository.GetQueryable());
                var items = MapToReadDto(query);

                return await DataSourceLoader.LoadAsync(items, loadOptions);
            },
            $"Request {correlationId}: DxLoaded {typeof(TEntity).Name}",
            $"Request {correlationId}: Failed to DxLoad {typeof(TEntity).Name}");
    }

    [HttpGet("getall")]
    public virtual async Task<ActionResult<APIListOfEntityResponse<TReadDto>>> GetAll()
    {
        var correlationId = HttpContext.TraceIdentifier;

        return await ExecuteAsync(async () =>
            {
                var query = PrepareQuery(_repository.GetQueryable());
                var items = await MapToReadDto(query).ToListAsync();

                LogFetchSuccess(items.Count, correlationId);

                return new APIListOfEntityResponse<TReadDto> { Success = true, Items = items };
            },
            $"Request {correlationId}: Fetched all {typeof(TEntity).Name}",
            $"Request {correlationId}: Failed to fetch all {typeof(TEntity).Name}");
    }


    [HttpGet("{Id}")]
    public virtual Task<ActionResult<APIEntityResponse<TReadDto>>> GetById([FromRoute] Tkey Id)
    {
        var correlationId = HttpContext.TraceIdentifier;

        return ExecuteAsync(async () =>
            {
                ValidateId(Id, correlationId);
                LogFetchByIdStart(Id, correlationId);
                var entity = _repository.GetByID(Id);

                if (entity == null)
                {
                    LogEntityNotFound(Id, correlationId);
                    throw new KeyNotFoundException($"Entity with ID {Id} was not found.");
                }

                var readDto = await MapToReadDto(entity).FirstAsync();

                LogFetchByIdSuccess(Id, correlationId);

                return new APIEntityResponse<TReadDto> { Success = true, Items = readDto };
            },
            $"Request {correlationId}: Fetched {typeof(TEntity).Name} by ID {Id}",
            $"Request {correlationId}: Failed to fetch {typeof(TEntity).Name} by ID {Id}");
    }

    [HttpGet("GetEditModelById/{id}")]
    public virtual Task<ActionResult<APIEntityResponse<TCreateDto>>> GetEditModelById([FromRoute] Tkey Id)
    {
        var correlationId = HttpContext.TraceIdentifier;

        return ExecuteAsync(async () =>
            {
                ValidateId(Id, correlationId);
                LogFetchByIdStart(Id, correlationId);

                var entity = _repository.GetByID(Id);

                if (entity == null)
                {
                    LogEntityNotFound(Id, correlationId);
                    throw new KeyNotFoundException($"Entity with ID {Id} was not found.");
                }

                var createDto = await MapToCreateDto(entity).FirstAsync();

                LogFetchByIdSuccess(Id, correlationId);

                return new APIEntityResponse<TCreateDto> { Success = true, Items = createDto };
            },
            $"Request {correlationId}: Fetched {typeof(TEntity).Name} CreateDto by ID {Id}",
            $"Request {correlationId}: Failed to fetch {typeof(TEntity).Name} CreateDto by ID {Id}");
    }

    [HttpPost("getwithfilter")]
    public virtual Task<ActionResult<APIEntityResponse<QueryFilterResponse<TReadDto>>>> GetWithFilter(
        [FromBody] QueryFilter<TEntity> Filter)
    {
        var correlationId = HttpContext.TraceIdentifier;

        return ExecuteAsync(async () =>
            {
                var query = PrepareQuery(_repository.Get(Filter));
                var items = await MapToReadDto(query).ToListAsync();

                LogFetchSuccess(items.Count, correlationId);

                _logger.LogInformation(
                    "Request {CorrelationId}: Fetched {EntityName} with provided filter",
                    correlationId,
                    typeof(TEntity).Name);

                return new APIEntityResponse<QueryFilterResponse<TReadDto>>
                {
                    Success = true,
                    Items = new QueryFilterResponse<TReadDto> { Items = items, TotalCount = items.Count }
                };
            },
            $"Request {correlationId}: Fetched {typeof(TEntity).Name} with filter",
            $"Request {correlationId}: Failed to fetch {typeof(TEntity).Name} with filter");
    }

    [HttpPost("getwithLinqfilter")]
    public virtual Task<ActionResult<APIEntityResponse<QueryFilterResponse<TReadDto>>>> GetWithLinqFilter(
        [FromBody] LinqQueryFilter<TEntity> linqQueryFilter)
    {
        var correlationId = HttpContext.TraceIdentifier;

        return ExecuteAsync(async () =>
            {
                ValidateLinqFilter(linqQueryFilter, correlationId);

                var query = PrepareQuery(_repository.Get(linqQueryFilter));
                var items = await MapToReadDto(query).ToListAsync();

                LogFetchSuccess(items.Count, correlationId);
                _logger.LogInformation(
                    "Request {CorrelationId}: Fetched {EntityName} with LINQ filter",
                    correlationId,
                    typeof(TEntity).Name);

                return new APIEntityResponse<QueryFilterResponse<TReadDto>>
                {
                    Success = true,
                    Items = new QueryFilterResponse<TReadDto> { Items = items, TotalCount = items.Count }
                };
            },
            $"Request {correlationId}: Fetched {typeof(TEntity).Name} with LINQ filter",
            $"Request {correlationId}: Failed to fetch {typeof(TEntity).Name} with LINQ filter");
    }

    [HttpPost("GetTotalCount")]
    public virtual Task<ActionResult<APIEntityResponse<CountDto>>> GetTotalCount(
        [FromBody] LinqQueryFilter<TEntity> queryFilter)
    {
        var correlationId = HttpContext.TraceIdentifier;

        return ExecuteAsync(async () =>
            {
                ValidateQueryFilter(queryFilter, correlationId);
                var count = await _repository.GetTotalCount(queryFilter);

                _logger.LogInformation(
                    "Request {CorrelationId}: Fetched total count for {EntityName}, count = {Count}",
                    correlationId,
                    typeof(TEntity).Name,
                    count);

                return new APIEntityResponse<CountDto>
                {
                    Success = true,
                    Items = new CountDto { Counter = count }
                };
            },
            $"Request {correlationId}: Fetched total count for {typeof(TEntity).Name}",
            $"Request {correlationId}: Failed to fetch total count for {typeof(TEntity).Name}");
    }

    [HttpPost("GetTotalCount")]
    public virtual Task<ActionResult<APIEntityResponse<CountDto>>> GetTotalCount(
        [FromBody] QueryFilter<TEntity> queryFilter)
    {
        var correlationId = HttpContext.TraceIdentifier;

        return ExecuteAsync(async () =>
            {
                ValidateQueryFilter(queryFilter, correlationId);
                var count = await _repository.GetTotalCount(queryFilter);

                _logger.LogInformation(
                    "Request {CorrelationId}: Fetched total count for {EntityName}, count = {Count}",
                    correlationId,
                    typeof(TEntity).Name,
                    count);

                return new APIEntityResponse<CountDto>
                {
                    Success = true,
                    Items = new CountDto { Counter = count }
                };
            },
            $"Request {correlationId}: Fetched total count for {typeof(TEntity).Name}",
            $"Request {correlationId}: Failed to fetch total count for {typeof(TEntity).Name}");
    }

    [HttpPost]
    public virtual Task<ActionResult<APIEntityResponse<TCreateDtoResponse>>> Post([FromBody] TCreateDto entity)
    {
        var correlationId = HttpContext.TraceIdentifier;

        return ExecuteAsync(async () =>
            {
                ValidateEntityForInsert(entity, correlationId);
                var toInsert = MapCreateDtoToEntity(entity);

                ApplyTimeStampForInsert(toInsert);

                var inserted = await _repository.Insert(toInsert);
                var createResponseDto = MapEntityToCreateDtoResponse(inserted);

                _logger.LogInformation(
                    "Request {CorrelationId}: Inserted new {EntityName} with Id {Id}",
                    correlationId,
                    typeof(TEntity).Name,
                    inserted.Id);

                return new APIEntityResponse<TCreateDtoResponse> { Success = true, Items = createResponseDto };
            },
            $"Request {correlationId}: Inserted {typeof(TEntity).Name}",
            $"Request {correlationId}: Failed to insert {typeof(TEntity).Name}",
            true);
    }

    [HttpPut]
    public virtual Task<ActionResult<APIEntityResponse<TUpdateDtoReponse>>> Put([FromBody] TUpdateDto updateDto)
    {
        var correlationId = HttpContext.TraceIdentifier;

        return ExecuteAsync(async () =>
            {
                ValidateEntityForUpdate(updateDto, correlationId);
                var toUpdate = _repository.GetByID(updateDto.Id).First();

                ValidateEntityExists(toUpdate, updateDto.Id, correlationId);

                MapUpdateDtoToEntity(updateDto, toUpdate);
                ApplyTimeStampForUpdate(toUpdate);

                var updated = await _repository.Update(toUpdate);
                var updatedDto = MapEntityToUpdateDtoResponse(toUpdate);

                _logger.LogInformation(
                    "Request {CorrelationId}: Updated {EntityName} with Id {Id}",
                    correlationId,
                    typeof(TEntity).Name,
                    updated.Id);

                return new APIEntityResponse<TUpdateDtoReponse> { Success = true, Items = updatedDto };
            },
            $"Request {correlationId}: Updated {typeof(TEntity).Name} with ID {updateDto.Id}",
            $"Request {correlationId}: Failed to update {typeof(TEntity).Name} with ID {updateDto.Id}",
            true);
    }

    [HttpDelete("{Id}")]
    public virtual Task<ActionResult<APIEntityResponse<TEntity>>> Delete([FromRoute] Tkey Id)
    {
        var correlationId = HttpContext.TraceIdentifier;

        return ExecuteAsync(async () =>
            {
                ValidateIdForDelete(Id, correlationId);
                var success = await _repository.Delete(Id);
                ValidateDeleteSuccess(success, Id, correlationId);

                _logger.LogInformation(
                    "Request {CorrelationId}: Deleted {EntityName} with Id {Id}",
                    correlationId,
                    typeof(TEntity).Name,
                    Id);

                return new APIEntityResponse<TEntity> { Success = true };
            },
            $"Request {correlationId}: Deleted {typeof(TEntity).Name} with ID {Id}",
            $"Request {correlationId}: Failed to delete {typeof(TEntity).Name} with ID {Id}");
    }

    /// <summary>
    ///     Executes an asynchronous operation with optional model state validation.
    /// </summary>
    /// <typeparam name="TResponse">The type of the response.</typeparam>
    /// <param name="func">The function representing the operation.</param>
    /// <param name="successMessage">The success message for logging.</param>
    /// <param name="errorMessage">The error message for logging.</param>
    /// <param name="validateModel">Flag indicating whether to validate the model state.</param>
    /// <returns>An ActionResult containing the response.</returns>
    protected async Task<ActionResult<TResponse>> ExecuteAsync<TResponse>(
        Func<Task<TResponse>> func,
        string successMessage,
        string errorMessage,
        bool validateModel = false) // New parameter for model validation
    {
        if (validateModel && !ModelState.IsValid)
        {
            // Log the validation errors
            var errors = ModelState.Values.SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();

            _logger.LogWarning("Model validation failed: {Errors}", string.Join("; ", errors));

            // Return a BadRequest with the validation errors
            return BadRequest(new
            {
                Success = false,
                ErrorMessages = errors
            });
        }

        try
        {
            var result = await func();
            _logger.LogInformation("{Message}", successMessage);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Message}", errorMessage);
            return Ok(new { Success = false, ErrorMessages = new List<string> { errorMessage } });
        }
    }

    protected virtual IQueryable<TEntity> PrepareQuery(IQueryable<TEntity> query)
    {
        return query;
    }

    protected virtual IQueryable<TReadDto> MapToReadDto(IQueryable<TEntity> query)
    {
        return query.Project().To<TReadDto>();
    }

    protected virtual IQueryable<TCreateDto> MapToCreateDto(IQueryable<TEntity> query)
    {
        return query.Project().To<TCreateDto>();
    }

    protected virtual void LogFetchSuccess(int count, string correlationId)
    {
        _logger.LogInformation(
            "Request {CorrelationId}: Fetched {Count} {EntityName} records",
            correlationId,
            count,
            typeof(TEntity).Name);
    }

    protected virtual void ValidateId(Tkey Id, string correlationId)
    {
        if (Id is null)
        {
            _logger.LogWarning(
                "Request {CorrelationId}: {Method} called with null Id",
                correlationId,
                nameof(GetById));
            throw new ArgumentException("ID cannot be null or empty.");
        }
    }

    protected virtual void LogFetchByIdStart(Tkey Id, string correlationId)
    {
        _logger.LogInformation(
            "Request {CorrelationId}: Fetching {EntityName} with Id = {Id}",
            correlationId,
            typeof(TEntity).Name,
            Id);
    }

    protected virtual void LogEntityNotFound(Tkey Id, string correlationId)
    {
        _logger.LogInformation(
            "Request {CorrelationId}: No {EntityName} found for Id {Id}",
            correlationId,
            typeof(TEntity).Name,
            Id);
    }

    protected virtual TReadDto MapEntityToReadDto(TEntity entity)
    {
        return entity.Map().ToANew<TReadDto>();
    }

    protected virtual void LogFetchByIdSuccess(Tkey Id, string correlationId)
    {
        _logger.LogInformation(
            "Request {CorrelationId}: Successfully fetched {EntityName} with Id {Id}",
            correlationId,
            typeof(TEntity).Name,
            Id);
    }

    protected virtual void ValidateFilter(QueryFilter<TEntity> filter, string correlationId)
    {
        if (filter == null)
        {
            _logger.LogWarning("Request {CorrelationId}: Filter cannot be null", correlationId);
            throw new ArgumentNullException("Filter cannot be null.");
        }
    }

    protected virtual void ValidateLinqFilter(LinqQueryFilter<TEntity> linqQueryFilter, string correlationId)
    {
        if (linqQueryFilter == null)
        {
            _logger.LogWarning("Request {CorrelationId}: LINQ filter cannot be null", correlationId);
            throw new ArgumentNullException("LINQ filter cannot be null.");
        }
    }

    protected virtual void ValidateQueryFilter(object queryFilter, string correlationId)
    {
        if (queryFilter == null)
        {
            _logger.LogWarning("Request {CorrelationId}: Query filter cannot be null", correlationId);
            throw new ArgumentNullException("Query filter cannot be null.");
        }
    }

    protected virtual void ValidateEntityForInsert(TCreateDto entity, string correlationId)
    {
        if (entity == null)
        {
            _logger.LogWarning(
                "Request {CorrelationId}: Entity cannot be null on Insert",
                correlationId);
            throw new ArgumentNullException("Entity cannot be null.");
        }
    }

    protected virtual TEntity MapCreateDtoToEntity(TCreateDto createDto)
    {
        return Mapper.Map(createDto).ToANew<TEntity>(cfg => cfg.IgnoreEntityKeys());
    }

    protected virtual void ApplyTimeStampForInsert(TEntity entity)
    {
        if (entity is ISupportTimeStamp ts)
        {
            ts.CreateDate = DateTime.UtcNow;
            ts.CreateUser = User.Identity?.Name ?? "anonymous";
        }
    }

    protected virtual TCreateDtoResponse MapEntityToCreateDtoResponse(TEntity entity)
    {
        return entity.Map().ToANew<TCreateDtoResponse>();
    }

    protected virtual void ValidateEntityForUpdate(TUpdateDto entity, string correlationId)
    {
        if (entity == null)
        {
            _logger.LogWarning(
                "Request {CorrelationId}: Entity cannot be null on Update",
                correlationId);
            throw new ArgumentNullException("Entity cannot be null.");
        }
    }

    protected virtual void ValidateEntityExists(TEntity entity, Tkey Id, string correlationId)
    {
        if (entity == null)
        {
            _logger.LogWarning(
                "Request {CorrelationId}: Entity with ID {Id} was not found for update",
                correlationId,
                Id);
            throw new KeyNotFoundException($"Entity with ID {Id} was not found.");
        }
    }

    protected virtual TEntity MapUpdateDtoToEntity(TUpdateDto updateDto, TEntity entity)
    {
        return Mapper.Map(updateDto).Over(entity, cfg => cfg.IgnoreEntityKeys());
    }

    protected virtual void ApplyTimeStampForUpdate(TEntity entity)
    {
        if (entity is ISupportTimeStamp ts)
        {
            ts.UpdateDate = DateTime.UtcNow;
            ts.UpdateUser = User.Identity?.Name ?? "anonymous";
        }
    }

    protected virtual TUpdateDtoReponse MapEntityToUpdateDtoResponse(TEntity entity)
    {
        return entity.Map().ToANew<TUpdateDtoReponse>();
    }

    protected virtual void ValidateIdForDelete(Tkey Id, string correlationId)
    {
        if (Id is null)
        {
            _logger.LogWarning(
                "Request {CorrelationId}: ID is null or empty on Delete",
                correlationId);
            throw new ArgumentException("ID cannot be null or empty.");
        }
    }

    protected virtual void ValidateDeleteSuccess(bool success, Tkey Id, string correlationId)
    {
        if (!success)
        {
            _logger.LogWarning(
                "Request {CorrelationId}: Could not find {EntityName} with Id {Id} to delete",
                correlationId,
                typeof(TEntity).Name,
                Id);
            throw new KeyNotFoundException($"Entity with ID {Id} was not found for deletion.");
        }
    }
}

public class OnaBaseController<TEntity, Tkey, TDataContext, TReadDto, TCreateDto>
    : OnaBaseController<TEntity, Tkey, TDataContext, TReadDto, TCreateDto, TCreateDto, TReadDto, TReadDto>
    where Tkey : IEquatable<Tkey>
    where TEntity : class, IModelBase<Tkey>
    where TReadDto : class, IReadDto<Tkey>
    where TCreateDto : class, ICreateDto<Tkey>
    where TDataContext : DbContext
{
    public OnaBaseController(IRepository<TEntity> repository,
        ILogger<OnaBaseController<TEntity, Tkey, TDataContext, TReadDto, TCreateDto, TCreateDto, TReadDto, TReadDto>>
            logger)
        : base(repository, logger)
    {
    }
}

public class OnaBaseController<TEntity, Tkey, TDataContext>
    : OnaBaseController<TEntity, Tkey, TDataContext, TEntity, TEntity, TEntity, TEntity, TEntity>
    where Tkey : IEquatable<Tkey>
    where TEntity : class, IModelBase<Tkey>
    where TDataContext : DbContext
{
    public OnaBaseController(IRepository<TEntity> repository,
        ILogger<OnaBaseController<TEntity, Tkey, TDataContext, TEntity, TEntity, TEntity, TEntity, TEntity>> logger)
        : base(repository, logger)
    {
    }
}