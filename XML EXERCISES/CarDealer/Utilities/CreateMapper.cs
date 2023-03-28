using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealer.Utilities
{
    public static class CreateMapper
    {
        public static IMapper InitialMapper()
        {
            MapperConfiguration config = new MapperConfiguration(config =>
            {
                config.AddProfile<CarDealerProfile>();
            });

            IMapper mapper = new Mapper(config);

            return mapper;
        }

    }
}
