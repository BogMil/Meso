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
        string OrderByProperty { get; set; }
    }
}