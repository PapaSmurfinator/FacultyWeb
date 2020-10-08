using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FacultyWeb.Models;
using FacultyWeb.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace FacultyWeb.Controllers
{
    [Authorize(Roles = "admin,moderator")]
    public class SidebarController : Controller
    {
        readonly MenuContext db;
        public SidebarController(MenuContext context)
        {
            db = context;
        }

        [HttpGet]
        public IActionResult CreateSidebar(int FacultyId)
        {
            ViewBag.FacultyId = FacultyId;
            return View();
        }

        [HttpPost]
        public IActionResult CreateSidebar(Sidebar sidebar)
        {
            int Id = sidebar.FacultyId;
            db.Sidebars.Add(sidebar);
            db.SaveChanges();
            return RedirectToAction("FacultyComponents", "Faculty", new { Id = Id });
        }

        [HttpGet]
        public IActionResult EditSidebar(int? Id)
        {
            Sidebar sidebar = db.Sidebars.Find(Id);
            if (sidebar != null)
            {
                ViewBag.FacultyId=sidebar.FacultyId;
                return View(sidebar);

            }
            return RedirectToAction("ListFaculty", "Faculty");
        }

        [HttpPost]
        public IActionResult EditSidebar(Sidebar sidebar)
        {
            int Id = sidebar.FacultyId;
            db.Entry(sidebar).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("FacultyComponents", "Faculty", new { Id = Id });
        }

        public IActionResult DeleteSidebar(int Id)
        {
            Sidebar sidebar = db.Sidebars.Find(Id);
            ViewBag.Id = sidebar.FacultyId;
            if (sidebar != null)
            {
                return View(sidebar);
            }
            return View();
        }

        [HttpPost, ActionName("DeleteSidebar")]
        public IActionResult DeleteConfirmed(int Id)
        {
            Sidebar sidebar = db.Sidebars.Include(p => p.SubSidebars).FirstOrDefault(p => p.Id == Id);
            int fId = sidebar.FacultyId;
            db.Sidebars.Remove(sidebar);
            db.SaveChanges();
            return RedirectToAction("FacultyComponents", "Faculty", new { Id = fId });
        }
    }
}
