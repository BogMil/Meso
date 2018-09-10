using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using GenericCSR.Filter;
using PagedList;

namespace GenericCSR.Repository
{
    public abstract class GenericRepository<TEntity, TContext, TOrderByPredicateCreator, TFilterPredicateCreator> : 
        IGenericRepository<TEntity> 
        where TEntity : class 
        where TContext: DbContext
        where TOrderByPredicateCreator : IOrderByPredicateCreator<TEntity>, new()
        where TFilterPredicateCreator : IWherePredicateCreator<TEntity>, new()
    {

        protected TContext Db { get; private set; }

        protected GenericRepository(TContext context)
        {
            Db = context;
        }

        public virtual IPagedList<TEntity> Filter(Pager pager,string filters,OrderByProperties orderByProperties) 
        {

            var orderBy = new TOrderByPredicateCreator().GetPropertyObject(orderByProperties);
            var filterPredicate = new TFilterPredicateCreator().GetWherePredicate(filters);


            IQueryable<TEntity> listOfEntities = Db.Set<TEntity>();


            var listOfFilteredEntities = filterPredicate == null ? listOfEntities : listOfEntities.Where(filterPredicate);
            listOfFilteredEntities = AddCustomCondition(listOfFilteredEntities);

            var listOfOrderedEntities = orderByProperties.OrderDirection == SortDirections.Ascending
                ? listOfFilteredEntities.OrderBy(orderBy.OrderByProperty)
                : listOfFilteredEntities.OrderByDescending(orderBy.OrderByProperty);

            var pagedList = Paged(listOfOrderedEntities, pager);
            return pagedList;
        }

        protected virtual IQueryable<TEntity> AddCustomCondition(IQueryable<TEntity> listOfFilteredEntities)
        {
            return listOfFilteredEntities;
        }

        protected IPagedList<TEntity> Paged(IEnumerable<TEntity> listOfEntities, Pager pager)
        {
            var paged = listOfEntities.ToPagedList(pager.CurrentPageNumber, pager.NumberOfRowsToDisplay);
            return paged;
        }

        public virtual void Create(TEntity entity)
        {
            Db.Set<TEntity>().Add(entity);
            Db.SaveChanges();
        }

        public virtual void Update(TEntity entity)
        {
            var id = GetPrimaryKey(entity);
            var oldEntity = Db.Set<TEntity>().Find(id);
            Db.Entry(oldEntity).CurrentValues.SetValues(entity);
            Db.SaveChanges();
        }

        public virtual void Delete(int id)
        {
            var entity = Db.Set<TEntity>().Find(id);
            Db.Set<TEntity>().Remove(entity ?? throw new Exception("Trazeni zapis za brisajne ne postoji u bazi"));
            Db.SaveChanges();
        }

        protected virtual object GetPrimaryKey(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}