using API.Exceptions;
using API.Models;
using API.Models.DTOs.CustomerDto;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using AutoMapper;

namespace API.Services
{
    public class ProductService : IProductSerivce
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository , IMapper mapper)
        {
            _repository = productRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReturnCustomerSearchProductDto>> Get()
        {
            var res = await _repository.GetAsync();
            return _mapper.Map<IEnumerable<ReturnCustomerSearchProductDto>>(res);
        }

        public async Task<ReturnCustomerSearchProductDto> Get(int id)
        {
            var res = await _repository.GetAsync(id);
            return _mapper.Map<ReturnCustomerSearchProductDto>(res);
        }

        public async Task<IEnumerable<ReturnCustomerSearchProductDto>> Search(CustomerProductSearchDto productSearchDto)
        {
            var res = await _repository.GetSearchAsync(productSearchDto);

            if (res.Any())
            {
                return _mapper.Map<IEnumerable<ReturnCustomerSearchProductDto>>(res);
            }
            throw new ProductNotFoundException();
        }
    }
}
