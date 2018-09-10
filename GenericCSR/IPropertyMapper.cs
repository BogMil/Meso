using System;
using System.Linq.Expressions;

namespace GenericCSR
{
    public interface IPropertyMapper<TEntity> where TEntity:class
    {
        Expression<Func<TEntity, dynamic>> GetPropertyPathByName(string fieldName);
    }
}
