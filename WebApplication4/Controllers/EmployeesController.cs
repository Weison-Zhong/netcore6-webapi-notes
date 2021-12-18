﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployeesForCompany(Guid companyId, [FromQuery(Name ="gender")] string genderA,string q)
        {
            if (!await _companyRepository.CompanyExistsAsync(companyId))
            {
                return NotFound();
            }
            var employees = await _companyRepository.GetEmployeesAsync(companyId, genderA,q);
            var employeeDtos = _mapper.Map<IEnumerable<EmployeeDto>>(employees);
            return Ok(employeeDtos);
        }
        [HttpGet("{employeeId}")]
        public async Task<ActionResult<EmployeeDto>> GetEmployeeForCompany(Guid companyId, Guid employeeId)
        {
            if(!await _companyRepository.CompanyExistsAsync(companyId))
            {
                return NotFound();
            }
            var employee = await _companyRepository.GetEmployeeAsync(companyId, employeeId);
            if(employee == null)
            {
                return NotFound();
            }
            var employeeDto =_mapper.Map<EmployeeDto>(employee);
            return Ok(employeeDto);
        }
        //[HttpPost]
        //public async Task<ActionResult<EmployeeDto>> CreateEmployeeForCompany()
    }
}