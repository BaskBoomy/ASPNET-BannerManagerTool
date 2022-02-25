using System.Collections.Generic;
using WebToolManager.Models;

namespace WebToolManager.ViewModel
{
    public class TemplateDataViewModel
    {
        public int flag { get; set; }
        public List<TemplateDataInformation> DataLists { get; set; }
        public List<BannerData> BannerDataLists { get; set; }
        public TemplateInfo TemplateInfo { get; set; }
        public string BannerName { get; set; }
        public int PageId { get; set; }
        public string CodeHtml { get; set; }


    }

}
