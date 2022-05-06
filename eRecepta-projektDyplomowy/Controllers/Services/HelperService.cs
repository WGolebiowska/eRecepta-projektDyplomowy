using eRecepta_projektDyplomowy.Models;
using eRecepta_projektDyplomowy.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq;
using System.Threading.Tasks;

namespace eRecepta_projektDyplomowy.Controllers.Services
{
    public class HelperService : IHelperService
    {
        public HelperService()
        {
        }

        public async Task<PropertyValues> RetrieveEntity(DbUpdateConcurrencyException ex)
        {
            var entry = ex.Entries.Single();
            var clientUser = (ApplicationUser)entry.Entity;
            return await entry.GetDatabaseValuesAsync();
        }
    }
}
