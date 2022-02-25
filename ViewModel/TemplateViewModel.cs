using System.Collections.Generic;
using WebToolManager.Models;

namespace WebToolManager.ViewModel
{
    public class TemplateViewModel
    {

        public IEnumerable<Page> Pages { get; set; }
        public IEnumerable<UploadedBannerList> UploadedBannerLists { get; set; }

    }
}
