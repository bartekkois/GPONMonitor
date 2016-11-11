using System.Collections.Generic;
using System.Threading.Tasks;
using GPONMonitor.Models;

namespace GPONMonitor.Services
{
    public interface IDataService
    {
        Task<string> GetOltDescriptionAsync(uint oltId);
        Task<string> GetOltUptimeAsync(uint oltId);
        Task<string> GetOltFirmwareVersionAsync(uint oltId);
        Task<List<OnuShortDescription>> GetOnuDescriptionListAsync(uint oltId);
        Task<object> GetOnuStateAsync(uint oltId, uint oltPortId, uint onuId);
        Task<string> GetStringPropertyAsync(uint oltId, string snmpOid);
        Task<int> GetIntPropertyAsync(uint oltId, string snmpOid);
    }
}
