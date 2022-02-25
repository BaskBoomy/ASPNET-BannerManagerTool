using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebToolManager.Models;

namespace WebToolManager.Controllers
{
    public class PreviewController : Controller
    {

        private readonly BannerManagerDbContext _db;
        public PreviewController(BannerManagerDbContext bannerManagerContext)
        {
            _db = bannerManagerContext;
        }
        public IActionResult Index(int id)
        {
            var htmlCode = _db.TemplateInfos.Where(x => x.Id == id).FirstOrDefault();
            return View(htmlCode);
        }
    }
}
