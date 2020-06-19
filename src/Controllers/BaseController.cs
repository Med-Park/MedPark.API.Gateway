using MedPark.Common.Dispatchers;
using MedPark.Common.RabbitMq;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedPark.API.Gateway.Controllers
{
    public class BaseController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;
        public readonly IBusPublisher _busPublisher;

        public BaseController(IBusPublisher busPublisher)
        {
            _busPublisher = busPublisher;
        }

        protected ActionResult<T> OkResult<T>(T data)
        {
            if (data == null)
            {
                return NotFound();
            }

            return Ok(data);
        }
    }
}
