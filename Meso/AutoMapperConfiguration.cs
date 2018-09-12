using AutoMapper;
using Meso.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meso
{
    class AutoMapperConfiguration
    {
        public MapperConfiguration Configure()
        {
            var config = new MapperConfiguration
                (
                    cfg =>
                    {
                        cfg.AddProfile<MusterijaMappingProfile>();
                    }
                );

            config.AssertConfigurationIsValid();
            return config;
        }
    }
}
