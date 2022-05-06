using AutoMapper;
using eRecepta_projektDyplomowy.Data;
using Microsoft.Extensions.Logging;

namespace eRecepta_projektDyplomowy.Controllers.Services
{
    public abstract class BaseService
    {
        protected readonly ApplicationDbContext DbContext;
        protected readonly ILogger Logger;
        protected readonly IMapper Mapper;
        public BaseService(ApplicationDbContext dbContext, IMapper mapper, ILogger logger)
        {
            DbContext = dbContext;
            Logger = logger;
            Mapper = mapper;
        }
    }
}