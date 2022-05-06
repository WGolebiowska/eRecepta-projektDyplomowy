using AutoMapper;
using eRecepta_projektDyplomowy.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace eRecepta_projektDyplomowy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        protected readonly ApplicationDbContext DbContext;
        protected readonly ILogger Logger;
        protected readonly IMapper Mapper;
        public BaseController(IMapper mapper, ILogger logger)
        {
            Logger = logger;
            Mapper = mapper;
        }
    }
}
