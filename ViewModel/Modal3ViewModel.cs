using System.Collections.Generic;
using WebToolManager.Models;

namespace WebToolManager.ViewModel
{
    public class Modal3ViewModel
    {
        public IEnumerable<UploadedBannerList> uploadedBannerList { get; set; }
        public int insertedId { get; set; }
    }
}
