using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApplication4.Entities;
using WebApplication4.ValidationAttributes;
namespace WebApplication4.Models
{
    [TestClassValidation(ErrorMessage = "员工编号不能=名!!!!!")]
    public abstract class EmployeeAddOrUpdateDto : IValidatableObject
    {
        [Display(Name = "员工号"), Required(ErrorMessage = "{0}是必填的")]
        public string EmployeeNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //实现IValidatableObject接口在这里可以实现复杂的验证规则(如果前面单个属性验证失败了就不会走这里）
            if (FirstName == LastName)
            {
                yield return new ValidationResult("姓和名不能一样", new[] { nameof(FirstName), nameof(LastName) });
            }
        }
    }
}
