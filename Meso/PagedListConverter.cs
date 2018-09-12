using AutoMapper;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace Meso
{
    public class PagedListConverter<TSource, TDestination> : ITypeConverter<PagedList<TSource>, StaticPagedList<TDestination>>
    {
        private IMapper _mapper { get; set; }

        public PagedListConverter()
        {
            //var container = new UnityContainer();
            //container.RegisterInstance(new AutoMapperConfiguration());
            //var AutoMapperConfiguration = container.Resolve<AutoMapperConfiguration>();
            _mapper = new AutoMapperConfiguration().Configure().CreateMapper();
            //_mapper = container.Resolve<IMapper>();
        }

        public StaticPagedList<TDestination> Convert(PagedList<TSource> source, StaticPagedList<TDestination> destination, ResolutionContext context)
        {
            var collection = _mapper.Map<IEnumerable<TSource>, IEnumerable<TDestination>>(source);
            return new StaticPagedList<TDestination>(collection, source.PageNumber, source.PageSize, source.TotalItemCount);
        }
    }
}
