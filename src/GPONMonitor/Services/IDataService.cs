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
        Task<List<OnuShortDescription>> GetOnuListAsync(uint oltId);
        Task<string> GetOnuModelAsync(uint oltId, uint oltPortId, uint onuId);
        Task<object> GetOnuStateAsync(uint oltId, uint oltPortId, uint onuId);
        Task<string> GetOnuStringPropertyAsync(uint oltId, string snmpOid);
        Task<int> GetOnuIntPropertyAsync(uint oltId, string snmpOid);
    }
}
