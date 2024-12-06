using System.Reflection;
namespace BlazingGidde.Client;

 
    public class BindConverterWrapper<TEntity>
    {
        private readonly TEntity _entity;
        private readonly PropertyInfo _property;

        public BindConverterWrapper(TEntity entity, PropertyInfo property)
        {
            _entity = entity;
            _property = property;
        }

        public object Value
        {
            get => _property.GetValue(_entity) ?? "";
            set => _property.SetValue(_entity, value);
        }
    }
 