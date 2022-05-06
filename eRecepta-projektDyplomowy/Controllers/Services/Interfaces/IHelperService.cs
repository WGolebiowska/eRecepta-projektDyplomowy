using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading.Tasks;

namespace eRecepta_projektDyplomowy.Services.Interfaces
{
    public interface IHelperService
    {
        Task<PropertyValues> RetrieveEntity(DbUpdateConcurrencyException ex);
    }
}
