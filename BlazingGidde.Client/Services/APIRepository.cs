using BlazingGidde.Shared.API;
using BlazingGidde.Shared.DTOs;
using BlazingGidde.Shared.DTOs.Common;
using BlazingGidde.Shared.Models;
using BlazingGidde.Shared.Models.FlowCheck;
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
		protected readonly IToastNotificationService notificationService;

		public APIRepository(HttpClient _http, string _controllerName, IToastNotificationService toastNotificationService)
		{
			http = _http;
			controllerName = _controllerName;
			notificationService = toastNotificationService;
		}

		public GridDevExtremeDataSource<TReadDto> Get()
		{
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

		public async Task<TCreateDto?> GetEditModelByID(object id)
		{
			try
			{
				var encodedId = WebUtility.HtmlEncode(id.ToString());
				var url = $"{controllerName}/GetEditModelById/{encodedId}";
				var response = await http.GetFromJsonAsync<APIEntityResponse<TCreateDto>>(url);
				if (response?.Success == true)
				{
					return response.Items;
				}
				return default;
			}
			catch (Exception ex)
			{
				var e = ex.Message;
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

			if (response.IsSuccessStatusCode)
			{
				var apiResponse = await response.Content.ReadFromJsonAsync<APIEntityResponse<TCreateDtoResponse>>();

				if (apiResponse?.Success == true)
				{
					return apiResponse.Items;
				}
				else
				{
					var errorMessages = apiResponse?.ErrorMessages ?? new List<string>();
					notificationService.ShowToast(new ToastOptions()
					{
						ProviderName = "Overview",
						ThemeMode = ToastThemeMode.Saturated,
						RenderStyle = ToastRenderStyle.Danger,
						Title = string.Join(", ", errorMessages),
					});
				}
			}
			else
			{
				// Handle validation errors
				var validationErrors = await response.Content.ReadFromJsonAsync<ValidationErrorResponse>();
				if (validationErrors?.Errors != null)
				{
					var errorList = validationErrors.Errors
						.SelectMany(err => err.Value)
						.ToList();

					notificationService.ShowToast(new ToastOptions()
					{
						ProviderName = "Overview",
						ThemeMode = ToastThemeMode.Saturated,
						RenderStyle = ToastRenderStyle.Danger,
						Title = "Validation Errors",

						 Text = string.Join("\n", errorList),
					});
				}
			}

			return default;
		}
		catch (Exception ex)
		{
			// Optionally log the exception
			notificationService.ShowToast(new ToastOptions()
			{
				ProviderName = "Overview",
				ThemeMode = ToastThemeMode.Saturated,
				RenderStyle = ToastRenderStyle.Warning,
				Title = "Error",
				Text = ex.Message
			});
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
				else
				{
					notificationService.ShowToast(new ToastOptions()
					{
						ProviderName = "Overview",
						ThemeMode = ToastThemeMode.Saturated,
						RenderStyle = ToastRenderStyle.Danger,
						Title = string.Join(", ", apiResponse.ErrorMessages),
					});
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
		public APIRepository(HttpClient _http, string _controllerName, IToastNotificationService toastNotificationService) :
		 base(_http, _controllerName, toastNotificationService)
		{
		}
	}
	public class APIRepository<TEntity, Tkey>
	   : APIRepository<TEntity, Tkey, TEntity, TEntity, TEntity, TEntity, TEntity>
	   where TEntity : class, IModelBase<Tkey>
	   where Tkey : IEquatable<Tkey>
	{
		public APIRepository(HttpClient _http, string _controllerName, IToastNotificationService toastNotificationService) :
		 base(_http, _controllerName, toastNotificationService)
		{
		}
	}
}