using AutoMapper;
using eRecepta_projektDyplomowy.Controllers.Services.Interfaces;
using eRecepta_projektDyplomowy.Data;
using eRecepta_projektDyplomowy.Models;
using eRecepta_projektDyplomowy.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace eRecepta_projektDyplomowy.Controllers.Services
{
    public class DoctorService : BaseService, IDoctorService
    {
        public DoctorService(ApplicationDbContext dbContext, IMapper mapper, ILogger logger) : base(dbContext, mapper, logger)
        {
        }

        public DoctorVm GetDoctor(Expression<Func<Doctor, bool>> filterExpression)
        {
            try
            {
                if (filterExpression == null)
                    throw new ArgumentNullException("Filter expression parameter is null");
                var doctorEntity = DbContext.Doctors.FirstOrDefault(filterExpression);
                var doctorVm = Mapper.Map<DoctorVm>(doctorEntity);
                return doctorVm;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public IEnumerable<DoctorVm> GetDoctors(Expression<Func<Doctor, bool>> filterExpression = null)
        {
            try
            {
                var doctorsQuery = DbContext.Doctors.AsQueryable();
                if (filterExpression != null)
                    doctorsQuery = doctorsQuery.Where(filterExpression);
                var doctorsVms = Mapper.Map<IEnumerable<DoctorVm>>(doctorsQuery);
                return doctorsVms;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
