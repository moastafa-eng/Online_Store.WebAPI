// Ignore Spelling: Dto

using AutoMapper;
using Domain.Contracts;
using Domain.Entities.Baskets;
using Domain.Exceptions.BadRequestEx;
using Domain.Exceptions.WebAPI;
using Services.Abstractions.Baskets;
using Shard.Baskets;

namespace Services.Baskets
{
    public class BasketService(IBasketRepository _repository, IMapper _mapper) : IBasketService
    {

        public async Task<BasketDto?> GetBasketAsync(string Id)
        {
            var basket = await _repository.GetBasketAsync(Id);
            if (basket is null) throw new BasketNotFoundException(Id);

            // mapping from CustomerBasket to basket Dto
            var result = _mapper.Map<BasketDto>(basket);

            return result;
        }

        public async Task<BasketDto?> CreateBasketAsync(BasketDto Dto, TimeSpan duration)
        {
            var basket = _mapper.Map<CustomerBasket>(Dto);

            var result = await _repository.CreateBasketAsync(basket, duration);
            if (result is null) throw new CreateOrUpdateBasketBadRequestEx();

            return _mapper.Map<BasketDto>(result);
        }

        public async Task<bool> DeleteBasket(string Id)
        {
            bool flag = await _repository.DeleteBasketAsync(Id);

            if (!flag) throw new DeleteBasketBadRequestEx();
            return flag;
        }
    }
}
