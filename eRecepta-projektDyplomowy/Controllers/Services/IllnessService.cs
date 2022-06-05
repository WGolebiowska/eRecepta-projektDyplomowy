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
    public class IllnessService : BaseService, IIllnessService
    {

        public IllnessService(ApplicationDbContext dbContext, IMapper mapper, ILogger logger) : base(dbContext, mapper, logger)
        {
        }

        public IllnessVm AddOrUpdateIllness(AddOrUpdateIllnessVm addOrUpdateIllnessVm)
        {
            try
            {
                if (addOrUpdateIllnessVm == null)
                    throw new ArgumentNullException("View model parameter is null");
                var illnessEntity = Mapper.Map<Illness>(addOrUpdateIllnessVm);
                if (!addOrUpdateIllnessVm.IllnessId.HasValue || addOrUpdateIllnessVm.IllnessId == 0)
                    DbContext.Illnesses.Add(illnessEntity);
                else
                    DbContext.Illnesses.Update(illnessEntity);
                DbContext.SaveChanges();
                var illnessVm = Mapper.Map<IllnessVm>(illnessEntity);

                return illnessVm;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public IllnessVm GetIllness(Expression<Func<Illness, bool>> filterExpression)
        {
            try
            {
                if (filterExpression == null)
                    throw new ArgumentNullException("Filter expression parameter is null");
                var illnessEntity = DbContext.Illnesses.Include(i => i.Medicines).AsNoTracking().FirstOrDefault(filterExpression);
                var illnessVm = Mapper.Map<IllnessVm>(illnessEntity);

                return illnessVm;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public IEnumerable<IllnessVm> GetIllnesses(Expression<Func<Illness, bool>> filterExpression = null)
        {
            try
            {
                var illnessesQuery = DbContext.Illnesses.AsQueryable().AsNoTracking().Include(i => i.Medicines);
                if (filterExpression != null)
                    illnessesQuery = (Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<Illness, List<Medicine>>)illnessesQuery.Where(filterExpression);
                var illnessesVms = Mapper.Map<IEnumerable<IllnessVm>>(illnessesQuery).OrderBy(i => i.Name);

                return illnessesVms;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }
        public bool DeleteIllness(Expression<Func<Illness, bool>> filterExpression)
        {
            try
            {
                if (filterExpression == null)
                    throw new ArgumentNullException("Filter expression parameter is null");
                var illnessEntity = DbContext.Illnesses.FirstOrDefault(filterExpression);
                DbContext.Remove(illnessEntity);
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public int GetAllIllnessesCount()
        {
            var illnesses = GetIllnesses();
            return illnesses.Count();
        }
    }
}
