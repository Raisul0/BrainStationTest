using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TestApplication.Models
{
    [Table("Post")]
    public class Post
    {
        public int Id { get; set; }
        public DateTime PostDate { get; set; }
        public string Text { get; set; }
        public string Username { get; set; }
    }
}