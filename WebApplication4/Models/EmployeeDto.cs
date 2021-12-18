using System;

namespace WebApplication4.Models
{
    public class EmployeeDto
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public string EmployeeNo { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
    }
}
