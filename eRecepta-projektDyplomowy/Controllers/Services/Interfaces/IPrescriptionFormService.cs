using eRecepta_projektDyplomowy.Models;
using eRecepta_projektDyplomowy.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace eRecepta_projektDyplomowy.Controllers.Services.Interfaces
{
    public interface IPrescriptionFormService
    {
        PrescriptionFormVm AddOrUpdatePrescriptionForm(AddOrUpdatePrescriptionFormVm addOrUpdatePrescriptionFormVm);
        PrescriptionFormVm GetPrescriptionForm(Expression<Func<PrescriptionForm, bool>> filterExpression);
        IEnumerable<PrescriptionFormVm> GetPrescriptionForms(Expression<Func<PrescriptionForm, bool>> filterExpression = null);
        bool DeletePrescriptionForm(Expression<Func<PrescriptionForm, bool>> filterExpression);
    }
}
