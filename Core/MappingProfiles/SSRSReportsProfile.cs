using AutoMapper;
using Escc.ServiceClosures;
using SchoolZone.SID.Mvc.Models;
using SchoolZone.SID.Mvc.SidService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolZone.SID.Mvc.Core.MappingProfiles
{
    public class SSRSReportsProfile : Profile
    {

        public SSRSReportsProfile()
        {
            CreateMap<SSRSDto, SSRSReportsModel>()
                .ForMember(dest => dest.DisplayName, opt => opt.Ignore());
            CreateMap<SSRSReportsModel, SSRSDto>()
                .ForMember(dest => dest.CategoryID, opt => opt.Ignore())
                .ForMember(dest => dest.DocumentID, opt => opt.Ignore())
                .ForMember(dest => dest.DocumentName, opt => opt.Ignore())
                .ForMember(dest => dest.DocumentTypeID, opt => opt.Ignore())
                .ForMember(dest => dest.ExtensionData, opt => opt.Ignore())
                .ForMember(dest => dest.ExtraReportParameters, opt => opt.Ignore())
                .ForMember(dest => dest.SchoolID, opt => opt.Ignore())
                .ForMember(dest => dest.TopicID, opt => opt.Ignore());
        }
    }
}