using System;
using BlazingGidde.Client.Services;
using BlazingGidde.Shared.Models;
using BlazingGidde.Shared.Repository;
using BlazorBootstrap;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Query;

namespace BlazingGidde.Client.Shared;

public partial class CustomGrid<TEntity, Tkey, TReadDto, TCreateDto> : ComponentBase
where TEntity : class, IModelBase<Tkey>
where TReadDto : class, IModelBase<Tkey>
where TCreateDto : class, IModelBase<Tkey>
{
    [Parameter] public IApiRepository<TEntity, Tkey, TReadDto, TCreateDto, TReadDto, TReadDto, TReadDto> Repository { get; set; } = default!;

    [Parameter] public RenderFragment<TCreateDto>? EditFormTemplate { get; set; }

    [Parameter] public RenderFragment GridColumns { get; set; }

    [Parameter] public string Title { get; set; } = "Item";
    // Modal
    private bool showModal = false;
    private bool isEditMode = false;

    private TCreateDto? selectedItem = default!;

    private async Task<GridDataProviderResult<TReadDto>> DataProvider(GridDataProviderRequest<TReadDto> request)
    {
        string sortString = "";
        SortDirection sortDirection = SortDirection.None;

        if (request.Sorting is not null && request.Sorting.Any())
        {
            // Note: Multi column sorting is not supported at this moment
            sortString = request.Sorting.FirstOrDefault()!.SortString;
            sortDirection = request.Sorting.FirstOrDefault()!.SortDirection;
        }

        var query = new QueryFilter<TEntity>()
        {
            OrderByDescending = sortDirection == SortDirection.Descending ? true : false,
            OrderByPropertyName = sortString,
            FilterProperties = new List<FilterProperty>(request.Filters.Count()),
            PageSize = request.PageSize,
            PageNumber = request.PageNumber
        };

        foreach (var filter in request.Filters)
        {
            var filterProperty = new FilterProperty()
            {
                CaseSensitive = false,
                Name = filter.PropertyName,
                Operator = MapFilterOperator(filter.Operator) ?? QueryFilterOperator.Contains,
                Value = filter.Value
            };

            query.FilterProperties.Add(filterProperty);
        }


        var result = await Repository.Get(query);
        if (result.Items is null)
        {
            result.Items = new List<TReadDto>();
        }

        return await Task.FromResult(new GridDataProviderResult<TReadDto> { Data = result.Items, TotalCount = result.TotalCount });
    }
   private void OpenAddModal()
    {
        isEditMode = false;
        selectedItem = Activator.CreateInstance<TCreateDto>();
        showModal = true;
    }

    private async Task OpenEditModal(TReadDto item)
    {
        isEditMode = true;
        selectedItem = (TCreateDto)(object)item;
        showModal = true;
    }

    private void CloseModal()
    {
        showModal = false;
        selectedItem = default;
    }

    private void CloseModalOutside()
    {
        CloseModal();
    }

    private async Task SaveItem()
    {
        if (isEditMode && selectedItem is TReadDto updateDto)
        {
            await Repository.Update(updateDto);
        }
        else if (selectedItem is TCreateDto createDto)
        {
            await Repository.Insert(createDto);
        }
        showModal = false;
        selectedItem = default;
        await InvokeAsync(StateHasChanged); // Ensure the UI updates
    }

    private async Task DeleteItem(TReadDto item)
    {
        var idProperty = typeof(TReadDto).GetProperty("Id");
        if (idProperty != null)
        {
            var id = idProperty.GetValue(item);
            if (id != null)
            {
                await Repository.Delete(id);
                    await InvokeAsync(StateHasChanged); // Ensure the UI updates
            }
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
            _ => null // Handle unmapped cases as needed
        };
    }
}
