using API.Repositories.Interfaces;
using API.Utility;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTest.Repositories;

namespace UnitTest.Services
{
    internal class ServicesTestBase : RepositoryTestBase
    {
        
        public IMapper _mapper;

        [SetUp]
        public async Task Setup()
        {
            // Calling the base setup method
            await base.Setup();
            // Creating a mapper configuration
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            }).CreateMapper();

        }

        [TearDown]
        public async Task TearDown()
        {
            await base.TearDown();
        }

    }

}
