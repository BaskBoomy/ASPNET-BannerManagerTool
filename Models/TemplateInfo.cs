using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace WebToolManager.Models
{
    public partial class TemplateInfo
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public string FrontImage { get; set; }
        public string CodeHtml { get; set; }
        public string CodeJs { get; set; }
        public string CodeCss { get; set; }
    }
}
