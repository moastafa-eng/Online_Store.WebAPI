using AutoMapper;
using Domain.Contracts;
using Domain.Entities.Orders;
using Domain.Entities.Products;
using Domain.Exceptions.WebAPI;
using Microsoft.Extensions.Configuration;
using Services.Abstractions.Payments;
using Shard.DTOs.Baskets;
using Stripe;
using Product = Domain.Entities.Products.Product;

namespace Services.Payments
{
    public class PaymentService(IBasketRepository _basketRepository, IUnitOfWork _unitOfWork, IConfiguration _config, IMapper _mapper) : IPaymentService
    {
        public async Task<BasketDto> CreatePaymentIntentAsync(string basketId)
        {
            // Calculate the amount => Sub Total + Delivery Method Cost
            // Get basket by id
            var basket = await _basketRepository.GetBasketAsync(basketId);

            // If basket is null => throw exception from type BasketNotFoundException
            if (basket is null) throw new BasketNotFoundException(basketId);

            // check Product and it's price
            foreach(var item in basket.Items)
            {
                // Get product by Id
                var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(item.Id);

                // If product not found throw exception from type product not found Exception
                if (product is null) throw new ProductNotFoundException(item.Id);

                item.Price = product.Price;
            }

            // calculate the sub total
            var subTotal = basket.Items.Sum(B => B.Quantity * B.Price);

            // if DeliveryMethodId is not found throw exception from type DeliveryMethodNotfounedException
            if (!basket.DeliveryMethodId.HasValue) throw new DeliveryMethodNotFoundEx(-1);

            // Get DeliveryMethodById
            var deliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(basket.DeliveryMethodId.Value);

            if (deliveryMethod is null) throw new DeliveryMethodNotFoundEx(basket.DeliveryMethodId.Value);

            basket.ShippingCost = deliveryMethod.Price;

            // Calculate the Total Amount
            var totalAmount = subTotal + deliveryMethod.Price;


            // Send the amount to strip
            StripeConfiguration.ApiKey = _config["StipeOptions:SecretKey"];

            var paymentIntentService = new PaymentIntentService();
            PaymentIntent paymentIntent;

            if(basket.PaymentIntentId is null)
            {
                // Create PaymentIntent
                var options = new PaymentIntentCreateOptions() 
                {
                    Amount = (long)totalAmount * 100, // *100 => to convert from Dolar to sent
                    Currency = "usd",
                    PaymentMethodTypes = new List<string>() { "card" },
                };

                paymentIntent = await paymentIntentService.CreateAsync(options);

            }
            else
            {
                // Update PaymentIntent
                var options = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)totalAmount * 100
                };

                paymentIntent = await paymentIntentService.UpdateAsync(basket.PaymentIntentId,options);
            }

            basket.PaymentIntentId = paymentIntent.Id;
            basket.ClientSecret = paymentIntent.ClientSecret;

            var finalbasket = await _basketRepository.CreateBasketAsync(basket, TimeSpan.FromDays(1));

            return _mapper.Map<BasketDto>(finalbasket);
        }
    }
}
