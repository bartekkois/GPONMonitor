using System.Collections.Generic;
using System.Threading.Tasks;
using GPONMonitor.Models;
using GPONMonitor.Models.Onu;

namespace GPONMonitor.Services
{
    public interface ISnmpDataService
    {
        Task<string> GetOltDescriptionAsync(uint oltId);
        Task<string> GetOltUptimeAsync(uint oltId);
        Task<List<OnuShortDescription>> GetOnuListAsync(uint oltId);
        Task<string> GetOnuModelAsync(uint oltId, uint oltPortId, uint onuId);
    }
}
