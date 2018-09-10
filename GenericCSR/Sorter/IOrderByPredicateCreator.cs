namespace GenericCSR

{
    public interface IOrderByPredicateCreator<TEntity>
    {
        IOrderByProperties<TEntity> GetPropertyObject(OrderByProperties orderByProperties);
    }
}