using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TestApplication.Models
{
    [Table("Comment")]
    public class Comment
    {
        [Required]
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public int PostId { get; set; }
        public string Text { get; set; }
        public DateTime CommentDate { get; set; }
        public string Username { get; set; }
        public int UpVote { get; set; }
        public int DownVote { get; set; }
    
    }
}