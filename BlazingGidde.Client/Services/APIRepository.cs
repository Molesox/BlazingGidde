using BlazingGidde.Shared.API;
using BlazingGidde.Shared.Repository;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;


namespace BlazingGidde.Client.Services
{
	public class APIRepository<TEntity> : IRepository<TEntity>
		where TEntity : class
	{
		protected string controllerName;
		protected HttpClient http;
		protected string primaryKeyName;

		public APIRepository(HttpClient _http, string _controllerName, string _primaryKeyName)
		{
			http = _http;
			controllerName = _controllerName;
			primaryKeyName = _primaryKeyName;
		}

		public async Task<IEnumerable<TEntity>> GetAll()
		{
			try
			{
				var result = await http.GetAsync(controllerName);
				result.EnsureSuccessStatusCode();

				var responseBody = await result.Content.ReadAsStringAsync();
				var response = JsonSerializer.Deserialize<APIListOfEntityResponse<TEntity>>(responseBody, new JsonSerializerOptions
				{
					PropertyNameCaseInsensitive = true
				});

				if (response is not null && response.Success)
				{
					return response.Items;
				}

				return new List<TEntity>();
			}
			catch (Exception)
			{
				return new List<TEntity>();
			}
		}

		public async Task<TEntity?> GetByID(object id)
		{
			try
			{
				var encodedId = WebUtility.HtmlEncode(id.ToString());
				var url = @$"{controllerName}/{encodedId}";

				var result = await http.GetAsync(url);
				result.EnsureSuccessStatusCode();

				var responseBody = await result.Content.ReadAsStringAsync();
				var response = JsonSerializer.Deserialize<APIEntityResponse<TEntity>>(responseBody, new JsonSerializerOptions
				{
					PropertyNameCaseInsensitive = true
				});

				if (response is not null && response.Success)
				{
					return response.Items;
				}

				return null;
			}
			catch (Exception)
			{
				return null;
			}
		}

		public async Task<IEnumerable<TEntity>> Get(LinqQueryFilter<TEntity> linqQueryFilter)
		{
			try
			{
				var url = $"{controllerName}/getwithLinqfilter";
				var result = await http.PostAsJsonAsync(url, linqQueryFilter);
				result.EnsureSuccessStatusCode();

				var responseBody = await result.Content.ReadAsStringAsync();
				var response = JsonSerializer.Deserialize<APIListOfEntityResponse<TEntity>>(responseBody, new JsonSerializerOptions
				{
					PropertyNameCaseInsensitive = true
				});

				if (response is not null && response.Success)
				{
					return response.Items;
				}

				return new List<TEntity>();
			}
			catch (Exception)
			{
				return new List<TEntity>();
			}
		}
		public async Task<int> GetTotalCount(LinqQueryFilter<TEntity> linqQueryFilter)
		{
			try
			{
				var url = $"{controllerName}/GetTotalCount";
				var result = await http.PostAsJsonAsync(url, linqQueryFilter);
				result.EnsureSuccessStatusCode();

				var responseBody = await result.Content.ReadAsStringAsync();
				var response = JsonSerializer.Deserialize<int>(responseBody, new JsonSerializerOptions
				{
					PropertyNameCaseInsensitive = true
				});
				return response;
			}
			catch (Exception)
			{
				return 0;
			}
		}
		public async Task<IEnumerable<TEntity>> Get(QueryFilter<TEntity> queryFilter)
		{
			try
			{
				var url = $"{controllerName}/getwithfilter";
				var result = await http.PostAsJsonAsync(url, queryFilter);
				result.EnsureSuccessStatusCode();

				var responseBody = await result.Content.ReadAsStringAsync();
				var response = JsonSerializer.Deserialize<APIListOfEntityResponse<TEntity>>(responseBody, new JsonSerializerOptions
				{
					PropertyNameCaseInsensitive = true
				});

				if (response is not null && response.Success)
				{
					return response.Items;
				}

				return new List<TEntity>();
			}
			catch (Exception)
			{
				return new List<TEntity>();
			}
		}

		public async Task<TEntity?> Insert(TEntity entity)
		{
			try
			{
				var result = await http.PostAsJsonAsync(controllerName, entity);
				result.EnsureSuccessStatusCode();

				var responseBody = await result.Content.ReadAsStringAsync();
				var response = JsonSerializer.Deserialize<APIEntityResponse<TEntity>>(responseBody, new JsonSerializerOptions
				{
					PropertyNameCaseInsensitive = true
				});

				if (response is not null && response.Success)
				{
					return response.Items!;
				}

				return null;
			}
			catch (Exception)
			{
				return null;
			}
		}

		public virtual async Task<TEntity?> Update(TEntity entityToUpdate)
		{
			try
			{
				var result = await http.PutAsJsonAsync(controllerName, entityToUpdate);
				result.EnsureSuccessStatusCode();

				var responseBody = await result.Content.ReadAsStringAsync();
				var response = JsonSerializer.Deserialize<APIEntityResponse<TEntity>>(responseBody, new JsonSerializerOptions
				{
					PropertyNameCaseInsensitive = true
				});

				if (response is not null && response.Success)
				{
					return response.Items!;
				}

				return null;
			}
			catch (Exception)
			{
				return null;
			}
		}

		public async Task<bool> Delete(TEntity entityToDelete)
		{
			try
			{
				var idFromEntity = default(string);

				var property = entityToDelete.GetType().GetProperty(primaryKeyName);

				if (property is not null)
				{
					var propertyValue = property.GetValue(entityToDelete);
					idFromEntity = propertyValue?.ToString();
				}

				if (idFromEntity is null)
				{
					throw new Exception("Id missing from entity");
				}

				var encodedID = WebUtility.HtmlEncode(idFromEntity);
				var url = $@"{controllerName}/{encodedID}";

				var result = await http.DeleteAsync(url);
				result.EnsureSuccessStatusCode();

				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public async Task<bool> Delete(object id)
		{
			try
			{
				var encodedID = WebUtility.HtmlEncode(id.ToString());
				var url = @$"{controllerName}/{encodedID}";

				var result = await http.DeleteAsync(url);
				result.EnsureSuccessStatusCode();

				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public IQueryable<TEntity> GetAllQueryable()
		{
			// IQueryable cannot be used across API boundaries.
			throw new InvalidOperationException(
				@"IQueryable cannot be used in the API repository.
				 Use the LinqQueryFilter or a similar mechanism to perform filtering over HTTP.");
		}
	}
}
