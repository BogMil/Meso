using System.Collections.Generic;
using ExpressionBuilder.Generics;
using PagedList;

namespace GenericCSR.Service
{
    public interface IGenericService<TQueryDto, TCommandDto>
    {
        void Create(TCommandDto dto);
        void Update(TCommandDto dto);
        void Delete(int id);
        //TQueryDto GetDto(int id);   
        IEnumerable<TQueryDto> GetListOfDto(Pager pager, string filters, OrderByProperties orderByProperties);
        //TViewModel GetJqGridViewModel(Pager pager, string filters, OrderByProperties orderByProperties);
        StaticPagedList<TQueryDto> GetJqGridData(Pager pager, string filters, OrderByProperties orderByProperties);
    }
}