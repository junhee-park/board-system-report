using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UserDate { get; set; }
    }
}
