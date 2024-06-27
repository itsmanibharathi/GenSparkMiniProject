using API.Exceptions;
using API.Models;
using API.Models.DTOs.RestaurantDto;
using API.Repositories;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using AutoMapper;
using System.Security.Cryptography.X509Certificates;

namespace API.Services
{
    public class RestaurantProductService : IRestaurantProductService
    {
        private readonly IRestaurantProductRepository _restaurantProductRepository;
        private readonly IMapper _mapper;

        public RestaurantProductService(IRestaurantProductRepository restaurantProductRepository , IMapper mapper)
        {
            _restaurantProductRepository = restaurantProductRepository;
            _mapper = mapper;
        }
        public async Task<ReturnRestaurantProductDto> Add(RestaurantProductDto restaurantProductDto)
        {
            Product product = _mapper.Map<Product>(restaurantProductDto);
            product = await _restaurantProductRepository.AddAsync(product);
            return _mapper.Map<ReturnRestaurantProductDto>(product);
        }
        public async Task<ReturnRestaurantProductDto> Get(int restaurantId, int productId)
        {
            var res = await _restaurantProductRepository.GetAsync(productId);
            if (res.RestaurantId == restaurantId)
            {
                return _mapper.Map<ReturnRestaurantProductDto>(res);
            }
            throw new ProductNotFoundException(productId);
        }

        public async Task<ReturnRestaurantProductDto> Available(int restaurantId, int productId)
        {
            try
            {
                var product = await _restaurantProductRepository.GetAsync(productId);
                if (product.RestaurantId != restaurantId)
                {
                    throw new ProductNotFoundException(productId);
                }
                if (product.ProductAvailable)
                {
                    throw new AlreadyUptoDateException($"The Product {productId} is Already Available ");
                }
                product.ProductAvailable = true;
                var res = await _restaurantProductRepository.UpdateAsync(product);
                return _mapper.Map<ReturnRestaurantProductDto>(res);
            }
            catch (EntityNotFoundException<Product>)
            {
                throw;
            }
            catch (ProductNotFoundException)
            {
                throw;
            }
            catch (AlreadyUptoDateException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new UnableToDoActionException("Unable to make product Available");
            }
        }

        public async Task<bool> Delete(int restaurantId, int productId)
        {
            var res = await Get(restaurantId, productId);
            return await _restaurantProductRepository.DeleteAsync(productId);
            
        }


        public async Task<IEnumerable<ReturnRestaurantProductDto>> GetAll(int restaurantId)
        {
            var res = await _restaurantProductRepository.GetByRestaurantIdAsync(restaurantId);
            return res.Where(x => x.RestaurantId == restaurantId).Select(x => _mapper.Map<ReturnRestaurantProductDto>(x));
        }

        public async Task<ReturnRestaurantProductDto> UnAvailable(int restaurantId, int productId)
        {
            try
            {
                var product = await _restaurantProductRepository.GetAsync(productId);
                if (product.RestaurantId != restaurantId)
                {
                    throw new ProductNotFoundException(productId);
                }
                if (!product.ProductAvailable)
                {
                    throw new AlreadyUptoDateException($"The Product {productId} is Already UnAvailable ");
                }
                product.ProductAvailable = false;
                var res = await _restaurantProductRepository.UpdateAsync(product);
                return _mapper.Map<ReturnRestaurantProductDto>(res);
            }
            catch (EntityNotFoundException<Product>)
            {
                throw;
            }
            catch (ProductNotFoundException)
            {
                throw;
            }
            catch (AlreadyUptoDateException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new UnableToDoActionException("Unable to make product Available");
            }
        }

        public async Task<ReturnRestaurantProductDto> Update(RestaurantProductDto restaurantProductDto)
        {
            try
            {
                var data = _mapper.Map<Product>(restaurantProductDto);
                var res = await _restaurantProductRepository.UpdateAsync(data);
                return _mapper.Map<ReturnRestaurantProductDto>(res);
            }
            catch (EntityNotFoundException<Product>)
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
