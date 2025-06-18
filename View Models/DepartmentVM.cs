using System.ComponentModel.DataAnnotations;

namespace Web_mvd.View_Models
{
    public class DepartmentVM
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "name is Reuired")]
        public string Name { get; set; }
        [Required(ErrorMessage = "code is Reuired")]
        public string code { get; set; }
        [Display(Name = "date of creation")]
        public DateTime CreationDate { get; set; }

        public string? Description { get; set; }

    }
}
