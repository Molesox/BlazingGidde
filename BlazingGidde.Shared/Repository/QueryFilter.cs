using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

// Author: DS
// Year: 2022 (Updated)

#nullable disable
namespace BlazingGidde.Shared.Repository;

/// <summary>
///     A serializable filter. An alternative to trying to serialize and deserialize LINQ expressions,
///     which are very finicky. This class uses standard types.
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public class QueryFilter<TEntity> : IQueryFilter<TEntity>
    where TEntity : class
{
    /// <summary>
    ///     If you want to return a subset of the properties, you can specify only
    ///     the properties that you want to retrieve in the SELECT clause.
    ///     Leave empty to return all columns
    /// </summary>
    public List<string> IncludePropertyNames { get; set; } = new();

    /// <summary>
    ///     Defines the property names and values in the WHERE clause
    /// </summary>
    public List<FilterProperty> FilterProperties { get; set; } = new();

    /// <summary>
    ///     Paging properties
    /// </summary>
    public int PageNumber { get; set; }

    public int PageSize { get; set; }

    /// <summary>
    ///     Specify the property to ORDER BY, if any
    /// </summary>
    public string OrderByPropertyName { get; set; } = "";

    /// <summary>
    ///     Set to true if you want to order DESCENDING
    /// </summary>
    public bool OrderByDescending { get; set; } = false;

    public async Task<int> GetTotalCount(IQueryable<TEntity> AllItems)
    {
        var query = ApplyFilters(AllItems);
        return await query.CountAsync();
    }

    public IQueryable<TEntity> GetFilteredList(IQueryable<TEntity> AllItems)
    {
        var query = ApplyFilters(AllItems);

        // Include the specified properties
        if (IncludePropertyNames != null && IncludePropertyNames.Any())
            foreach (var includeProperty in IncludePropertyNames)
                query = query.Include(includeProperty);

        // Order by
        if (!string.IsNullOrEmpty(OrderByPropertyName))
        {
            var parameter = Expression.Parameter(typeof(TEntity), "x");
            var property = Expression.PropertyOrField(parameter, OrderByPropertyName);
            var lambda = Expression.Lambda(property, parameter);

            var methodName = OrderByDescending ? "OrderByDescending" : "OrderBy";

            var resultExpression = Expression.Call(typeof(Queryable), methodName,
                new[] { typeof(TEntity), property.Type },
                query.Expression, Expression.Quote(lambda));

            query = query.Provider.CreateQuery<TEntity>(resultExpression);
        }

        // Apply Pagination
        if (PageSize > 0)
        {
            var skip = (PageNumber - 1) * PageSize;
            query = query.Skip(skip).Take(PageSize);
        }

        return query;
    }

    private IQueryable<TEntity> ApplyFilters(IQueryable<TEntity> query)
    {
        if (FilterProperties != null && FilterProperties.Any())
        {
            var parameter = Expression.Parameter(typeof(TEntity), "x");
            Expression combinedExpression = null;

            foreach (var filterProperty in FilterProperties)
            {
                var propertyName = filterProperty.Name;

                // Use reflection to get the property info
                var propertyInfo = typeof(TEntity).GetProperty(propertyName,
                    BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (propertyInfo == null)
                    // Handle the case where the property does not exist
                    continue;

                var propertyType = propertyInfo.PropertyType;
                var operatorType = filterProperty.Operator;
                var caseSensitive = filterProperty.CaseSensitive;
                var filterValue = ConvertValue(filterProperty.Value, propertyType);

                if (filterValue == null)
                    continue;

                Expression propertyExpression = Expression.Property(parameter, propertyInfo);

                if (IsNullableType(propertyType))
                {
                    propertyExpression = Expression.Property(propertyExpression, "Value");
                    propertyType = Nullable.GetUnderlyingType(propertyType);
                }

                var constant = Expression.Constant(filterValue, propertyType);
                Expression filterExpression = null;

                switch (operatorType)
                {
                    case QueryFilterOperator.Equals:
                        filterExpression = Expression.Equal(propertyExpression, constant);
                        break;
                    case QueryFilterOperator.NotEquals:
                        filterExpression = Expression.NotEqual(propertyExpression, constant);
                        break;
                    case QueryFilterOperator.Contains:
                    case QueryFilterOperator.StartsWith:
                    case QueryFilterOperator.EndsWith:
                        if (propertyType != typeof(string))
                            continue;

                        var method = operatorType switch
                        {
                            QueryFilterOperator.Contains => typeof(string).GetMethod("Contains",
                                new[] { typeof(string) }),
                            QueryFilterOperator.StartsWith => typeof(string).GetMethod("StartsWith",
                                new[] { typeof(string) }),
                            QueryFilterOperator.EndsWith => typeof(string).GetMethod("EndsWith",
                                new[] { typeof(string) }),
                            _ => null
                        };

                        if (method == null)
                            continue;

                        if (!caseSensitive)
                        {
                            propertyExpression = Expression.Call(propertyExpression,
                                typeof(string).GetMethod("ToLower", Type.EmptyTypes));
                            constant = Expression.Constant(filterValue.ToString().ToLower(), typeof(string));
                        }

                        filterExpression = Expression.Call(propertyExpression, method, constant);
                        break;
                    case QueryFilterOperator.LessThan:
                        filterExpression = Expression.LessThan(propertyExpression, constant);
                        break;
                    case QueryFilterOperator.GreaterThan:
                        filterExpression = Expression.GreaterThan(propertyExpression, constant);
                        break;
                    case QueryFilterOperator.LessThanOrEqual:
                        filterExpression = Expression.LessThanOrEqual(propertyExpression, constant);
                        break;
                    case QueryFilterOperator.GreaterThanOrEqual:
                        filterExpression = Expression.GreaterThanOrEqual(propertyExpression, constant);
                        break;
                    default:
                        continue;
                }

                if (filterExpression == null)
                    continue;

                combinedExpression = combinedExpression == null
                    ? filterExpression
                    : Expression.AndAlso(combinedExpression, filterExpression);
            }

            if (combinedExpression != null)
            {
                var lambda = Expression.Lambda<Func<TEntity, bool>>(combinedExpression, parameter);
                query = query.Where(lambda);
            }
        }

        return query;
    }

    /// <summary>
    ///     Converts the string value to the specified type.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    private object ConvertValue(string value, Type type)
    {
        try
        {
            if (type == typeof(string)) return value;

            if (type.IsEnum) return Enum.Parse(type, value, true);

            if (type == typeof(Guid)) return Guid.Parse(value);

            return Convert.ChangeType(value, type);
        }
        catch
        {
            // Handle conversion errors (log if necessary)
            return null;
        }
    }

    /// <summary>
    ///     Checks if a type is nullable.
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    private bool IsNullableType(Type type)
    {
        return Nullable.GetUnderlyingType(type) != null;
    }
}