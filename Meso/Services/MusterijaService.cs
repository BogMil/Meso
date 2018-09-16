using AutoMapper;
using GenericCSR.Service;
using Meso.Models;
using Meso.Repositories.Interfaces;
using Meso.Services.Interfaces;

namespace Meso.Services
{
    public class MusterijaService :
        GenericService<MusterijaQueryDto, MusterijaCommandDto, IMusterijaRepository, tbl_musterija>,
        IMusterijaService
    {
        public MusterijaService(IMusterijaRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
