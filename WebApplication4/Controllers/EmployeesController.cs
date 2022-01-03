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
    [Route("api/companies/{companyId}/employees")]
    public class EmployeesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICompanyRepository _companyRepository;

        public EmployeesController(IMapper mapper, ICompanyRepository companyRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
        }
        [HttpGet]                                               //GetEmployeesForCompany(Guid companyId, [FromQuery(Name = "gender")] string genderA, string q)
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployeesForCompany(Guid companyId, [FromQuery]EmployeeDtoParameters parameters)
        {
            if (!await _companyRepository.CompanyExistsAsync(companyId))
            {
                return NotFound();
            }
            var employees = await _companyRepository.GetEmployeesAsync(companyId, parameters);
            var employeeDtos = _mapper.Map<IEnumerable<EmployeeDto>>(employees);
            return Ok(employeeDtos);
        }
        [HttpGet("{employeeId}")]
        public async Task<ActionResult<EmployeeDto>> GetEmployeeForCompany(Guid companyId, Guid employeeId)
        {
            if (!await _companyRepository.CompanyExistsAsync(companyId))
            {
                return NotFound();
            }
            var employee = await _companyRepository.GetEmployeeAsync(companyId, employeeId);
            if (employee == null)
            {
                return NotFound();
            }
            var employeeDto = _mapper.Map<EmployeeDto>(employee);
            return Ok(employeeDto);
        }

        [HttpPost]  //创建子资源
        public async Task<ActionResult<EmployeeDto>> CreateEmployeeForCompany(Guid companyId, EmployeeAddDto employee)
        {
            if (!await _companyRepository.CompanyExistsAsync(companyId))
            {
                return NotFound();
            }
            var entity = _mapper.Map<Employee>(employee);
            _companyRepository.AddEmployee(companyId, entity);
            await _companyRepository.SaveAsync();

            var dtoToReturn = _mapper.Map<EmployeeDto>(entity);
            return Ok(dtoToReturn);
        }

        [HttpPut("{employeeId}")]  //IActionResult返回值没有具体的类型
        public async Task<IActionResult> UpdateEmployeeForCompany(Guid companyId, Guid employeeId, EmployeeUpdateDto employee)
        {
            if (!await _companyRepository.CompanyExistsAsync(companyId))
            {
                return NotFound();
            }
            var employeeEntity = await _companyRepository.GetEmployeeAsync(companyId, employeeId);
            if (employeeEntity == null)
            {
                return NotFound();
            }
            //entity转化为updateDto
            //把传进来的employee的值更新到updateDto
            //把updateDto映射回entity
            _mapper.Map(employee, employeeEntity);
            _companyRepository.UpdateEmployee(employeeEntity);
            await _companyRepository.SaveAsync();
            return NoContent(); //204状态码
        }
        [HttpDelete("{employeeId}")]
        public async Task<IActionResult> DeleteEmployeeForCompany(Guid companyId,Guid employeeId)
        {
            if (!await _companyRepository.CompanyExistsAsync(companyId))
            {
                return NotFound();
            }
            var employeeEntity = await _companyRepository.GetEmployeeAsync(companyId, employeeId);
            if (employeeEntity == null)
            {
                return NotFound();
            }
            _companyRepository.DeleteEmployee(employeeEntity);
            await _companyRepository.SaveAsync();
            return NoContent();
        }
    }
}
