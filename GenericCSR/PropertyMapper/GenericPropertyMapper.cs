using System;
using System.Linq.Expressions;

namespace GenericCSR.PropertyMapper
{
    public abstract class GenericPropertyMapper<TEntity,TQueryDto> : IPropertyMapper<TEntity> 
        where TEntity : class
    {
        public abstract Expression<Func<TEntity, dynamic>> GetPathInEfForDtoFieldExpression(string dtoFieldName);

        public string GetDtoPropertyPathAsString(Expression<Func<TQueryDto, dynamic>> exp)
        {
            return exp.ExpressionBodyToString();
        }
    }
}
