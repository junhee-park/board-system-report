using System;
using System.ComponentModel.DataAnnotations;

namespace BoardSystem.Models
{
    public class User
    {
        [Key]   
        public string UserId { get; set; }

        [Required]
        public string UserPassword { get; set; }

        [Required]
        public string UserName { get; set; }

        public DateTime UserDate { get; set; }
    }
}
