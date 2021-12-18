using AutoMapper;
using System;
using WebApplication4.Entities;

namespace WebApplication4.Models.Profiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                //.ForMember(dest => dest.Gender,opt=>opt.MapFrom(src=>))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => DateTime.Now.Year - src.DateOfBirth.Year))
                ;
        }
    }
}
