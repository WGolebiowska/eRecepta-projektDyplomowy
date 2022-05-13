using eRecepta_projektDyplomowy.Models;
using eRecepta_projektDyplomowy.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace eRecepta_projektDyplomowy.Controllers.Services.Interfaces
{
    public interface IPrescriptionService
    {
        PrescriptionVm AddOrUpdatePrescription(AddOrUpdatePrescriptionVm addOrUpdatePrescriptionVm);
        PrescriptionVm GetPrescription(Expression<Func<Prescription, bool>> filterExpression);
        IEnumerable<PrescriptionVm> GetPrescriptions(Expression<Func<Prescription, bool>> filterExpression = null);
        bool DeletePrescription(Expression<Func<Prescription, bool>> filterExpression);
    }
}
