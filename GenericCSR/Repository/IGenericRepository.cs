using PagedList;

namespace GenericCSR.Repository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        //IEnumerable<TEntity> All(Pager pager);

        IPagedList<TEntity> Filter(Pager pager, string filters, OrderByProperties orderByProperties);

        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(int id);
    }
}
