using System;
using BlazingGidde.Client.Services;
using BlazingGidde.Shared.Models;
using BlazingGidde.Shared.Repository;
using BlazorBootstrap;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Query;
using AgileObjects.AgileMapper;
using AgileObjects.AgileMapper.Extensions;

namespace BlazingGidde.Client.Shared;

public partial class CustomGrid<TEntity, Tkey, TReadDto, TCreateDto> : ComponentBase
	where Tkey : IEquatable<Tkey>
	where TEntity : class, IModelBase<Tkey>
	where TReadDto : class, IModelBase<Tkey>
	where TCreateDto : class, IModelBase<Tkey>
{
	[Parameter] public IApiRepository<TEntity, Tkey, TReadDto, TCreateDto, TCreateDto, TReadDto, TReadDto> Repository { get; set; } = default!;
	[Parameter] public RenderFragment<TCreateDto>? EditFormTemplate { get; set; }
	[Parameter] public RenderFragment GridColumns { get; set; }
	[Parameter] public string Title { get; set; } = "Item";

	private Modal modal = default!;
	private bool isEditMode = false;
	private TCreateDto? selectedItem = default!;

	private async Task<GridDataProviderResult<TReadDto>> DataProvider(GridDataProviderRequest<TReadDto> request)
	{
		string sortString = request.Sorting?.FirstOrDefault()?.SortString ?? "";
		SortDirection sortDirection = request.Sorting?.FirstOrDefault()?.SortDirection ?? SortDirection.None;

		var query = new QueryFilter<TEntity>
		{
			OrderByDescending = sortDirection == SortDirection.Descending,
			OrderByPropertyName = sortString,
			FilterProperties = request.Filters.Select(filter => new FilterProperty
			{
				CaseSensitive = false,
				Name = filter.PropertyName,
				Operator = MapFilterOperator(filter.Operator) ?? QueryFilterOperator.Contains,
				Value = filter.Value
			}).ToList(),
			PageSize = request.PageSize,
			PageNumber = request.PageNumber
		};

		var result = await Repository.Get(query);
		result.Items ??= new List<TReadDto>();

		return new GridDataProviderResult<TReadDto>
		{
			Data = result.Items,
			TotalCount = result.TotalCount
		};
	}

	private async Task OpenAddModal()
	{
		isEditMode = false;
		var e = Activator.CreateInstance<TEntity>();
		selectedItem = e.Map().ToANew<TCreateDto>();

		await modal?.ShowAsync();
	}

	private async Task OpenEditModal(TReadDto item)
	{
		isEditMode = true;
		selectedItem = Mapper.Map(item).ToANew<TCreateDto>();
		await modal?.ShowAsync();
	}

	private async Task CloseModal()
	{
		await modal?.HideAsync();
		selectedItem = default;
	}

	private async Task SaveItem()
	{
		if (selectedItem is null) return;

		if (isEditMode)
		{
			var updateDto = Mapper.Map(selectedItem).ToANew<TCreateDto>();
			await Repository.Update(updateDto);
		}
		else
		{
			var createDto = Mapper.Map(selectedItem).ToANew<TCreateDto>();
			await Repository.Insert(createDto);
		}

		CloseModal();
		await InvokeAsync(StateHasChanged); // Ensure the UI updates
	}

	private async Task DeleteItem(TReadDto item)
	{
		var idProperty = typeof(TReadDto).GetProperty("Id");
		if (idProperty?.GetValue(item) is Tkey id)
		{
			await Repository.Delete(id);
			await InvokeAsync(StateHasChanged); // Ensure the UI updates
		}
	}

	public static QueryFilterOperator? MapFilterOperator(FilterOperator sourceOperator)
	{
		return sourceOperator switch
		{
			FilterOperator.Equals => QueryFilterOperator.Equals,
			FilterOperator.NotEquals => QueryFilterOperator.NotEquals,
			FilterOperator.LessThan => QueryFilterOperator.LessThan,
			FilterOperator.LessThanOrEquals => QueryFilterOperator.LessThanOrEqual,
			FilterOperator.GreaterThan => QueryFilterOperator.GreaterThan,
			FilterOperator.GreaterThanOrEquals => QueryFilterOperator.GreaterThanOrEqual,
			FilterOperator.Contains => QueryFilterOperator.Contains,
			FilterOperator.StartsWith => QueryFilterOperator.StartsWith,
			FilterOperator.EndsWith => QueryFilterOperator.EndsWith,
			_ => null
		};
	}
}
