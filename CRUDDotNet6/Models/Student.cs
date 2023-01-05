using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRUDDotNet6.Models
{
    public partial class Student
    {
        [Required]
        [Range(1, Int32.MaxValue)]
        public int Id { get; set; }

        [Required]
        [MaxLength(60)]
        [MinLength(3)]
        public string Name { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [MinLength(3)]
        [MaxLength(45)]
        public string? City { get; set; }
    }
}
