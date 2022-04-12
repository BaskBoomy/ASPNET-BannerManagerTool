using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebToolManager.Models;

namespace WebToolManager.Controllers
{
    public class BlackDesertController : Controller
    {
        private readonly BannerManagerDbContext _db;
        public BlackDesertController(BannerManagerDbContext bannerManagerDbContext)
        {
            _db = bannerManagerDbContext;
        }


        public IActionResult Main()
        {
            var bannerList = _db.UploadedBannerLists.Where(x=>x.Page ==1).ToList().OrderBy(x => x.RowNo);
     
            return View(bannerList);
        }
        
        [Route("BlackDesert/GameInfo/Class")]
        public IActionResult Class()
        {
            var bannerList = _db.UploadedBannerLists.Where(x=>x.Page ==2).ToList().OrderBy(x => x.RowNo);
     
            return View(bannerList);
        }
    }
}
