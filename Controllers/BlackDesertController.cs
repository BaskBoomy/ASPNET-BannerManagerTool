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
            var bannerList = _db.UploadedBannerLists.ToList().OrderBy(x => x.RowNo);
     
            return View(bannerList);
        }
    }
}
