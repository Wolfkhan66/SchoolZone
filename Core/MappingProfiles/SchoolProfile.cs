using AutoMapper;
using Escc.ServiceClosures;
using SchoolZone.SID.Mvc.Models;
using SchoolZone.SID.Mvc.SidService;
using System.Data;

namespace SchoolZone.SID.Mvc.Core.MappingProfiles
{
    public class SchoolProfile : Profile
    {
        public SchoolProfile()
        {
            CreateMap<SchoolDto, SchoolModel>()
                .ForMember(dest => dest.Contacts, opt => opt.Ignore())
                .ForMember(dest => dest.Report, opt => opt.Ignore())
                .ForMember(dest => dest.Website, opt => opt.Ignore())
                .ForMember(dest => dest.SSRSReports, opt => opt.Ignore())
                .ForMember(dest => dest.ReportServerURL, opt => opt.Ignore())
                .ForMember(dest => dest.Clusters, opt => opt.Ignore());

            CreateMap<SchoolModel, Service>()
                .ForMember(dest => dest.Closures, opt => opt.Ignore())
                .ForMember(dest => dest.LinkedDataUri, opt => opt.Ignore())
                .ForMember(dest => dest.LinkedDataUriSerialisable, opt => opt.Ignore())
                .ForMember(dest => dest.ReasonsForClosure, opt => opt.Ignore())
                .ForMember(dest => dest.Type, opt => opt.Ignore())
                .ForMember(dest => dest.Url, opt => opt.Ignore())
                .ForMember(dest => dest.UrlSerialisable, opt => opt.Ignore());
        }
    }
}