﻿//Author: DS
//Year: 2022

namespace BlazingGidde.Shared.API;

/// <summary>
///     This class represents a standard response for an API call with an entity of type TEntity.
/// </summary>
/// <typeparam name="TEntity">Entity type returned by the API.</typeparam>
public class APIEntityResponse<TEntity> where TEntity : class
{
    #region Properties

    /// <summary>
    ///     Gets or sets a value indicating whether the API call was successful.
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    ///     Gets or sets the error messages, if any, resulting from the API call.
    /// </summary>
    public List<string> ErrorMessages { get; set; } = new();

    /// <summary>
    ///     Gets or sets the data resulting from a successful API call.
    /// </summary>
    public TEntity? Items { get; set; }

    #endregion
}