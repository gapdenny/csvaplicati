using AutoMapper;
using DAL.Models;
using Services.Dto;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<Csv, ViewCsv>().ForMember("FullName", opt => opt.MapFrom(c => c.FirstName + " " + c.LastName));
            CreateMap<ViewCsv, Csv>().ForMember("FirstName", opt => opt.MapFrom(c => c.FullName.Split(' ', StringSplitOptions.None)[0]))
                .ForMember("LastName", opt => opt.MapFrom(c => c.FullName.Split(' ', StringSplitOptions.None)[1]));
            // CreateMap<ViewCsv, Csv>().ForMember("FullName", opt => opt.MapFrom(c => c.FirstName + " " + c.LastName)).ReverseMap();
            //CreateMap<CsvDto, Csv>().ForMember("FullName", opt => opt.MapFrom(c => c.FirstName + " " + c.LastName));
            //CreateMap<CreateUserViewModel, User>()
            //         .ForMember("Name", opt => opt.MapFrom(c => c.FirstName + " " + c.LastName))
           //CreateMap<ViewCsv, Csv>().ForMember("FirstName", opt => opt.MapFrom(c => c.FullName.Split(' ')[0]));
        }
    }
}
