using API.Exceptions;
using API.Models;
using API.Models.DTOs;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using AutoMapper;
using System.Security.Cryptography.X509Certificates;

namespace API.Services
{
    public class RestaurantProductService : IRestaurantProductService
    {
        private readonly IRepository<int, Product> _repository;
        private readonly IMapper _mapper;

        public RestaurantProductService(IRepository<int, Product> repository , IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<ReturnRestaurantProductDto> Add(RestaurantProductDto restaurantProductDto)
        {
            Product product = _mapper.Map<Product>(restaurantProductDto);
            product = await _repository.Add(product);
            return _mapper.Map<ReturnRestaurantProductDto>(product);
        }
        public async Task<ReturnRestaurantProductDto> Get(int restaurantId, int productId)
        {
            var res = await _repository.Get(productId);
            if(res.RestaurantId == restaurantId)
            {
                return _mapper.Map<ReturnRestaurantProductDto>(res);
            }
            throw new ProductNotFoundException(productId);
        }

        public async Task<ReturnRestaurantProductDto> Available(int restaurantId, int productId)
        {
            try
            {
                var res = await Get(restaurantId, productId);
                if (res.ProductAvailable)
                {
                    throw new AlreadyUptoDateException(productId);
                }
                res.ProductAvailable = true;
                return await Update(_mapper.Map<RestaurantProductDto>(res));
            }
            catch (AlreadyUptoDateException)
            {
                throw;
            }
            catch (ProductNotFoundException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new UnableToDoActionException("Unable to make product available");
            }
        }

        public async Task<bool> Delete(int restaurantId, int productId)
        {
            var res = await _repository.Get(productId);
            return await _repository.Delete(productId);
        }


        public async Task<IEnumerable<ReturnRestaurantProductDto>> GetAll(int restaurantId)
        {
            var res = await _repository.Get();
            return res.Where(x => x.RestaurantId == restaurantId).Select(x => _mapper.Map<ReturnRestaurantProductDto>(x));
        }

        public async Task<ReturnRestaurantProductDto> UnAvailable(int restaurantId, int productId)
        {
            try
            {
                var res = await Get(restaurantId, productId);
                if (!res.ProductAvailable)
                {
                    throw new AlreadyUptoDateException($"The Product {productId} is Already in UnAvailable ");
                }
                res.ProductAvailable = false;
                return await Update(_mapper.Map<RestaurantProductDto>(res));
            }
            catch (AlreadyUptoDateException)
            {
                throw;
            }
            catch (ProductNotFoundException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new UnableToDoActionException("Unable to make product Unavailable");
            }
        }

        public async Task<ReturnRestaurantProductDto> Update(RestaurantProductDto restaurantProductDto)
        {
            try
            {
                var product = await Get(restaurantProductDto.RestaurantId,restaurantProductDto.ProductId);
                product.ProductName = restaurantProductDto.ProductName;
                product.ProductDescription = restaurantProductDto.ProductDescription;
                product.ProductPrice = restaurantProductDto.ProductPrice;
                product.ProductCategories = restaurantProductDto.ProductCategories;
                product.ProductAvailable = restaurantProductDto.ProductAvailable;
                product.UpdateAt = DateTime.Now;
                var res = await _repository.Update(_mapper.Map<Product>(product));
                return _mapper.Map<ReturnRestaurantProductDto>(res);
            }
            catch (ProductNotFoundException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new UnableToDoActionException("Unable to update the product");
            }
        }
    }
}
