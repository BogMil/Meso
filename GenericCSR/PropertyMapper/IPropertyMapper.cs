using System;
using System.Linq.Expressions;

namespace GenericCSR.PropertyMapper
{
    public interface IPropertyMapper<TEntity> where TEntity:class
    {
        Expression<Func<TEntity, dynamic>> GetPathInEfForDtoFieldExpression(string dtoFieldName);
    }
}
