using AutoMapper;
using WebApplication4.Entities;

namespace WebApplication4.Models.Profiles
{
    public class CompanyProfile : Profile
    {
        public CompanyProfile()
        {
            //配置文件，从company映射到companyDto

            //如果CompanyDto中有属性名和Company中一致，那么Company中的该属性值就会赋值给CompanyDto的属性。
            //如果CompanyDto中的某个属性在Company中不存在，那么就会忽略（不会赋值）
            //例如这里Dto中CompanyName属性在Company中没有，则需要手动映射
            CreateMap<Company, CompanyDto>()
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Name));
            //dest目标的CompanyName从源头Name中映射


            CreateMap<CompanyAddDto, Company>();
        }
    }
}
