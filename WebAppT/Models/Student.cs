using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppT.Models
{
    public class Student
    {
        public int Id { get; set; }
        [StringLength(30)]
        [Required]
        public string Firstname { get; set; }
        [StringLength(30)]
        [Required]
        public string Lastname { get; set; }
        public double GPA { get; set; }
        public int? MajorId { get; set; }
        public virtual Major Major { get; set; }
        public Student() { }
    }
}
