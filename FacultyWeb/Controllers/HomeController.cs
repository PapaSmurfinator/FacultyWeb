using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FacultyWeb.Models;
using FacultyWeb.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;

namespace FacultyWeb.Controllers
{
    public class HomeController : Controller
    {
        //в Package Manager Console Scaffold-DbContext "Server=(localdb)\mssqllocaldb;Database=helloappdb;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer
        readonly MenuContext db;
        public HomeController(MenuContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            var faculties = db.Faculties.ToList();
            return View(faculties);
        }
        public IActionResult Content(int? fId, string subheaderD, string subsidebarD)
        {
            var FacultyName = db.Faculties.Where(p => p.Id == fId).Select(p => p.Name).FirstOrDefault();
            ViewBag.FacultyName = FacultyName;
            ContentViewModel ivm = new ContentViewModel
            {
                Faculties = db.Faculties.ToList(),
                Headers = db.Headers.ToList(),
                Sidebars = db.Sidebars.ToList(),
                SubHeaders = db.SubHeaders.ToList(),
                SubSidebars = db.SubSidebars.ToList()
            };
            if (fId != null && fId > 0)
            {
                ivm.Faculties = db.Faculties.Where(p => p.Id == fId);
                ivm.Headers = db.Headers.Where(p => p.FacultyId == fId).Include(p => p.SubHeaders);
                ivm.Sidebars = db.Sidebars.Where(p => p.FacultyId == fId).Include(p => p.SubSidebars);
            }
            if (subheaderD != null)
            {
                var subheader = db.SubHeaders.Where(p => p.Header.FacultyId == fId).Where(p => p.Name == subheaderD).Select(p => p.Content).FirstOrDefault();
                ViewBag.Content = subheader;
            }
            else
            {
                var subheader = db.SubHeaders.Where(p=>p.Header.FacultyId==fId).Select(p=>p.Content).FirstOrDefault();
                ViewBag.Content = subheader;
            }
            if (subsidebarD != null)
            {
                var subsidebar = db.SubSidebars.Where(p => p.Sidebar.FacultyId == fId).Where(p => p.Name == subsidebarD).Select(p => p.Content).FirstOrDefault();
                ViewBag.Content = subsidebar;
            }
            return View(ivm);
        }
    }
}
