using AutoMapper;
using Meso.Models;
using Meso.Repositories;
using Meso.Repositories.Interfaces;
using Meso.Services;
using Meso.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Lifetime;

namespace Meso.App_Start
{
    public class UnityConfig
    {
        public static IMapper MapperInstance { get; set; }

        public static UnityContainer ContainerInstance { get; private set; }
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            container.RegisterInstance(new AutoMapperConfiguration(), new SingletonLifetimeManager());
            var AutoMapperConfiguration = container.Resolve<AutoMapperConfiguration>();
            container.RegisterInstance(AutoMapperConfiguration.Configure().CreateMapper());
            MapperInstance = container.Resolve<IMapper>();

            container.RegisterType<MesoEntities, MesoEntities>();

            container.RegisterType<IMusterijaRepository, MusterijaRepository>();

            container.RegisterType<IMusterijaService, MusterijaService>();

            ContainerInstance = container;
        }
    }
}
