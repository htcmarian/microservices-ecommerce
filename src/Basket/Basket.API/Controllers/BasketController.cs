using System;
using System.Net;
using System.Threading.Tasks;
using Basket.API.Entities;
using Basket.API.GrpcServices;
using Basket.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _repository;
        private readonly DiscountGrpcService _discountGrpcService;

        public BasketController(IBasketRepository repository, DiscountGrpcService discountGrpcService)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _discountGrpcService = discountGrpcService ?? throw new ArgumentNullException(nameof(discountGrpcService));
        }

        [HttpGet("{username}", Name = "GetBasket")]
        [ProducesResponseType(typeof(BasketCart), (int) HttpStatusCode.OK)]
        public async Task<ActionResult<BasketCart>> GetBasket(string username)
        {
            var basket = await _repository.GetBasket(username);
            return Ok(basket ?? new BasketCart(username));
        }

        [HttpPut]
        [ProducesResponseType(typeof(BasketCart), (int) HttpStatusCode.OK)]
        public async Task<ActionResult<BasketCart>> UpdateBasket([FromBody] BasketCart basket)
        {
            // TODO : Communicate with Discount.Grpc and Calculate latest prices for products into the shopping cart
            foreach (var item in basket.Items)
            {
                var coupon = await _discountGrpcService.GetDiscount(item.ProductName);
                item.Price -= coupon.Amount;
            }

            return Ok(await _repository.UpdateBasket(basket));
        }

        [HttpDelete("{username}", Name = "DeleteBasket")]
        [ProducesResponseType(typeof(void), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteBasket(string username)
        {
            await _repository.DeleteBasket(username);

            return Ok();
        }
        
        
    }
}