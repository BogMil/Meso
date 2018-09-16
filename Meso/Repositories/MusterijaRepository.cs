using GenericCSR.Repository;
using Meso.Models;
using Meso.Repositories.Interfaces;

namespace Meso.Repositories
{
    public class MusterijaRepository :
        GenericRepository<tbl_musterija, MesoEntities, MusterijaOrderByPredicateCreator, MusterijaWherePredicateCreator>,
        IMusterijaRepository
    {
        public MusterijaRepository(MesoEntities context) : base(context)
        {
        }

        protected override object GetPrimaryKey(tbl_musterija entity)
        {
            return entity.IdMusterije;
        }
    }
}
