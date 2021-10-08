using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace izo.Models
{
    public class Average
    {
        [Display(Name = "Student Full Name")]
        public string StudentFullName { get; set; }
        [Display(Name = "Course Name")]
        public string CourseName { get; set; }
        public double Score { get; set; }
    }
}
