using System.ComponentModel.DataAnnotations;

namespace crudapp.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [Display(Name="User Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage ="Enter valid email")]
        [Display(Name = "User Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Age is required")]
        [Display(Name = "User Age")]
        [Range(18,60,ErrorMessage ="Age must be in between 18-60")]
        public int Age { get; set; }

        public int DeptId { get; set; }
    }
}