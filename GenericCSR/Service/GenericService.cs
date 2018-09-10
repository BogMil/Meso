using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GenericCSR.Repository;
using PagedList;

namespace GenericCSR.Service
{
    public abstract class GenericService<TQueryDto,TCommandDto,TRepository, TEntity>
        : IGenericService<TQueryDto, TCommandDto>
        where TQueryDto : class
        where TCommandDto : class
        where TRepository : IGenericRepository<TEntity>
        where TEntity : class
    {
        protected TRepository Repository { get; private set; }
        protected IMapper Mapper { get; private set; }

        protected GenericService(TRepository repository, IMapper mapper)
        {
            Repository = repository;
            Mapper = mapper;
        }

        public IEnumerable<TQueryDto> GetListOfDto(Pager pager, string filters, OrderByProperties orderByProperties)
        {
            var entities = GetListOfEntites(pager, filters, orderByProperties);
            var listOfDto = Mapper.Map<List<TEntity>, List<TQueryDto>>(entities.ToList());
            return listOfDto;
        }

        public StaticPagedList<TQueryDto> GetJqGridData(Pager pager, string filters, OrderByProperties orderByProperties)
        {
            var entities = GetListOfEntites(pager, filters, orderByProperties);

            return Mapper.Map<IPagedList<TEntity>, StaticPagedList<TQueryDto>>((PagedList<TEntity>)entities);
        }

        protected virtual IPagedList<TEntity> GetListOfEntites(Pager pager, string filters, OrderByProperties orderByProperties)
        {
            return Repository.Filter(pager, filters, orderByProperties);
        }

        public virtual void Create(TCommandDto dto)
        {
            var entity = Mapper.Map<TCommandDto, TEntity>(dto);
            Repository.Create(entity);
        }

        public virtual void Update(TCommandDto dto)
        {
            var entity = Mapper.Map<TCommandDto, TEntity>(dto);
            Repository.Update(entity);
        }

        public virtual void Delete(int id)
        {
            Repository.Delete(id);
        }

    }
}