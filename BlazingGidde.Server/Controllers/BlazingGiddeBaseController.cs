using AgileObjects.AgileMapper;
using AgileObjects.AgileMapper.Extensions;
using BlazingGidde.Shared.API;
using BlazingGidde.Shared.DTOs;
using BlazingGidde.Shared.DTOs.Common;
using BlazingGidde.Shared.Models;
using BlazingGidde.Shared.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazingGidde.Server.Controllers
{
    [ApiController]
    public class BlazingGiddeBaseController<TEntity, Tkey, TDataContext, TReadDto, TCreateDto, TUpdateDto, TCreateDtoResponse, TUpdateDtoReponse>
        : ControllerBase, IBlazingGiddeBaseController<TEntity, Tkey, TDataContext, TReadDto, TCreateDto, TUpdateDto, TCreateDtoResponse, TUpdateDtoReponse>
        where TEntity : class, IModelBase<Tkey>
        where TReadDto : class
        where TCreateDto : class
        where TUpdateDto : class, IModelBase<Tkey>
        where TCreateDtoResponse : class
        where TUpdateDtoReponse : class
        where TDataContext : DbContext
    {
        protected readonly IRepository<TEntity> _repository;
        protected readonly ILogger<BlazingGiddeBaseController<TEntity, Tkey, TDataContext, TReadDto, TCreateDto, TUpdateDto, TCreateDtoResponse, TUpdateDtoReponse>> _logger;

        public BlazingGiddeBaseController(IRepository<TEntity> repository,
         ILogger<BlazingGiddeBaseController<TEntity, Tkey, TDataContext, TReadDto, TCreateDto, TUpdateDto, TCreateDtoResponse, TUpdateDtoReponse>> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        protected async Task<ActionResult<TResponse>> ExecuteAsync<TResponse>(
            Func<Task<TResponse>> func,
            string successMessage,
            string errorMessage)
        {
            try
            {
                var result = await func();
                _logger.LogInformation("{Message}", successMessage);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Message}", errorMessage);
                return StatusCode(500, new { Success = false, ErrorMessages = new List<string> { errorMessage } });
            }
        }

        [HttpGet]
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

        protected virtual IQueryable<TEntity> PrepareQuery(IQueryable<TEntity> query) => query;
        protected virtual IQueryable<TReadDto> MapToReadDto(IQueryable<TEntity> query) => query.Project().To<TReadDto>();

        protected virtual void LogFetchSuccess(int count, string correlationId)
        {
            _logger.LogInformation(
                "Request {CorrelationId}: Fetched {Count} {EntityName} records",
                correlationId,
                count,
                typeof(TEntity).Name);
        }

        [HttpGet("{Id}")]
        public virtual Task<ActionResult<APIEntityResponse<TReadDto>>> GetById([FromRoute] Tkey Id)
        {
            var correlationId = HttpContext.TraceIdentifier;

            return ExecuteAsync(async () =>
            {
                ValidateId(Id, correlationId);
                LogFetchByIdStart(Id, correlationId);

                var entity = await _repository.GetByID(Id);
                if (entity == null)
                {
                    LogEntityNotFound(Id, correlationId);
                    throw new KeyNotFoundException($"Entity with ID {Id} was not found.");
                }

                var readDto = MapEntityToReadDto(entity);

                LogFetchByIdSuccess(Id, correlationId);

                return new APIEntityResponse<TReadDto> { Success = true, Items = readDto };
            },
            $"Request {correlationId}: Fetched {typeof(TEntity).Name} by ID {Id}",
            $"Request {correlationId}: Failed to fetch {typeof(TEntity).Name} by ID {Id}");
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
            => entity.Map().ToANew<TReadDto>();

        protected virtual void LogFetchByIdSuccess(Tkey Id, string correlationId)
        {
            _logger.LogInformation(
                "Request {CorrelationId}: Successfully fetched {EntityName} with Id {Id}",
                correlationId,
                typeof(TEntity).Name,
                Id);
        }

        [HttpPost("getwithfilter")]
        public virtual Task<ActionResult<APIEntityResponse<QueryFilterResponse<TReadDto>>>> GetWithFilter([FromBody] QueryFilter<TEntity> Filter)
        {
            var correlationId = HttpContext.TraceIdentifier;

            return ExecuteAsync(async () =>
            {
                
                var result = await _repository.Get(Filter);
                var readDtos = result.Item1.Map().ToANew<List<TReadDto>>();
                _logger.LogInformation(
                    "Request {CorrelationId}: Fetched {EntityName} with provided filter",
                    correlationId,
                    typeof(TEntity).Name);

                return new APIEntityResponse<QueryFilterResponse<TReadDto>> { Success = true, Items = new QueryFilterResponse<TReadDto>{Items= readDtos, TotalCount = result.Item2 } };
            },
            $"Request {correlationId}: Fetched {typeof(TEntity).Name} with filter",
            $"Request {correlationId}: Failed to fetch {typeof(TEntity).Name} with filter");
        }

        protected virtual void ValidateFilter(QueryFilter<TEntity> filter, string correlationId)
        {
            if (filter == null)
            {
                _logger.LogWarning("Request {CorrelationId}: Filter cannot be null", correlationId);
                throw new ArgumentNullException("Filter cannot be null.");
            }
        }

        [HttpPost("getwithLinqfilter")]
        public virtual Task<ActionResult<APIEntityResponse<QueryFilterResponse<TReadDto>>>> GetWithLinqFilter([FromBody] LinqQueryFilter<TEntity> linqQueryFilter)
        {
            var correlationId = HttpContext.TraceIdentifier;

            return ExecuteAsync(async () =>
            {
                ValidateLinqFilter(linqQueryFilter, correlationId);
                var result = await _repository.Get(linqQueryFilter);
                var readDtos = result.Item1.Map().ToANew<List<TReadDto>>();
                _logger.LogInformation(
                    "Request {CorrelationId}: Fetched {EntityName} with LINQ filter",
                    correlationId,
                    typeof(TEntity).Name);

                return new APIEntityResponse<QueryFilterResponse<TReadDto>> { Success = true, Items = new QueryFilterResponse<TReadDto>() { Items = readDtos, TotalCount = result.Item2 } };
            },
            $"Request {correlationId}: Fetched {typeof(TEntity).Name} with LINQ filter",
            $"Request {correlationId}: Failed to fetch {typeof(TEntity).Name} with LINQ filter");
        }

        protected virtual void ValidateLinqFilter(LinqQueryFilter<TEntity> linqQueryFilter, string correlationId)
        {
            if (linqQueryFilter == null)
            {
                _logger.LogWarning("Request {CorrelationId}: LINQ filter cannot be null", correlationId);
                throw new ArgumentNullException("LINQ filter cannot be null.");
            }
        }

        [HttpPost("GetTotalCount")]
        public virtual Task<ActionResult<APIEntityResponse<CountDto>>> GetTotalCount([FromBody] LinqQueryFilter<TEntity> queryFilter)
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
        public virtual Task<ActionResult<APIEntityResponse<CountDto>>> GetTotalCount([FromBody] QueryFilter<TEntity> queryFilter)
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

        protected virtual void ValidateQueryFilter(object queryFilter, string correlationId)
        {
            if (queryFilter == null)
            {
                _logger.LogWarning("Request {CorrelationId}: Query filter cannot be null", correlationId);
                throw new ArgumentNullException("Query filter cannot be null.");
            }
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
                var readDto = MapEntityToCreateDtoResponse(inserted);

                _logger.LogInformation(
                    "Request {CorrelationId}: Inserted new {EntityName} with Id {Id}",
                    correlationId,
                    typeof(TEntity).Name,
                    inserted.Id);

                return new APIEntityResponse<TCreateDtoResponse> { Success = true, Items = readDto };
            },
            $"Request {correlationId}: Inserted {typeof(TEntity).Name}",
            $"Request {correlationId}: Failed to insert {typeof(TEntity).Name}");
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
            => createDto.Map().ToANew<TEntity>();

        protected virtual void ApplyTimeStampForInsert(TEntity entity)
        {
            if (entity is ISupportTimeStamp ts)
            {
                ts.CreateDate = DateTime.UtcNow;
                ts.CreateUser = User.Identity?.Name ?? "anonymous";
            }
        }

        protected virtual TCreateDtoResponse MapEntityToCreateDtoResponse(TEntity entity)
            => entity.Map().ToANew<TCreateDtoResponse>();

        [HttpPut]
        public virtual Task<ActionResult<APIEntityResponse<TUpdateDtoReponse>>> Put([FromBody] TUpdateDto entity)
        {
            var correlationId = HttpContext.TraceIdentifier;

            return ExecuteAsync(async () =>
            {
                ValidateEntityForUpdate(entity, correlationId);
                var toUpdate = await _repository.GetByID(entity.Id);
                ValidateEntityExists(toUpdate, entity.Id, correlationId);
                MapUpdateDtoToEntity(entity, toUpdate);
                ApplyTimeStampForUpdate(toUpdate);

                var updated = await _repository.Update(toUpdate);
                var updatedDto = MapEntityToUpdateDtoResponse(updated);

                _logger.LogInformation(
                    "Request {CorrelationId}: Updated {EntityName} with Id {Id}",
                    correlationId,
                    typeof(TEntity).Name,
                    toUpdate.Id);

                return new APIEntityResponse<TUpdateDtoReponse> { Success = true, Items = updatedDto };
            },
            $"Request {correlationId}: Updated {typeof(TEntity).Name} with ID {nameof(entity.Id)}",
            $"Request {correlationId}: Failed to update {typeof(TEntity).Name} with ID {nameof(entity.Id)}");
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

        protected virtual void MapUpdateDtoToEntity(TUpdateDto updateDto, TEntity entity)
            => updateDto.Map().Over(entity);

        protected virtual void ApplyTimeStampForUpdate(TEntity entity)
        {
            if (entity is ISupportTimeStamp ts)
            {
                ts.UpdateDate = DateTime.UtcNow;
                ts.UpdateUser = User.Identity?.Name ?? "anonymous";
            }
        }

        protected virtual TUpdateDtoReponse MapEntityToUpdateDtoResponse(TEntity entity)
            => entity.Map().ToANew<TUpdateDtoReponse>();

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

    public class BlazingGiddeBaseController<TEntity, Tkey, TDataContext, TReadDto, TCreateDto>
        : BlazingGiddeBaseController<TEntity, Tkey, TDataContext, TReadDto, TCreateDto, TCreateDto, TReadDto, TReadDto>
        where TEntity : class, IModelBase<Tkey>
        where TReadDto : class, IReadDto<Tkey>
        where TCreateDto : class, ICreateDto<Tkey>
        where TDataContext : DbContext
    {
        public BlazingGiddeBaseController(IRepository<TEntity> repository,
          ILogger<BlazingGiddeBaseController<TEntity, Tkey, TDataContext, TReadDto, TCreateDto, TCreateDto, TReadDto, TReadDto>> logger)
           : base(repository, logger)
        { }
    }

    public class BlazingGiddeBaseController<TEntity, Tkey, TDataContext>
        : BlazingGiddeBaseController<TEntity, Tkey, TDataContext, TEntity, TEntity, TEntity, TEntity, TEntity>
        where TEntity : class, IModelBase<Tkey>
        where TDataContext : DbContext
    {
        public BlazingGiddeBaseController(IRepository<TEntity> repository,
        ILogger<BlazingGiddeBaseController<TEntity, Tkey, TDataContext, TEntity, TEntity, TEntity, TEntity, TEntity>> logger)
            : base(repository, logger)
        { }
    }
}