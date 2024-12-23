using Microsoft.EntityFrameworkCore;
using Serialize.Linq.Serializers;
using System.Linq.Expressions;
// Author: DSanz
// Year: 2022
// Updated: 5 Dec 2023 by DSanz

namespace BlazingGidde.Shared.Repository
{
    /// <summary>
    /// Provides a generic LINQ query filter with support for serialization.
    /// </summary>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    public class LinqQueryFilter<TEntity> : IQueryFilter<TEntity>
    where TEntity : class
    {
        #region Constructors

        /// <summary>
        /// Default constructor required for serialization. Do NOT DELETE THIS.
        /// </summary>
        public LinqQueryFilter()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LinqQueryFilter{TEntity}"/> class with the specified parameters.
        /// </summary>
		/// 
        /// <param name="expression">The LINQ expression for filtering.</param>
        /// <param name="orderBy">The LINQ expression for ordering.</param>
        /// <param name="includeProperties">The list of related properties to include.</param>
        public LinqQueryFilter(
            Expression<Func<TEntity, bool>>? expression = null,
            Expression<Func<TEntity, object>>? orderBy = null,
            List<string>? includeProperties = null)
        {
            var serializer = new ExpressionSerializer(new JsonSerializer());

            LinqExpression = expression is null ? null : serializer.SerializeText(expression);
            OrderByPropertyName = orderBy is null ? null : serializer.SerializeText(orderBy);
            IncludePropertyNames = includeProperties;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the serialized LINQ expression for filtering.
        /// </summary>
        public string? LinqExpression { get; set; }

        /// <summary>
        /// Gets or sets the list of related properties to include in the query.
        /// Leave empty to include all properties.
        /// </summary>
        public List<string>? IncludePropertyNames { get; set; }

        /// <summary>
        /// Gets or sets the serialized LINQ expression for ordering.
        /// </summary>
        public string? OrderByPropertyName { get; set; } = "";

        /// <summary>
        /// Gets or sets a value indicating whether to order by descending.
        /// </summary>
        public bool OrderByDescending { get; set; } = false;

        /// <summary>
        /// Gets or sets the number of items to skip (for paging).
        /// </summary>
        public int Skip { get; set; } = 0;

        /// <summary>
        /// Gets or sets the number of items to take (for paging).
        /// </summary>
        public int Take { get; set; } = int.MaxValue;

        #endregion

        #region Methods

        /// <summary>
        /// Applies the filter, ordering, includes, and paging to the query and returns the result as a list asynchronously.
        /// </summary>
        /// <param name="query">The queryable source.</param>
        /// <returns>A task representing the asynchronous operation, with a result of the filtered list.</returns>
        public async Task<(IEnumerable<TEntity>, int)> GetFilteredList(IQueryable<TEntity> query)
        {
            var serializer = new ExpressionSerializer(new JsonSerializer());

            // Deserialize the filter expression
            var deserializedExpression = LinqExpression is null
                ? null
                : serializer.DeserializeText(LinqExpression) as Expression<Func<TEntity, bool>>;

            // Deserialize the order by expression
            var deserializedOrderBy = string.IsNullOrEmpty(OrderByPropertyName)
                ? null
                : serializer.DeserializeText(OrderByPropertyName) as Expression<Func<TEntity, object>>;

            // Apply the filter
            if (deserializedExpression != null)
            {
                query = query.Where(deserializedExpression);
            }

            var totalCount = await query.CountAsync();

            // Include the specified properties
            if (IncludePropertyNames != null)
            {
                foreach (var propName in IncludePropertyNames)
                {
                    query = query.Include(propName);
                }
            }

            // Apply ordering
            if (deserializedOrderBy != null)
            {
                query = OrderByDescending
                    ? query.OrderByDescending(deserializedOrderBy)
                    : query.OrderBy(deserializedOrderBy);
            }

            // Apply paging
            query = query.Skip(Skip).Take(Take);

            // Execute the query and return the list
            return (await query.ToListAsync(), totalCount);
        }

        /// <summary>
        /// Gets the total count of items that match the filter asynchronously.
        /// </summary>
        /// <param name="query">The queryable source.</param>
        /// <returns>A task representing the asynchronous operation, with a result of the total count.</returns>
        public async Task<int> GetTotalCount(IQueryable<TEntity> query)
        {
            var serializer = new ExpressionSerializer(new JsonSerializer());

            // Deserialize the filter expression
            var deserializedExpression = LinqExpression is null
                ? null
                : serializer.DeserializeText(LinqExpression) as Expression<Func<TEntity, bool>>;

            // Apply the filter
            if (deserializedExpression != null)
            {
                query = query.Where(deserializedExpression);
            }

            // Return the total count
            return await query.CountAsync();
        }



        #endregion
    }
}