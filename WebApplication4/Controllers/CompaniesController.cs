using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication4.Models;
using WebApplication4.Services;

namespace WebApplication4.Controllers
{
    [ApiController]
    [Route("api/companies")]
    //[Route("api/[controller]")]这样会自动拿controller类名
    public class CompaniesController : ControllerBase  //ControllerBase里面提供了很多处理http请求的方法
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public CompaniesController(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        }
        [HttpGet]
        //public async Task<IActionResult> GetCompanies()
        public async Task<ActionResult<IEnumerable<CompanyDto>>> GetCompanies()
        {
            var companies = await _companyRepository.GetCompaniesAsync();

            //var companyDtos = new List<CompanyDto>();
            //foreach (var companyDto in companies)
            //{
            //如果属性很多这样一个个加很麻烦，所以用AutoMapper插件。。注册后在上面依赖注入
            //companyDtos.Add(new CompanyDto
            //{
            //    Id = companyDto.Id,
            //    Name = companyDto.Name,
            //});
            //}

            var companyDtos = _mapper.Map< IEnumerable < CompanyDto >> (companies);
            return Ok(companyDtos);

            //return Ok(companyDtos); //200
            //return Ok();//如果写成Task<ActionResult<IEnumerable<CompanyDto>>>这样那就可以直接return Ok或者下面那行。这样写在swagger里面也会更清晰
            //return companyDtos;
        }
        [HttpGet("{companyId}")]  //api/companies/xxx
        public async Task<ActionResult<CompanyDto>> GetCompany(Guid companyId)
        {
            //var exist = await _companyRepository.CompanyExistsAsync(companyId); 这种做法在并发的时候不一定对
            //if(!exist)
            //{
            //    return NotFound();
            //}
            var company = await _companyRepository.GetCompanyAsync(companyId);
            if (company == null)
            {
                return NotFound();
            }
            //return Ok(company); //200

            return Ok(_mapper.Map<CompanyDto>(company));
        }
    }
}
