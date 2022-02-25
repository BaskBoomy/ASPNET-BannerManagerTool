using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace WebToolManager.Models
{
    public partial class TemplateInputType
    {
        [Key]
        public int Id { get; set; }
        public int BannerId { get; set; }
        public int TemplateId { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public string Placeholder { get; set; }
    }
}
