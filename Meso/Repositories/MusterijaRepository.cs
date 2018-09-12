using GenericCSR.Repository;
using Meso.Models;
using Meso.Repositories.Interfaces;

namespace Meso.Repositories
{
    public class MusterijaRepository :
        GenericRepository<Musterije, MesoEntities, MusterijaOrderByPredicateCreator, MusterijaWherePredicateCreator>,
        IMusterijaRepository
    {
        public MusterijaRepository(MesoEntities context) : base(context)
        {
        }

        protected override object GetPrimaryKey(Musterije entity)
        {
            return entity.Id_musterije;
        }
    }
}
