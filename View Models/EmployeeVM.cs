using Data_Access_Layer.Models.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace Web_mvd.View_Models
{
    public class EmployeeVM
    {
        public int Id { get; set; }

        public DateTime createdon { get; set; }

        [Required]
        [Display(Name = "Full Name")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Address { get; set; }

        public DateTime last_modifiedon { get; set; }

        [Range(18, 65)]
        public int Age { get; set; } 

        [Range(0, double.MaxValue)]
        public decimal Salary { get; set; }
        public int last_modified_by { get; set; }

        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Employee Type")]
        public EmployeeType EmployeeType { get; set; }

        [Required]
        public Gender Gender { get; set; }
        public int created_by { get; set; }

        [Display(Name = "Description")]
        public string? Description { get; set; }

        [Display(Name = "Department")]  
          public int? DepartmentId { get; set; }  

        public DateTime HiringDate { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; } = true;  
    }
}
