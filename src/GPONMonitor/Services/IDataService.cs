using System.Collections.Generic;
using System.Threading.Tasks;
using GPONMonitor.Models;
using GPONMonitor.ViewModels;

namespace GPONMonitor.Services
{
    public interface IDataService
    {
        IEnumerable<OltConfigurationViewModel> GetConfiguredOltListAsync();
        Task<string> GetOltDescriptionAsync(uint oltId);
        Task<string> GetOltUptimeAsync(uint oltId);
        Task<string> GetOltFirmwareVersionAsync(uint oltId);
        Task<IEnumerable<OnuShortDescription>> GetOnuDescriptionListAsync(uint oltId);
        Task<object> GetOnuStateAsync(uint oltId, uint oltPortId, uint onuId);
        Task<string> GetStringPropertyAsync(uint oltId, string snmpOid);
        Task<int> GetIntPropertyAsync(uint oltId, string snmpOid);
    }
}
