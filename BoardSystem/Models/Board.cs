using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoardSystem.Models
{
    public class Board
    {
        [Key]
        public string BoardNum { get; set; }

        [Required]
        public string BoardTitle { get; set; }

        [Required]
        public string BoardContents { get; set; }

        [Required]
        public string UserId { get; set; }

        public int BoardViews { get; set; }

        public DateTime BoardDate { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
