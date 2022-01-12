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
            CreateMap<IOnuFactory, HL4GQVSOnu>();
            CreateMap<IOnuFactory, HL2GRVOnu>();
            CreateMap<IOnuFactory, HL4GQVOnu>();
            CreateMap<IOnuFactory, HL4GMVOnu>();
            CreateMap<IOnuFactory, HL4GMVROnu>();
            CreateMap<IOnuFactory, HL4GXVOnu>();
            CreateMap<IOnuFactory, HL4GMV2Onu>();
            CreateMap<IOnuFactory, HL4GMV3Onu>();
            CreateMap<IOnuFactory, HL4GMV4Onu>();
            CreateMap<IOnuFactory, HL1GROnu>();
            CreateMap<IOnuFactory, HL1GEOnu>();
            CreateMap<IOnuFactory, HL4GOnu>();
            CreateMap<IOnuFactory, HLGSFPOnu>();
            CreateMap<IOnuFactory, LXT010GDOnu>();
            CreateMap<IOnuFactory, LXT011GDOnu>();
            CreateMap<IOnuFactory, UnknownOnu>();
        }
    }
}
