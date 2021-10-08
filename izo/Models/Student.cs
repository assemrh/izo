using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace izo.Models
{
    public class Student
    {
        [Key]
        public Guid Id { get; set; }
        [Display(Name = "Full Name")]
        public string FullName { get; set; }
        public int Number { get; set; }
        public string Email { get; set; }
        [Display(Name = "Gsm Number")]
        public string GsmNumber { get; set; }
        public ICollection<ExamResult> ExamResults { get; set; } = new List<ExamResult>();
    }
}
