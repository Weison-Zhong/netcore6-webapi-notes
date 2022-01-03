using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication4.DtoParameters;
using WebApplication4.Entities;
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
        public async Task<ActionResult<IEnumerable<CompanyDto>>> GetCompanies([FromQuery]CompanyDtoParameters parameters)
        {
            //throw new Exception("an error"); 
            var companies = await _companyRepository.GetCompaniesAsync(parameters);

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
        [HttpGet("{companyId}" ,Name =nameof(GetCompany))]  //api/companies/xxx
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
        [HttpPost]                                                //这里参数是类 类型，APIController会自动从Body中拿，当然也可手动写明                      
        public async Task<ActionResult<CompanyDto>> CreateCompany([FromBody]CompanyAddDto company)
        {
            Console.WriteLine("here!!!!!!");
            var entity = _mapper.Map<Company>(company);
            _companyRepository.AddCompany(entity);
            await _companyRepository.SaveAsync(); //这里如果出现异常，框架会自动返回400状态码
            var returnDto = _mapper.Map<CompanyDto>(entity);
            //CreatedAtRoute这个会在响应header上自动带上一个url，这个url可以找到上面的新创建的资源。
            //上面的GetCompany方法就可以找到这个资源，命名为Name =nameof(GetCompany))。第二个参数是一个对象，就是前面第一个参数那个方法的参数，第三个参数是响应的body
            //但是实际上好像没啥用，还是直接返回就可以了。    
            return CreatedAtRoute(nameof(GetCompany),new {companyId=returnDto.Id},returnDto);
        }
        [HttpDelete("{companyId}")]
        public async Task<IActionResult> DeleteCompany(Guid companyId)
        {
            var companyEntity = await _companyRepository.GetCompanyAsync(companyId);
            if(companyEntity == null)
            {
                return NotFound();
            }
            _companyRepository.DeleteCompany(companyEntity);
            await _companyRepository.SaveAsync();
            return Ok(); 
        }
    }
}
