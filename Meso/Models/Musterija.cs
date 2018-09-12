using AutoMapper;
using GenericCSR;
using GenericCSR.Filter;
using GenericCSR.PropertyMapper;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meso.Models
{
    public class Musterija
    {
        public int Id_musterije { get; set; }
        public string Prezime { get; set; }
        public string Ime { get; set; }
        public string Telefon { get; set; }
    }

    public class MusterijaQueryDto : Musterija
    {

    }

    public class MusterijaCommandDto : Musterija
    {

    }

    public class MusterijaOrderByPredicateCreator : GenericOrderByPredicateCreator<Musterije, MusterijaPropertyMapper>
    {
        protected override System.Linq.Expressions.Expression<Func<Musterije, dynamic>> GetDefaultOrderByColumn()
        {
            return x => x.Id_musterije;
        }
    }

    public class MusterijaWherePredicateCreator : GenericWherePredicateCreator<Musterije, MusterijaPropertyMapper>
    {

    }

    public class MusterijaPropertyMapper : GenericPropertyMapper<Musterije, MusterijaQueryDto>
    {
        public override System.Linq.Expressions.Expression<Func<Musterije, dynamic>> GetPathInEfForDtoFieldExpression(string dtoFieldName)
        {
            if (dtoFieldName == GetDtoPropertyPathAsString(t => t.Id_musterije))
                return x => x.Id_musterije;
            if (dtoFieldName == GetDtoPropertyPathAsString(t => t.Ime))
                return x => x.Ime;
            if (dtoFieldName == GetDtoPropertyPathAsString(t => t.Prezime))
                return x => x.Prezime;
            if (dtoFieldName == GetDtoPropertyPathAsString(t => t.Telefon))
                return x => x.Telefon;

            throw new Exception("Nedefinisano polje "+ dtoFieldName);
        }
    }

    public class MusterijaMappingProfile : Profile
    {
        public MusterijaMappingProfile()
        {
            CreateMap<Musterije, MusterijaQueryDto>();
            CreateMap<MusterijaCommandDto, Musterije>();
            CreateMap<PagedList<Musterije>, StaticPagedList<MusterijaQueryDto>>()
                .ConvertUsing<PagedListConverter<Musterije, MusterijaQueryDto>>();
        }
    }
}
