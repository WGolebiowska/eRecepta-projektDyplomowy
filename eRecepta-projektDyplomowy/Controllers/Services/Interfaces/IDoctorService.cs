using eRecepta_projektDyplomowy.Models;
using eRecepta_projektDyplomowy.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace eRecepta_projektDyplomowy.Controllers.Services.Interfaces
{
    public interface IDoctorService
    {
        DoctorVm GetDoctor(Expression<Func<Doctor, bool>> filterExpression);
        IEnumerable<DoctorVm> GetDoctors(Expression<Func<Doctor, bool>> filterExpression = null);
    }
}
