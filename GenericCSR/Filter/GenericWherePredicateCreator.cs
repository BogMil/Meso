// ReSharper disable InconsistentNaming

using System;
using ExpressionBuilder.Generics;
using GenericCSR.PropertyMapper;
using Newtonsoft.Json.Linq;

namespace GenericCSR.Filter
{
    public abstract class GenericWherePredicateCreator<TEntity, TPropertyMapper>: IWherePredicateCreator<TEntity>
        where TEntity : class where TPropertyMapper : class, IPropertyMapper<TEntity>, new()
    {
        private Filter<TEntity> WherePredicate { get; set; } = new Filter<TEntity>();
        private FilterCreator<TEntity,TPropertyMapper> _filterCreator;

        public GenericWherePredicateCreator()
        {
            _filterCreator = new FilterCreator<TEntity, TPropertyMapper>();
        }

        public Filter<TEntity> GetWherePredicate(string filters)
        {
            if (filters == null)
                return WherePredicate;

            var jsonFilters = filters.TryParseJToken();
            WherePredicate = _filterCreator.Create(jsonFilters);

            return WherePredicate;
        }

    }
}