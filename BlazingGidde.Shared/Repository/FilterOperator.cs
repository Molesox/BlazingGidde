//Author: DS
//Year: 2022

namespace BlazingGidde.Shared.Repository
{
	/// <summary>
	/// Specify the compare operator
	/// </summary>
	public enum FilterOperator
	{
		Equals,
		NotEquals,
		StartsWith,
		EndsWith,
		Contains,
		LessThan,
		GreaterThan,
		LessThanOrEqual,
		GreaterThanOrEqual
	}
}
