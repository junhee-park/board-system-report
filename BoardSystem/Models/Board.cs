using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoardSystem.Models
{
    public class Board
    {
        [Key]
        public int BoardNum { get; set; }

        [Required(ErrorMessage = "タイトルを入力してください。")]
        public string BoardTitle { get; set; }

        [Required(ErrorMessage = "内容を入力してください。")]
        public string BoardContents { get; set; }

        
        public string UserId { get; set; }

        public int BoardViews { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime BoardDate { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
