using GPONMonitor.Models.ComplexStateTypes;

namespace GPONMonitor.Services
{
    public interface IResponseDescriptionDictionaries
    {
        ResponseDescription OpticalConnectionStateResponse(int? responseCode);
        ResponseDescription OpticalConnectionDeactivationReasonResponse(int? responseCode);
        ResponseDescription BlockReasonResponse(int? responseCode);
        ResponseDescription BlockStatusResponse(int? responseCode);
        ResponseDescription EthernetPortStateResponse(int? responseCode);
        ResponseDescription EthernetPortSpeedResponse(int? responseCode);
        ResponseDescription VoIPLinestatusResponse(int? responseCode);
    }
}
