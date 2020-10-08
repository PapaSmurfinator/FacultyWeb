using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FacultyWeb.Models;
using FacultyWeb.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FacultyWeb.Controllers
{
    [Authorize(Roles = "admin,moderator")]
    public class SubHeaderController : Controller
    {
        readonly MenuContext db;
        public SubHeaderController(MenuContext context)
        {
            db = context;
        }

        [HttpGet]
        public IActionResult CreateSHeader(int HeaderId)
        {
            ViewBag.HeaderId = HeaderId;
            return View();
        }

        [HttpPost]
        public IActionResult CreateSHeader(SubHeader subheader)
        {
            db.SubHeaders.Add(subheader);
            db.SaveChanges();
            return RedirectToAction("ListFaculty", "Faculty");
        }

        [HttpGet]
        public IActionResult EditSHeader(int? Id)
        {
            SubHeader subheader = db.SubHeaders.Find(Id);
            if (subheader != null)
            {
                ViewBag.HeaderId = subheader.HeaderId;
                return View(subheader);

            }
            return RedirectToAction("ListFaculty", "Faculty");
        }

        [HttpPost]
        public IActionResult EditSHeader(SubHeader subheader)
        {
            db.Entry(subheader).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("ListFaculty", "Faculty");
        }

        public IActionResult DeleteSHeader(int Id)
        {

            SubHeader subheader = db.SubHeaders.Find(Id);
            if (subheader != null)
            {
                var subheader1 = db.SubHeaders.Where(p => p.Id == Id).Select(p => p.Content).FirstOrDefault(); ;
                ViewBag.Content = subheader1;
                return View(subheader);
            }
            return View();
        }

        [HttpPost, ActionName("DeleteSHeader")]
        public IActionResult DeleteConfirmed(int Id)
        {
            SubHeader subheader = db.SubHeaders.Find(Id);
            db.SubHeaders.Remove(subheader);
            db.SaveChanges();
            return RedirectToAction("ListFaculty", "Faculty");
        }
    }
}
