using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebToolManager.Models;
using WebToolManager.ViewModel;

namespace WebToolManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BannerManagerDbContext _db;
        public HomeController(ILogger<HomeController> logger,
            BannerManagerDbContext bannerManagerContext)
        {
            _logger = logger;
            _db = bannerManagerContext;
        }

        public IActionResult Index()
        {
            var tables = new TemplateViewModel
            {
                Pages = _db.Pages.ToList(),
                UploadedBannerLists = _db.UploadedBannerLists.ToList().OrderBy(x => x.RowNo)
            };

            return View(tables);
        }

        
        public IActionResult Section()
        {
            var templateInfo = _db.TemplateInfos.ToList();
            return View(templateInfo);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public ActionResult ChooseTemplate(int id)
        {
            var templates = new TemplateInfoPageIdVM
            {
                TemplateInfo = _db.TemplateInfos.ToList(),
                PageId = id
            };
            return PartialView("ModalSelectTemplate1",templates);
        }
        public ActionResult TemplateData(int templateId, int pageId)
        {
            var dataList = (from d in _db.TemplateInputTypes
                            join i in _db.TemplateInfos on d.TemplateId equals i.Id
                            select new TemplateDataInformation()
                            {
                                id = d.Id,
                                templateId = d.TemplateId,
                                type = d.Type,
                                name = d.Name,
                                label = d.Label,
                                placeholder = d.Placeholder
                            }).ToList();
            var tables = new TemplateDataViewModel
            {
                flag = 1,
                DataLists = dataList.Where(x => x.templateId == templateId).ToList(),
                TemplateInfo = _db.TemplateInfos.Where(x=>x.Id == templateId).FirstOrDefault(),
                PageId = pageId
            };
           
            return PartialView("ModalSelectTemplate2", tables);
        }
        [HttpPost]
        public async Task<ActionResult<UploadedBannerList>> TemplateData(TemplateDataViewModel model)
        {
            int lastUploadedBannerId;
            try
            {
                if (model.flag == 1)
                {
                    var uploadedBannerCount = _db.UploadedBannerLists.Count() + 1;
                    UploadedBannerList UploadBannerData = new UploadedBannerList
                    {
                        Page = model.PageId,
                        TemplateId = model.TemplateInfo.Id,
                        BannerName = model.BannerName,
                        CodeHtml = model.CodeHtml,
                        RowNo = uploadedBannerCount,
                        ColNo = uploadedBannerCount
                    };
                    _db.UploadedBannerLists.Add(UploadBannerData);
                    await _db.SaveChangesAsync();
                    lastUploadedBannerId = _db.UploadedBannerLists.Max(x=>x.Id);

                
                    foreach (var item in model.DataLists)
                    {
                        var bannerData = new BannerData { BannerId = lastUploadedBannerId, Type = item.type, Data = item.data, Name = item.name, Label = item.label };
                        _db.BannerData.Add(bannerData);
                        _db.SaveChanges();
                    }

                    var modal3Data = new Modal3ViewModel
                    {
                        uploadedBannerList = _db.UploadedBannerLists.Where(x => x.Page == model.PageId).ToList().OrderBy(x => x.RowNo),
                        insertedId = lastUploadedBannerId
                    };

                    return PartialView("ModalSelectTemplate3", modal3Data);
                }
                else
                {
                    var uploadedBanner = _db.UploadedBannerLists.Where(x => x.Id == model.BannerDataLists[0].BannerId).FirstOrDefault();
                    uploadedBanner.BannerName = model.BannerName;
                    uploadedBanner.CodeHtml = model.CodeHtml;
                    foreach (var item in model.BannerDataLists)
                    {
                        var bannerData = _db.BannerData.Find(item.Id);
                        bannerData.Data = item.Data;
                        _db.BannerData.Update(bannerData);
                        _db.SaveChanges();
                    }

                    return Ok();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public IActionResult DeleteUploadedBanner(IFormCollection fc)
        {
            string bannerIds = fc["bannerId"];
            string[] bannerId = bannerIds.Split(new char[] { ',' });
            for(int idx = 0;idx < bannerId.Length; idx++)
            {
                if (idx % 2 == 0)
                {
                    var template = _db.UploadedBannerLists.Find(int.Parse(bannerId[idx]));
                    _db.UploadedBannerLists.Remove(template);
                    _db.SaveChanges();
                }
            }
            //foreach (string id in bannerId)
            //{
            //    var template = _db.UploadedBannerLists.Find(int.Parse(id));
            //    _db.UploadedBannerLists.Remove(template);
            //    _db.SaveChanges();
            //}

            return RedirectToAction("Index");
        }

        public IActionResult EditBanner(int id)
        {
            var banner = _db.UploadedBannerLists.Where(x => x.Id == id).FirstOrDefault();
            var tables = new TemplateDataViewModel
            {
                flag = 2,
                BannerDataLists = _db.BannerData.Where(x => x.BannerId == id).ToList(),
                TemplateInfo = _db.TemplateInfos.Where(x => x.Id == banner.TemplateId).FirstOrDefault(),
                PageId = banner.Page,
                BannerName = banner.BannerName,
                CodeHtml = banner.CodeHtml
            };
            return PartialView("ModalSelectTemplate2", tables);
        }
        public IActionResult SwitchBanner(int id)
        {
            var modal3Data = new Modal3ViewModel
            {
                uploadedBannerList = _db.UploadedBannerLists.Where(x => x.Page == id).ToList().OrderBy(x => x.RowNo),
                insertedId = 0
            };
            return PartialView("ModalSelectTemplate3", modal3Data);
        }
        [HttpPost]
        public IActionResult UpdateItem(string itemIds)
        {
            int count = 1;
            List<int> itemIdList = new List<int>();
            itemIdList = itemIds.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
            foreach (var itemId in itemIdList)
            {
                try
                {
                    UploadedBannerList item = _db.UploadedBannerLists.Where(x => x.Id == itemId).FirstOrDefault();
                    item.RowNo = count;
                    _db.UploadedBannerLists.Update(item);
                    _db.SaveChanges();
                }
                catch (Exception)
                {
                    continue;
                }
                count++;
            }
            return Json(true, new Newtonsoft.Json.JsonSerializerSettings());
        }

        public IActionResult EditTemplate(int id)
        {
            var template = _db.TemplateInfos.Where(x=>x.Id == id).FirstOrDefault();
            return View(template);
        }

        [HttpPost]
        public IActionResult UpdateTemplate(TemplateInfo tpData)
        {
            try
            {
                var seletedTemplateItem = _db.TemplateInfos.Where(x => x.Id == tpData.Id).FirstOrDefault();
                seletedTemplateItem.CodeHtml = tpData.CodeHtml;
                seletedTemplateItem.CodeCss = tpData.CodeCss;
                seletedTemplateItem.CodeJs = tpData.CodeJs;
                _db.TemplateInfos.Update(seletedTemplateItem);
                _db.SaveChanges();
            }
            catch (Exception)
            {
                
            }
            return RedirectToAction("Section");
        }
    }
}
