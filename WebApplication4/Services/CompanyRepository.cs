using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication4.Data;
using WebApplication4.DtoParameters;
using WebApplication4.Entities;

namespace WebApplication4.Services
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly RoutineDbContext _context;
        //要操作数据库，构造函数注入dbcontext
        public CompanyRepository(RoutineDbContext context)
        {
            //判断如果为null则抛出异常
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public void AddCompany(Company company)
        {
            if (company == null)
            {
                throw new ArgumentNullException(nameof(company));
            }
            company.Id = Guid.NewGuid();
            if (company.Employees != null)
            {
                foreach (var employee in company.Employees)
                {
                    employee.Id = Guid.NewGuid();
                }
            }

            _context.Companies.Add(company);
        }

        public void AddEmployee(Guid companyId, Employee employee)
        {
            if (companyId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(companyId));
            }
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee));
            }
            employee.CompanyId = companyId;
            _context.Employees.Add(employee);
        }

        public async Task<bool> CompanyExistsAsync(Guid companyId)
        {
            if (companyId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(companyId));
            }
            return await _context.Companies.AnyAsync(x => x.Id == companyId);
        }

        public void DeleteCompany(Company company)
        {
            if (company == null)
            {
                throw new ArgumentNullException(nameof(company));
            }
            _context.Companies.Remove(company);
        }

        public void DeleteEmployee(Employee employee)
        {
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee));
            }
            _context.Employees.Remove(employee);
        }

        public async Task<IEnumerable<Company>> GetCompaniesAsync(CompanyDtoParameters parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }
         
            var queryExpression = _context.Companies as IQueryable<Company>;
            if (!string.IsNullOrWhiteSpace(parameters.CompanyName))
            {
                parameters.CompanyName = parameters.CompanyName.Trim();
                queryExpression = queryExpression.Where(x => x.Name == parameters.CompanyName);
            }
            if (!string.IsNullOrWhiteSpace(parameters.SearchTerm))
            {
                parameters.SearchTerm = parameters.SearchTerm.Trim();
                queryExpression = queryExpression.Where(x => x.Name.Contains(parameters.SearchTerm) || x.Introduction.Contains(parameters.SearchTerm));

            }

            //fen页
            queryExpression = queryExpression.Skip(parameters.PageSize * (parameters.PageNumber - 1))// 跳过
                .Take(parameters.PageSize);//跳过后取多少

        

            return await queryExpression.ToListAsync(); //这一行才是真正查询数据库
        }

        public async Task<IEnumerable<Company>> GetCompaninesAsync(IEnumerable<Guid> companyIds)
        {
            if (companyIds == null)
            {
                throw new ArgumentNullException(nameof(companyIds));
            }
            return await _context.Companies
                    .Where(x => companyIds.Contains(x.Id))
                    .OrderBy(x => x.Name)
                    .ToListAsync();
        }

        public async Task<Company> GetCompanyAsync(Guid companyId)
        {
            if (companyId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(companyId));
            }
            return await _context.Companies.FirstOrDefaultAsync(x => x.Id == companyId);
        }

        public async Task<Employee> GetEmployeeAsync(Guid companyId, Guid employeeId)
        {
            if (companyId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(companyId));
            }
            if (employeeId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(employeeId));
            }
            return await _context.Employees
                 .Where(x => x.CompanyId == companyId && x.Id == employeeId)
                 .FirstOrDefaultAsync();
        }
                                                                    //Guid companyId, string genderDisplay, string q
        public async Task<IEnumerable<Employee>> GetEmployeesAsync(Guid companyId,EmployeeDtoParameters parameters)
        {
            if (companyId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(companyId));
            }
            //if (string.IsNullOrWhiteSpace(parameters.Gender) && string.IsNullOrWhiteSpace(parameters.Q))
            //{
            //    return await _context.Employees
            //    .Where(x => x.CompanyId == companyId)
            //    .OrderBy(x => x.EmployeeNo)
            //    .ToListAsync();
            //}
            var items = _context.Employees.Where(x => x.CompanyId == companyId);//到这还没执行查询，下面还在继续拼接
            if (!string.IsNullOrWhiteSpace(parameters.Gender))
            {
                parameters.Gender = parameters.Gender.Trim();
                var gender = Enum.Parse<Gender>(parameters.Gender);
                items = items.Where(x => x.Gender == gender);
            }
            if (!string.IsNullOrWhiteSpace(parameters.Q))
            {
                parameters.Q = parameters.Q.Trim();
                //模糊搜索
                items = items.Where(x => x.EmployeeNo.Contains(parameters.Q) || x.FirstName.Contains(parameters.Q) || x.LastName.Contains(parameters.Q));
            }

            //paixu
            if (!string.IsNullOrWhiteSpace(parameters.OrderBy))
            {
                if(parameters.OrderBy.ToLowerInvariant() == "name")
                {
                    items = items.OrderBy(x=>x.FirstName).ThenBy(x=>x.LastName);
                }
            }


            return await items.ToListAsync();
        }

        public async Task<bool> SaveAsync()
        {
            //判断影响记录数是否大于0
            return await _context.SaveChangesAsync() >= 0;
        }

        public void UpdateCompany(Company company)
        {

        }

        public void UpdateEmployee(Employee employee)
        {
            //就算什么都没，但是还要写的，这样context才能跟踪
        }
    }
}
