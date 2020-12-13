using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebLab.DAL.Entities;
using WebLab.Models;
using WebLab.Extensions;
using WebLab.DAL.Data;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace WebLab.Controllers
{
    public class ProductController : Controller
    {
        ApplicationDbContext _context;
        int _pageSize;

        private ILogger _logger;
        //public ProductController(ApplicationDbContext context, ILogger<ProductController> logger)//---------------------
        //{
        //    _pageSize = 3;
        //    _context = context;
        //    _logger = logger;
        //}
        public ProductController(ApplicationDbContext context)
        {
            _pageSize = 3;
            _context = context;            
        }
       
        [Route("Catalog")]
        [Route("Catalog/Page_{pageNo}")]
        public IActionResult Index(int? group, int pageNo=1)
        {
            var groupName = group.HasValue ? _context.FoodGroups.Find(group.Value)?.GroupName : "all groups";
            var foodsFiltered = _context.Foods.Where(d => !group.HasValue || d.FoodGroupId == group.Value);

           
        
            
            // Поместить список групп во ViewData
            ViewData["Groups"] = _context.FoodGroups;
            ViewData["CurrentGroup"] = group ?? 0; // Получить id текущей группы и поместить в TempData

            var model = ListViewModel<Food>.GetModel(foodsFiltered, pageNo,_pageSize);
            if (Request.IsAjaxRequest())
                return PartialView("_listpartial", model);
            else
                return View(model);
        }

        
    }
}
        
