using System;
using System.Linq.Expressions;
using System.Reflection;
namespace BlazingGidde.Client;
public  class ColumnDefinition<TEntity>
{
    public string Title { get; set; } = string.Empty;
    public string FieldName { get; set; } = string.Empty;
    public PropertyInfo Property { get; set; }
    public bool IsEditable { get; set; }

    public Func<TEntity, object?> PropertyValue { get; }
    public Func<TEntity, object> PropertyValueEdit { get; }

    public ColumnDefinition(Expression<Func<TEntity, object?>> expression, string title, bool isEditable = false)
    {
        Title = title;
        IsEditable = isEditable;

        var member = expression.Body as MemberExpression ?? (expression.Body as UnaryExpression)?.Operand as MemberExpression;
        if (member is null)
            throw new ArgumentException("Invalid expression");

        Property = (PropertyInfo)member.Member;
        FieldName = Property.Name;

        PropertyValue = (entity) => Property.GetValue(entity);
 
        PropertyValueEdit = (entity) => new BindConverterWrapper<TEntity>(entity, Property);
    }
}