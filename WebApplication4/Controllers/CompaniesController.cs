using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebApplication4.Services;

namespace WebApplication4.Controllers
{
    [ApiController]
    [Route("api/companies")]
    //[Route("api/[controller]")]这样会自动拿controller类名
    public class CompaniesController:ControllerBase  //ControllerBase里面提供了很多处理http请求的方法
    {
        private readonly ICompanyRepository _companyRepository;

        public CompaniesController(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
        }
        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {
            var companies = await _companyRepository.GetCompaniesAsync();
            return Ok(companies); //200
        }
        [HttpGet("{companyId}")]  //api/companies/xxx
        public async Task<IActionResult> GetCompany(Guid companyId)
        {
            //var exist = await _companyRepository.CompanyExistsAsync(companyId); 这种做法在并发的时候不一定对
            //if(!exist)
            //{
            //    return NotFound();
            //}
            var company = await _companyRepository.GetCompanyAsync(companyId);
            if(company == null)
            {
                return NotFound();
            }
            return Ok(company); //200
        }
    }
}
