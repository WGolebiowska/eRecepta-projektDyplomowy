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

namespace eRecepta_projektDyplomowy.Controllers.Services
{
    public class MedicineService : BaseService, IMedicineService
    {

        public MedicineService(ApplicationDbContext dbContext, IMapper mapper, ILogger logger) : base(dbContext, mapper, logger)
        {
        }

        public MedicineVm AddOrUpdateMedicine(AddOrUpdateMedicineVm addOrUpdateMedicineVm)
        {
            try
            {
                if (addOrUpdateMedicineVm == null)
                    throw new ArgumentNullException("View model parameter is null");
                var medicineEntity = Mapper.Map<Medicine>(addOrUpdateMedicineVm);
                if (!addOrUpdateMedicineVm.MedicineId.HasValue || addOrUpdateMedicineVm.MedicineId == 0)
                    DbContext.Medicines.Add(medicineEntity);
                else
                    DbContext.Medicines.Update(medicineEntity);
                DbContext.SaveChanges();
                var medicineVm = Mapper.Map<MedicineVm>(medicineEntity);

                return medicineVm;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public MedicineVm GetMedicine(Expression<Func<Medicine, bool>> filterExpression)
        {
            try
            {
                if (filterExpression == null)
                    throw new ArgumentNullException("Filter expression parameter is null");
                var medicineEntity = DbContext.Medicines.AsNoTracking().FirstOrDefault(filterExpression);
                var medicineVm = Mapper.Map<MedicineVm>(medicineEntity);

                return medicineVm;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public IEnumerable<MedicineVm> GetMedicines(Expression<Func<Medicine, bool>> filterExpression = null)
        {
            try
            {
                var medicinesQuery = DbContext.Medicines.AsQueryable().AsNoTracking();
                if (filterExpression != null)
                    medicinesQuery = medicinesQuery.Where(filterExpression);
                var medicinesVms = Mapper.Map<IEnumerable<MedicineVm>>(medicinesQuery).OrderBy(m => m.Name);

                return medicinesVms;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }
        public bool DeleteMedicine(Expression<Func<Medicine, bool>> filterExpression)
        {
            try
            {
                if (filterExpression == null)
                    throw new ArgumentNullException("Filter expression parameter is null");
                var medicineEntity = DbContext.Medicines.FirstOrDefault(filterExpression);
                DbContext.Remove(medicineEntity);
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }
        public int GetAllMedicinesCount()
        {
            var medicines = GetMedicines();
            return medicines.Count();
        }
    }
}