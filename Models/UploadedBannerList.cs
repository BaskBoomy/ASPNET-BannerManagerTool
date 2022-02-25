using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace WebToolManager.Models
{
    public class UploadedBannerList
    {
        [Key]
        public int Id { get; set; }
        public int Page { get; set; }
        public int TemplateId { get; set; }
        public string BannerName { get; set; }
        public string CodeHtml { get; set; }
        public string CodeCss { get; set; }
        public int RowNo { get; set; }
        public int ColNo { get; set; }
    }
}
