using System;
using System.Linq.Expressions;
using System.Reflection;

namespace GenericCSR

{
    public interface IOrderByProperties<TEntity>
    {
        Func<TEntity,dynamic> OrderByColumnFunc { get; set; }
        string OrderDirection { get; set; }
        PropertyInfo TestPropertyInfo { get; set; }
    }

    public interface IRequestedOrderBy<TEntity>
    {
        IOrderByProperties<TEntity> GetPropertyObject(OrderByProperties orderByProperties);
    }

    public abstract class RequestedOrderBy<TEntity,TPropertyMapper> : 
        IOrderByProperties<TEntity>, IRequestedOrderBy<TEntity> where TPropertyMapper : class,IPropertyMapper<TEntity>, new() 
        where TEntity : class
    {
        Func<TEntity, dynamic> _orderByColumnFunc;
        protected readonly TPropertyMapper PropertyMapper;
        public string OrderDirection { get; set; }

        private PropertyInfo _testPropertyInfo { get; set; }
        public PropertyInfo TestPropertyInfo
        {
            get => _testPropertyInfo ?? GetDefaultPropertyInfo();
            set => _testPropertyInfo = value;
        }

        public Func<TEntity, dynamic> OrderByColumnFunc
        {
            get => _orderByColumnFunc ?? GetDefaultOrderByColumnFunc();
            set => _orderByColumnFunc = value;
        }

        protected RequestedOrderBy()
        {
            PropertyMapper = new TPropertyMapper();
        }

        public IOrderByProperties<TEntity> GetPropertyObject(OrderByProperties orderByProperties)
        {
            if (orderByProperties.OrderByColumn != null)
            {

                var x = PropertyMapper.GetPropertyPathByName(orderByProperties.OrderByColumn);

                var propName = ExpressionPropertyExtractor.GetFullPropertyName(x);
                TestPropertyInfo = PropertyTypeExtractor.GetPropertyInfo(typeof(TEntity), propName);

                OrderByColumnFunc = x.Compile();
            }

            OrderDirection = orderByProperties.OrderDirection ?? SortDirections.Ascending;

            return this;
        }

        private Func<TEntity, dynamic> GetDefaultOrderByColumnFunc()
        {
            var defaultOrderByColumn = GetDefaultOrderByColumn();
            var defaultOrderByColumnPath = ExpressionPropertyExtractor.GetFullPropertyName(defaultOrderByColumn);
            return PropertyMapper.GetPropertyPathByName(defaultOrderByColumnPath).Compile();
        }

        private PropertyInfo GetDefaultPropertyInfo()
        {
            var defaultOrderByColumn = GetDefaultOrderByColumn();
            var defaultOrderByColumnPath = ExpressionPropertyExtractor.GetFullPropertyName(defaultOrderByColumn);

            return PropertyTypeExtractor.GetPropertyInfo(typeof(TEntity), defaultOrderByColumnPath);
        }

        protected abstract Expression<Func<TEntity, dynamic>> GetDefaultOrderByColumn();
    }
}