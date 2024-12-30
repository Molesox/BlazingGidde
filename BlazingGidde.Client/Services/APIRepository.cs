using BlazingGidde.Shared.API;
using BlazingGidde.Shared.DTOs;
using BlazingGidde.Shared.DTOs.Common;
using BlazingGidde.Shared.Models;
using BlazingGidde.Shared.Repository;
using DevExpress.Blazor;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace BlazingGidde.Client.Services
{
	public class APIRepository<TEntity, Tkey, TReadDto, TCreateDto, TUpdateDto, TCreateDtoResponse, TUpdateDtoResponse>
		: IApiRepository<TEntity, Tkey, TReadDto, TCreateDto, TUpdateDto, TCreateDtoResponse, TUpdateDtoResponse>
		where TEntity : class, IModelBase<Tkey>
		where TReadDto : class
		where Tkey : IEquatable<Tkey>
		where TCreateDto : class, IModelBase<Tkey>
		where TUpdateDto : class, IModelBase<Tkey>
		where TCreateDtoResponse : class
		where TUpdateDtoResponse : class
	{
		protected readonly string controllerName;
		protected readonly HttpClient http;

		public APIRepository(HttpClient _http, string _controllerName)
		{
			http = _http;
			controllerName = _controllerName;
		}

		public GridDevExtremeDataSource<TReadDto> Get(){
			try
			{

				var url = new Uri(http.BaseAddress + controllerName);
				return new GridDevExtremeDataSource<TReadDto>(http, url);
			}
			catch (System.Exception e)
			{
				var exception = e;
				return null;
			}
		}

		public async Task<IEnumerable<TReadDto>> GetAll()
		{
            try
            {
				var url = $"{controllerName}/getall";
				var response = await http.GetFromJsonAsync<APIListOfEntityResponse<TReadDto>>(url);
				if (response?.Success == true)
				{
					return response.Items;
				}
				return Enumerable.Empty<TReadDto>();
			}
			catch (Exception)
            {
				// Optionally log the exception
				return Enumerable.Empty<TReadDto>();
			}
		}

		public async Task<TReadDto?> GetByID(object id)
		{
            try
            {
				var encodedId = WebUtility.HtmlEncode(id.ToString());
				var url = $"{controllerName}/{encodedId}";
				var response = await http.GetFromJsonAsync<APIEntityResponse<TReadDto>>(url);
				if (response?.Success == true)
				{
					return response.Items;
				}
				return default;
			}
			catch (Exception)
            {
				// Optionally log the exception
				return default;
			}
		}
	
		public async Task<QueryFilterResponse<TReadDto>> Get(QueryFilter<TEntity> queryFilter)
		{
            try
            {
				var url = $"{controllerName}/getwithfilter";
				var response = await http.PostAsJsonAsync(url, queryFilter);
				response.EnsureSuccessStatusCode();

				var responseBody = await response.Content.ReadAsStringAsync();
				var apiResponse = JsonSerializer.Deserialize<APIEntityResponse<QueryFilterResponse<TReadDto>>>(responseBody, new JsonSerializerOptions
				{
					PropertyNameCaseInsensitive = true
				});

				if (apiResponse?.Success == true)
				{

					return apiResponse.Items!;
				}
				return new QueryFilterResponse<TReadDto>();
			}
			catch (Exception)
            {
				// Optionally log the exception
				return new QueryFilterResponse<TReadDto>();
			}
		}

		public async Task<QueryFilterResponse<TReadDto>> Get(LinqQueryFilter<TEntity> linqQueryFilter)
		{
            try
            {
				var url = $"{controllerName}/getwithLinqfilter";
				var response = await http.PostAsJsonAsync(url, linqQueryFilter);
				response.EnsureSuccessStatusCode();

				var responseBody = await response.Content.ReadAsStringAsync();
				var apiResponse = JsonSerializer.Deserialize<APIEntityResponse<QueryFilterResponse<TReadDto>>>(responseBody, new JsonSerializerOptions
				{
					PropertyNameCaseInsensitive = true
				});

				if (apiResponse?.Success == true)
				{
					return apiResponse.Items!;
				}
				return new QueryFilterResponse<TReadDto>();
			}
			catch (Exception)
            {
				// Optionally log the exception
				return new QueryFilterResponse<TReadDto>();
			}
		}

		public async Task<int> GetTotalCount(QueryFilter<TEntity> queryFilter)
		{
            try
            {
				var url = $"{controllerName}/GetTotalCount";
				var response = await http.PostAsJsonAsync(url, queryFilter);
				response.EnsureSuccessStatusCode();

				var apiResponse = await response.Content.ReadFromJsonAsync<APIEntityResponse<CountDto>>();

				return apiResponse?.Success == true ? apiResponse.Items?.Counter ?? 0 : 0;
			}
			catch (Exception)
            {
				// Optionally log the exception
				return 0;
			}
		}

		public async Task<int> GetTotalCount(LinqQueryFilter<TEntity> linqQueryFilter)
		{
            try
            {
				var url = $"{controllerName}/GetTotalCount";
				var response = await http.PostAsJsonAsync(url, linqQueryFilter);
				response.EnsureSuccessStatusCode();

				var apiResponse = await response.Content.ReadFromJsonAsync<APIEntityResponse<CountDto>>();

				return apiResponse?.Success == true ? apiResponse.Items?.Counter ?? 0 : 0;
			}
			catch (Exception)
            {
				// Optionally log the exception
				return 0;
			}
		}

		public async Task<TCreateDtoResponse?> Insert(TCreateDto createDto)
		{
            try
            {
				var response = await http.PostAsJsonAsync(controllerName, createDto);
				response.EnsureSuccessStatusCode();

				var apiResponse = await response.Content.ReadFromJsonAsync<APIEntityResponse<TCreateDtoResponse>>();

				if (apiResponse?.Success == true)
				{
					return apiResponse.Items;
				}
				return default;
			}
			catch (Exception)
            {
				// Optionally log the exception
				return default;
			}
		}

		public async Task<TUpdateDtoResponse?> Update(TUpdateDto updateDto)
		{
            try
            {
				var url = $"{controllerName}";
				var response = await http.PutAsJsonAsync(url, updateDto);
				response.EnsureSuccessStatusCode();

				var apiResponse = await response.Content.ReadFromJsonAsync<APIEntityResponse<TUpdateDtoResponse>>();

				if (apiResponse?.Success == true)
				{
					return apiResponse.Items;
				}
				return default;
			}
			catch (Exception)
            {
				// Optionally log the exception
				return default;
			}
		}

		public async Task<bool> Delete(object id)
		{
            try
            {
				var encodedID = WebUtility.HtmlEncode(id.ToString());
				var url = $"{controllerName}/{encodedID}";
				var response = await http.DeleteAsync(url);
				response.EnsureSuccessStatusCode();

				return true;
			}
			catch (Exception)
            {
				// Optionally log the exception
				return false;
			}
		}


	}
	
    public class APIRepository<TEntity, Tkey, TReadDto, TCreateDto>
        : APIRepository<TEntity, Tkey, TReadDto, TCreateDto, TCreateDto, TReadDto, TReadDto>
        where TEntity : class, IModelBase<Tkey>
		where Tkey : IEquatable<Tkey>
        where TReadDto : class, IReadDto<Tkey>
		where TCreateDto : class, IModelBase<Tkey>
    {
        public APIRepository(HttpClient _http, string _controllerName) :
		 base(_http, _controllerName)
        {
        }
    }
	 public class APIRepository<TEntity, Tkey>
        : APIRepository<TEntity, Tkey, TEntity, TEntity, TEntity, TEntity, TEntity>
        where TEntity : class, IModelBase<Tkey>
		where Tkey : IEquatable<Tkey>
    {
        public APIRepository(HttpClient _http, string _controllerName) :
		 base(_http, _controllerName)
        {
        }
    }
}