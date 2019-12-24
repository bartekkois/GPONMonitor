using AutoMapper;
using GPONMonitor.Models.Onu;
using GPONMonitor.Models.OnuFactory;

namespace GPONMonitor.Models
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<IOnuFactory, H640GOnu>();
            CreateMap<IOnuFactory, H640GW02Onu>();
            CreateMap<IOnuFactory, H645BOnu>();
            CreateMap<IOnuFactory, H645GOnu>();
            CreateMap<IOnuFactory, H660GMOnu>();
            CreateMap<IOnuFactory, H660GWOnu>();
            CreateMap<IOnuFactory, H660RMOnu>();
            CreateMap<IOnuFactory, H665GOnu>();
            CreateMap<IOnuFactory, H665Onu>();
            CreateMap<IOnuFactory, H665COnu>();
            CreateMap<IOnuFactory, H680GWOnu>();
            CreateMap<IOnuFactory, UnknownOnu>();
        }
    }
}
