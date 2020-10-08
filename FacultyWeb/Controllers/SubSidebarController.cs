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
    public class SubSidebarController : Controller
    {
        readonly MenuContext db;
        public SubSidebarController(MenuContext context)
        {
            db = context;
        }

        [HttpGet]
        public IActionResult CreateSSidebar(int SidebarId)
        {
            ViewBag.SidebarId = SidebarId;
            return View();
        }

        [HttpPost]
        public IActionResult CreateSSidebar(SubSidebar subsidebar)
        {
            db.SubSidebars.Add(subsidebar);
            db.SaveChanges();
            return RedirectToAction("ListFaculty", "Faculty");
        }

        [HttpGet]
        public IActionResult EditSSidebar(int? Id)
        {
            SubSidebar subsidebar = db.SubSidebars.Find(Id);
            if (subsidebar != null)
            {
                ViewBag.SidebarId = subsidebar.SidebarId;
                return View(subsidebar);

            }
            return RedirectToAction("ListFaculty", "Faculty");
        }

        [HttpPost]
        public IActionResult EditSSidebar(SubSidebar subsidebar)
        {
            db.Entry(subsidebar).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("ListFaculty", "Faculty");
        }

        public IActionResult DeleteSSidebar(int Id)
        {

            SubSidebar subsidebar = db.SubSidebars.Find(Id);
            if (subsidebar != null)
            {
                var subsidebar1 = db.SubSidebars.Where(p => p.Id == Id).Select(p => p.Content).FirstOrDefault(); ;
                ViewBag.Content = subsidebar1;
                return View(subsidebar);
            }
            return View();
        }

        [HttpPost, ActionName("DeleteSSidebar")]
        public IActionResult DeleteConfirmed(int Id)
        {
            SubSidebar subsidebar = db.SubSidebars.Find(Id);
            db.SubSidebars.Remove(subsidebar);
            db.SaveChanges();
            return RedirectToAction("ListFaculty", "Faculty");
        }
    }
}
