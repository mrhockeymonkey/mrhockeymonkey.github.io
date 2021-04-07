using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace GenericHost.ASP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SomethingController : ControllerBase
    {
        private readonly ILogger<SomethingController> _logger;
        private readonly SomethingOptions _options;

        public SomethingController(
            ILogger<SomethingController> logger,
            IOptions<SomethingOptions> options)
        {
            _logger = logger;
            _options = options.Value;
        }

        [HttpGet]
        public SomethingOptions Get()
        {
            return _options;
        }
    }
}
