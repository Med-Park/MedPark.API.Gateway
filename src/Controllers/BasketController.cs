using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedPark.API.Gateway.Messages.Commands.BasketService;
using MedPark.API.Gateway.Models.BasketService;
using MedPark.API.Gateway.Services;
using MedPark.Common;
using MedPark.Common.Cache;
using MedPark.Common.Consul;
using MedPark.Common.RabbitMq;
using MedPark.Common.RestEase;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace MedPark.API.Gateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBusPublisher _busPublisher;
        private readonly ConsulService _basketConsulService;
        private readonly IConsulHttpClient _consulHttpClient;

        public BasketController(IBusPublisher busPublisher, IOptions<ConsulOptions> consulServices, IConsulHttpClient consulHttpClient)
        {
            _busPublisher = busPublisher;
            _basketConsulService = consulServices.Value.Services.Where(x => x.Name.Contains("basket")).FirstOrDefault();
            _consulHttpClient = consulHttpClient;
        }

        [HttpGet("{customerid}")]
        [Cached(Constants.Day_In_Seconds)]
        public async Task<IActionResult> GetCustomerBasket(Guid customerid)
        {
            var basket = await _consulHttpClient.GetAsync<BasketDto>($"{_basketConsulService.Name}/basket/{customerid}");

            return Ok(basket);
        }

        [HttpPost("createbasket/{customerid}")]
        public async Task<IActionResult> CreateBasket(Guid customerid)
        {
            CreateBasket command = new CreateBasket(customerid, Guid.NewGuid());

            await _busPublisher.SendAsync(command, null);

            return Accepted();
        }

        [HttpPost("{customerid}/addproduct")]
        public async Task<IActionResult> AddProduct([FromBody] AddProductToBasket command)
        {
            await _busPublisher.SendAsync(command.Bind(x => x.BasketItemtId, Guid.NewGuid()), null);

            return Accepted();
        }

        [HttpPost("{customerid}/checkout/")]
        public async Task<IActionResult> CheckoutBasket([FromBody] CheckoutBasket command)
        {
            await _busPublisher.SendAsync(command, null);

            return Accepted();
        }
    }
}