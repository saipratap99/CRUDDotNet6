using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRUDDotNet6.Models
{
    public partial class User
    {
        public int Id { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
        [Required]
        [MaxLength(45)]
        public string Name { get; set; } = null!;
    }
}
