using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace crudapp.Models
{
    public class Department
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Department Name is required")]
        [Display(Name = "Department Name")]
        public string DeptName { get; set; }
        [Required(ErrorMessage = "Department Code is required")]
        [Display(Name = "Department Code")]
        public string DeptCode { get; set; }
        
    }
}