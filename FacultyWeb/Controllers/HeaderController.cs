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
    public class HeaderController : Controller
    {
        readonly MenuContext db;
        public HeaderController(MenuContext context)
        {
            db = context;
        }

        [HttpGet]
        public IActionResult CreateHeader(int FacultyId)
        {
            ViewBag.FacultyId = FacultyId;
            return View();
        }

        [HttpPost]
        public IActionResult CreateHeader(Header header)
        {
            int Id = header.FacultyId;
            db.Headers.Add(header);
            db.SaveChanges();
            return RedirectToAction("FacultyComponents", "Faculty", new {Id=Id });
        }

        [HttpGet]
        public IActionResult EditHeader(int? Id)
        {
            Header header = db.Headers.Find(Id);
            if (header != null)
            {
                ViewBag.FacultyId = header.FacultyId;
                return View(header);
            }
            return RedirectToAction("ListFaculty", "Faculty");
        }

        [HttpPost]
        public IActionResult EditHeader(Header header)
        {
            int Id = header.FacultyId;
            db.Entry(header).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("FacultyComponents", "Faculty", new { Id = Id });
        }

        public IActionResult DeleteHeader(int Id)
        {
            Header header = db.Headers.Find(Id);
            ViewBag.Id = header.FacultyId;
            if (header != null)
            {
                return View(header);
            }
            return View();
        }

        [HttpPost, ActionName("DeleteHeader")]
        public IActionResult DeleteConfirmed(int Id)
        {
            Header header = db.Headers.Include(p => p.SubHeaders).FirstOrDefault(p => p.Id == Id);
            int fId = header.FacultyId;
            db.Headers.Remove(header);
            db.SaveChanges();
            return RedirectToAction("FacultyComponents", "Faculty", new { Id = fId });
        }
    }
}
