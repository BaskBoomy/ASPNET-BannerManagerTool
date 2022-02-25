using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace WebToolManager.Models
{
    public partial class Page
    {
        [Key]
        public int Id { get; set; }
        public string Root { get; set; }
        public string Url { get; set; }
    }
}
