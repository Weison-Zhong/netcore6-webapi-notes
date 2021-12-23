using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models
{
    public class CompanyAddDto
    {
        [Display(Name = "公司名")] //不加这个下面的{0}被替换成属性名name,加了这个后会替换为 公司名
        [Required(ErrorMessage = "{0}这个是必填的")] //{0}就代表这个属性名 
        [MaxLength(100, ErrorMessage = "{0}的最大长度是{1}")]  //{0}就代表这个属性名 {1}就代表这个attribute从前往后第一个参数
        public string Name { get; set; }
        [Display(Name = "简介")]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "{0}的长度允许范围是从{2}到{1}")]
        public string Introduction { get; set; }
        public ICollection<EmployeeAddDto> Employees { get; set; } = new List<EmployeeAddDto>();
    }
}
