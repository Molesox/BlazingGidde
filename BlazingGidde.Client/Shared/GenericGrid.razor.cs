using Microsoft.AspNetCore.Components;
using System.Linq.Expressions;
using BlazingGidde.Shared.Repository;
using BlazingGidde.Client.Services;
using Microsoft.EntityFrameworkCore.Metadata;
using BlazingGidde.Shared.Models;
namespace BlazingGidde.Client.Shared;


public partial class GenericGrid<TEntity, Tkey> : ComponentBase 
where TEntity : class, IModelBase<Tkey>, new()
{
    [Parameter] public APIRepository<TEntity, Tkey, TEntity, TEntity, TEntity, TEntity, TEntity> Repository { get; set; } = default!;
    [Parameter] public string Title { get; set; } = "Data Grid";
    [Parameter] public List<ColumnDefinition<TEntity>> Columns { get; set; } = new();
    [Parameter] public RenderFragment<TEntity>? EditFormTemplate { get; set; }

    // Paging
    private int pageSize = 10;
    private int currentPage = 1;
    private int totalPages = 1;
    private int totalItemCount = 0;

    // Sorting
    private string? sortColumn;
    private bool sortAscending = true;

    // Filtering
    private string searchTerm = string.Empty;

    // Data
    private List<TEntity> items = new();
    private TEntity selectedItem = new();

    // Modal
    private bool showModal = false;
    private bool isEditMode = false;

    protected override async Task OnInitializedAsync()
    {
        await LoadData();
    }

    private async Task LoadData()
    {
        var param = BuildQuery();

        items = (await Repository.Get(param.Item1)).ToList();
        totalItemCount = await Repository.GetTotalCount(param.Item1);

        totalPages = (int)Math.Ceiling((double)totalItemCount / pageSize);
        StateHasChanged();
    }

    private (LinqQueryFilter<TEntity>, Expression<Func<TEntity, bool>>) BuildQuery()
    {
        Expression<Func<TEntity, bool>> filterExp = x => true;

        // If searchTerm is not empty, try filtering on first string column
        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            var stringColumn = Columns.FirstOrDefault(c => c.Property.PropertyType == typeof(string));
            if (stringColumn != null)
            {
                ParameterExpression param = Expression.Parameter(typeof(TEntity), "x");
                var propertyExp = Expression.Property(param, stringColumn.Property);
                var searchExp = Expression.Constant(searchTerm);
                var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                var callExp = Expression.Call(propertyExp, containsMethod!, searchExp);
                filterExp = Expression.Lambda<Func<TEntity, bool>>(callExp, param);
            }
        }

        // Sorting
        Expression<Func<TEntity, object>>? orderByExp = null;
        if (!string.IsNullOrEmpty(sortColumn))
        {
            var col = Columns.FirstOrDefault(c => c.FieldName == sortColumn);
            if (col is not null)
            {
                ParameterExpression param = Expression.Parameter(typeof(TEntity));
                var body = Expression.Convert(Expression.Property(param, col.Property), typeof(object));
                orderByExp = Expression.Lambda<Func<TEntity, object>>(body, param);
            }
        }

        return (new LinqQueryFilter<TEntity>(filterExp, orderByExp)
        {
            Skip = (currentPage - 1) * pageSize,
            Take = pageSize,
            OrderByDescending = !sortAscending,
        }, 
        filterExp);
    }

    private async Task SortByColumn(ColumnDefinition<TEntity> column)
    {
        if (sortColumn == column.FieldName)
        {
            sortAscending = !sortAscending;
        }
        else
        {
            sortColumn = column.FieldName;
            sortAscending = true;
        }
        await LoadData();
    }

    private async Task PrevPage()
    {
        if (currentPage > 1)
        {
            currentPage--;
            await LoadData();
        }
    }

    private async Task NextPage()
    {
        if (currentPage < totalPages)
        {
            currentPage++;
            await LoadData();
        }
    }

    private async Task GoToPage(int page)
    {
        currentPage = page;
        await LoadData();
    }

    private void OpenAddModal()
    {
        selectedItem = new TEntity();
        isEditMode = false;
        showModal = true;
    }

    private void OpenEditModal(TEntity entity)
    {
        selectedItem = CloneEntity(entity);
        isEditMode = true;
        showModal = true;
    }

    private void CloseModal()
    {
        showModal = false;
    }

    private void CloseModalOutside()
    {
        // Close modal if user clicks outside the dialog
        showModal = false;
    }

    private async Task SaveItem()
    {
        if (isEditMode)
        {
            await Repository.Update(selectedItem);
        }
        else
        {
            await Repository.Insert(selectedItem);
        }
        showModal = false;
        await LoadData();
    }

    private async Task DeleteItem(TEntity entity)
    {
        await Repository.Delete(entity.Id);
        await LoadData();
    }

    private TEntity CloneEntity(TEntity source)
    {
        var cloned = new TEntity();
        foreach (var prop in typeof(TEntity).GetProperties().Where(p => p.CanWrite))
        {
            prop.SetValue(cloned, prop.GetValue(source));
        }
        return cloned;
    }
}
