using eRecepta_projektDyplomowy.Models;
using eRecepta_projektDyplomowy.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace eRecepta_projektDyplomowy.Controllers.Services.Interfaces
{
    public interface IAppointmentService
    {
            AppointmentVm AddOrUpdateAppointment(AddOrUpdateAppointmentVm addOrUpdateAppointmentVm);
            AppointmentVm GetAppointment(Expression<Func<Appointment, bool>> filterExpression); 
            IEnumerable<AppointmentVm> GetAppointments(Expression<Func<Appointment, bool>> filterExpression = null);
            bool DeleteAppointment(Expression<Func<Appointment, bool>> filterExpression);
    }
}
