using eRecepta_projektDyplomowy.Models;
using eRecepta_projektDyplomowy.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace eRecepta_projektDyplomowy.Controllers.Services.Interfaces
{
    public interface IPatientService
    {
        PatientVm GetPatient(Expression<Func<Patient, bool>> filterExpression);
        IEnumerable<PatientVm> GetPatients(Expression<Func<Patient, bool>> filterExpression = null);
    }
}