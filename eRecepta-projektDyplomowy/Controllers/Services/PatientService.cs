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
    public class PatientService : BaseService, IPatientService
    {
        public PatientService(ApplicationDbContext dbContext, IMapper mapper, ILogger logger) : base(dbContext, mapper, logger)
        {
        }

        public PatientVm GetPatient(Expression<Func<Patient, bool>> filterExpression)
        {
            try
            {
                if (filterExpression == null)
                    throw new ArgumentNullException("Filter expression parameter is null");
                var patientEntity = DbContext.Patients.AsNoTracking().FirstOrDefault(filterExpression);
                var patientVm = Mapper.Map<PatientVm>(patientEntity);
                return patientVm;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public IEnumerable<PatientVm> GetPatients(Expression<Func<Patient, bool>> filterExpression = null)
        {
            try
            {
                var patientsQuery = DbContext.Patients.AsQueryable();
                if (filterExpression != null)
                    patientsQuery = patientsQuery.AsNoTracking().Where(filterExpression);
                var patientsVms = Mapper.Map<IEnumerable<PatientVm>>(patientsQuery);
                return patientsVms;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
