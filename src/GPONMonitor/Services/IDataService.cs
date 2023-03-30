using GPONMonitor.Models;
using GPONMonitor.Models.OnuFactory;
using GPONMonitor.ViewModels;
using Lextm.SharpSnmpLib;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GPONMonitor.Services
{
    public interface IDataService
    {
        IEnumerable<OltConfigurationViewModel> GetConfiguredOltListAsync();
        Task<string> GetOltDescriptionAsync(uint oltId);
        Task<string> GetOltUptimeAsync(uint oltId);
        Task<string> GetOltFirmwareVersionAsync(uint oltId);
        int GetOltIpHostWebManagementPort(uint oltId);
        Task<IEnumerable<OnuShortDescription>> GetOnuDescriptionListAsync(uint oltId);
        Task<IOnuFactory> GetOnuStateAsync(uint oltId, uint oltPortId, uint onuId);
        Task<string> GetStringPropertyAsync(uint oltId, string snmpOid);
        Task<int?> GetIntPropertyAsync(uint oltId, string snmpOid);
        Task<IList<Variable>> SetStringPropertyAsync(uint oltId, string oid, string data);
        Task<IList<Variable>> SetIntPropertyAsync(uint oltId, string oid, int data);
    }
}
