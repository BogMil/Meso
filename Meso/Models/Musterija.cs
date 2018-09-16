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
        public int IdMusterije { get; set; }
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

    public class MusterijaOrderByPredicateCreator : GenericOrderByPredicateCreator<tbl_musterija, MusterijaPropertyMapper>
    {
        protected override System.Linq.Expressions.Expression<Func<tbl_musterija, dynamic>> GetDefaultOrderByColumn()
        {
            return x => x.IdMusterije;
        }
    }

    public class MusterijaWherePredicateCreator : GenericWherePredicateCreator<tbl_musterija, MusterijaPropertyMapper>
    {

    }

    public class MusterijaPropertyMapper : GenericPropertyMapper<tbl_musterija, MusterijaQueryDto>
    {
        public override System.Linq.Expressions.Expression<Func<tbl_musterija, dynamic>> GetPathInEfForDtoFieldExpression(string dtoFieldName)
        {
            if (dtoFieldName == GetDtoPropertyPathAsString(t => t.IdMusterije))
                return x => x.IdMusterije;
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
            CreateMap<tbl_musterija, MusterijaQueryDto>();
            CreateMap<MusterijaCommandDto, tbl_musterija>();
            CreateMap<PagedList<tbl_musterija>, StaticPagedList<MusterijaQueryDto>>()
                .ConvertUsing<PagedListConverter<tbl_musterija, MusterijaQueryDto>>();
        }
    }
}
