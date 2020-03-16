using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BoardSystem.Models
{
    public class Comment
    {
        [Key]
        public int CommentNum { get; set; }

        public int BoardNum { get; set; }

        [Required]
        public string CommentContents { get; set; }
        
        public string UserId { get; set; }
        
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CommentDate { get; set; }
        
    }
}
