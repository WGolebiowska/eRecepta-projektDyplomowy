using AutoMapper;
using eRecepta_projektDyplomowy.Controllers.Services.Interfaces;
using eRecepta_projektDyplomowy.Data;
using eRecepta_projektDyplomowy.Models;
using eRecepta_projektDyplomowy.ViewModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace eRecepta_projektDyplomowy.Controllers.Services
{
    public class AppointmentService : BaseService, IAppointmentService
    {
        public AppointmentService(ApplicationDbContext dbContext, IMapper mapper, ILogger logger) : base(dbContext, mapper, logger)
        {
        }

        public AppointmentVm AddOrUpdateAppointment(AddOrUpdateAppointmentVm addOrUpdateAppointmentVm)
        {
            try
            {
                if (addOrUpdateAppointmentVm == null)
                    throw new ArgumentNullException("View model parameter is null");
                var appointmentEntity = Mapper.Map<Appointment>(addOrUpdateAppointmentVm);
                if (!addOrUpdateAppointmentVm.AppointmentId.HasValue || addOrUpdateAppointmentVm.AppointmentId == 0)
                    DbContext.Appointments.Add(appointmentEntity);
                else
                    DbContext.Appointments.Update(appointmentEntity);
                DbContext.SaveChanges();
                var appointmentVm = Mapper.Map<AppointmentVm>(appointmentEntity);
                return appointmentVm;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public AppointmentVm GetAppointment(Expression<Func<Appointment, bool>> filterExpression)
        {
            try
            {
                if (filterExpression == null)
                    throw new ArgumentNullException("Filter expression parameter is null");
                var appointmentEntity = DbContext.Appointments.FirstOrDefault(filterExpression);
                var appointmentVm = Mapper.Map<AppointmentVm>(appointmentEntity);
                return appointmentVm;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public IEnumerable<AppointmentVm> GetAppointments(Expression<Func<Appointment, bool>> filterExpression = null)
        {
            try
            {
                var appointmentsQuery = DbContext.Appointments.AsQueryable();
                if (filterExpression != null)
                    appointmentsQuery = appointmentsQuery.Where(filterExpression);
                var appointmentsVms = Mapper.Map<IEnumerable<AppointmentVm>>(appointmentsQuery);
                return appointmentsVms;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }
        public bool DeleteAppointment(Expression<Func<Appointment, bool>> filterExpression)
        {
            try
            {
                if (filterExpression == null)
                    throw new ArgumentNullException("Filter expression parameter is null");
                var appointmentEntity = DbContext.Appointments.FirstOrDefault(filterExpression);
                DbContext.Remove(appointmentEntity);
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
