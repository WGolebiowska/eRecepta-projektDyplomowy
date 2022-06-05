using eRecepta_projektDyplomowy.Models;
using eRecepta_projektDyplomowy.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace eRecepta_projektDyplomowy.Controllers.Services.Interfaces
{
    public interface IIllnessService
    {
        IllnessVm AddOrUpdateIllness(AddOrUpdateIllnessVm addOrUpdateIllnessVm);
        IllnessVm GetIllness(Expression<Func<Illness, bool>> filterExpression);
        IEnumerable<IllnessVm> GetIllnesses(Expression<Func<Illness, bool>> filterExpression = null);
        bool DeleteIllness(Expression<Func<Illness, bool>> filterExpression);
        int GetAllIllnessesCount();
    }
}
