using API.Exceptions;
using API.Models;
using API.Models.DTOs;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using AutoMapper;

namespace API.Services
{
    public class ProductService : IProductSerivce
    {
        private readonly IProductSearchRepository _productSearch;
        private readonly IRepository<int, Product> _repository;
        private readonly IMapper _mapper;

        public ProductService(IProductSearchRepository productSearch, IRepository<int,Product> repository , IMapper mapper)
        {
            _productSearch = productSearch;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReturnSearchProductDto>> Get()
        {
            var res= await _repository.Get();
            return _mapper.Map<IEnumerable<ReturnSearchProductDto>>(res);
        }

        public async Task<ReturnSearchProductDto> Get(int id)
        {
            var res = await _repository.Get(id);
            return _mapper.Map<ReturnSearchProductDto>(res);
        }

        public async Task<IEnumerable<ReturnSearchProductDto>> Search(ProductSearchDto productSearchDto)
        {
            var res = await _productSearch.Search(productSearchDto);
            if (res.Any())
            {
                return _mapper.Map<IEnumerable<ReturnSearchProductDto>>(res);
            }
            throw new ProductNotFoundException();
        }
    }
}
