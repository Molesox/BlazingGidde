using Microsoft.EntityFrameworkCore;
using Serialize.Linq.Serializers;
using System.Linq.Expressions;
//Author: DS
//Year: 2022

namespace BlazingGidde.Shared.Repository
{
	public class LinqQueryFilter<TEntity> where TEntity : class
	{

		public LinqQueryFilter(Expression<Func<TEntity, bool>>? expression = null, Expression<Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>>? orderBy = null, List<string>? includeProperties =null)
		{
			var serializer = new ExpressionSerializer(new JsonSerializer());

			LinqExpression = expression is null ? null : serializer.SerializeText(expression);
			IncludePropertyNames = orderBy is null ? null : serializer.SerializeText(orderBy);
			IncludePropertyNames = includeProperties;
		}

		/// <summary>
		/// Gets or sets the linq expression for filtering.
		/// </summary>
		public string? LinqExpression { get; set; }

		/// <summary>
		/// If you want to return a subset of the properties, you can specify only
		/// the properties that you want to retrieve in the SELECT clause.
		/// Leave empty to return all columns
		/// </summary>
		public List<string>? IncludePropertyNames { get; set; }

		/// <summary>
		/// Specify the property to ORDER BY, if any 
		/// </summary>
		public string? OrderByPropertyName { get; set; } = "";

		/// <summary>
		/// Set to true if you want to order DESCENDING
		/// </summary>
		public bool OrderByDescending { get; set; } = false;

		/// <summary>
		/// Number of items to skip (for paging).
		/// </summary>
		public int Skip { get; set; } = 0;

		/// <summary>
		/// Number of items to take (for paging).
		/// </summary>
		public int Take { get; set; } = int.MaxValue;

		public async Task<IEnumerable<TEntity>> GetFilteredList(IQueryable<TEntity> query)
		{
			var serializer = new ExpressionSerializer(new JsonSerializer());
			var deserializedExpression = LinqExpression is null ? null : serializer.DeserializeText(LinqExpression) as Expression<Func<TEntity, bool>>;
			var deserializedOrderBy = OrderByPropertyName is null ? null : serializer.DeserializeText(OrderByPropertyName) as Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>;
			if (deserializedExpression != null)
			{
				query = query.Where(deserializedExpression);
			}

			// Include the specified properties
			foreach (var includeProperty in IncludePropertyNames)
			{
				query = query.Include(includeProperty);
			}

			// order by
			if (deserializedOrderBy != null)
			{
				query = deserializedOrderBy(query);
			}

			// paging
			query = query.Skip(Skip).Take(Take);

			// execute and return the list
			return await query.ToListAsync();
		}

		public async Task<int> GetTotalCount(IQueryable<TEntity> query)
		{
			var serializer = new ExpressionSerializer(new JsonSerializer());
			var deserializedExpression = LinqExpression is null ? null : serializer.DeserializeText(LinqExpression) as Expression<Func<TEntity, bool>>;

			if (deserializedExpression != null)
			{
				query = query.Where(deserializedExpression);
			}

			return await query.CountAsync();
		}
	}
}