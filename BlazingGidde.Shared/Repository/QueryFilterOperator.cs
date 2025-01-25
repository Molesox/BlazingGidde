//Author: DS
//Year: 2022

namespace BlazingGidde.Shared.Repository;

/// <summary>
///     Specify the compare operator
/// </summary>
public enum QueryFilterOperator
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