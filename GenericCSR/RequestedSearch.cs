// ReSharper disable InconsistentNaming

using System;
using ExpressionBuilder.Generics;
using Newtonsoft.Json.Linq;

namespace GenericCSR
{ 
    public interface IRequestedSearch<TEntity> where TEntity : class
    {
        Filter<TEntity> GetFilterExpression(string filters);
    }
    public abstract class RequestedSearch<TEntity, TPropertyMapper>: IRequestedSearch<TEntity>
        where TEntity : class where TPropertyMapper : class, IPropertyMapper<TEntity>, new()
    {
        private JToken _jsonFilters;
        private Filter<TEntity> FilterExpression { get; set; } = new Filter<TEntity>();

        public Filter<TEntity> GetFilterExpression(string filters)
        {
            if (filters == null)
                return FilterExpression;

            _jsonFilters = TryParseJsonFromFilters(filters);

            if (_jsonFilters != null)
                FilterExpression = new FilterExpressionCreator<TEntity, TPropertyMapper>().Create(_jsonFilters);

            return FilterExpression;
        }

        private JToken TryParseJsonFromFilters(string filters)
        {
            JToken jsonFilter = null;
            try
            {
                jsonFilter = JToken.Parse(filters);
            }
            catch
            {
                throw new Exception("Format filtera nije JSON");
            }

            return jsonFilter;
        }
     
    }
}