using eRecepta_projektDyplomowy.Models;
using eRecepta_projektDyplomowy.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace eRecepta_projektDyplomowy.Controllers.Services.Interfaces
{
    public interface IMedicineService
    {
        MedicineVm AddOrUpdateMedicine(AddOrUpdateMedicineVm addOrUpdateMedicineVm);
        MedicineVm GetMedicine(Expression<Func<Medicine, bool>> filterExpression);
        IEnumerable<MedicineVm> GetMedicines(Expression<Func<Medicine, bool>> filterExpression = null);
        bool DeleteMedicine(Expression<Func<Medicine, bool>> filterExpression);
        int GetAllMedicinesCount();
    }
}