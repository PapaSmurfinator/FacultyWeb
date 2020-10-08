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
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace FacultyWeb.Controllers
{
    [Authorize(Roles ="admin,moderator")]
    public class FacultyController : Controller
    {
        readonly MenuContext db;
        public FacultyController(MenuContext context)
        {
            db = context;
        }

        public IActionResult ListFaculty()
        {
            var faculties = db.Faculties.ToList();
            return View(faculties);
        }

        [HttpGet]
        public IActionResult CreateFaculty()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateFaculty(FacultyViewModel fvm)
        {
            Faculty faculty = new Faculty { Id = fvm.Id, Name = fvm.Name };
            if (fvm.Image != null)
            {
                byte[] imageData = null;
                using (var binaryReader = new BinaryReader(fvm.Image.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)fvm.Image.Length);
                }
                faculty.Image = imageData;
            }
            db.Faculties.Add(faculty);
            db.SaveChanges();
            return RedirectToAction("ListFaculty", "Faculty");
        }

        [HttpGet]
        public IActionResult EditFaculty(int? Id)
        {

            if (Id != null)
            {
                Faculty faculty = db.Faculties.FirstOrDefault(p => p.Id == Id);
                if (faculty != null)
                {
                    return View(faculty);
                }
            }
            return View();
        }

        [HttpPost]
        public IActionResult EditFaculty(FacultyViewModel fvm)
        {
            Faculty faculty = new Faculty { Id = fvm.Id, Name = fvm.Name };
            if (fvm.Image != null)
            {
                byte[] imageData = null;
                using (var binaryReader = new BinaryReader(fvm.Image.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)fvm.Image.Length);
                }
                faculty.Image = imageData;
            }
            db.Entry(faculty).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("ListFaculty", "Faculty");
        }

        public IActionResult DeleteFaculty(int Id)
        {

            Faculty faculty = db.Faculties.Find(Id);
            if (faculty != null)
            {
                return View(faculty);
            }
            return View();
        }

        [HttpPost, ActionName("DeleteFaculty")]
        public IActionResult DeleteConfirmed(int Id)
        {
            Faculty faculty = db.Faculties.Include(p=>p.Headers).Include(p => p.Sidebars).FirstOrDefault(p=>p.Id==Id);
            db.Faculties.Remove(faculty);
            db.SaveChanges();
            return RedirectToAction("ListFaculty", "Faculty");
        }


        public IActionResult FacultyComponents(int Id)
        {
            AdminViewModel avm = new AdminViewModel()
            {
                Faculties = db.Faculties.ToList(),
                Headers = db.Headers.ToList(),
                Sidebars = db.Sidebars.ToList(),
                SubHeaders = db.SubHeaders.ToList(),
                SubSidebars = db.SubSidebars.ToList()
            };
            ViewBag.FacultyId = Id;
            avm.Faculties = db.Faculties.Where(p => p.Id == Id);
            avm.Headers = db.Headers.Where(p => p.FacultyId == Id).Include(p => p.SubHeaders);
            avm.Sidebars = db.Sidebars.Where(p => p.FacultyId == Id).Include(p => p.SubSidebars);
            return View(avm);
        }
    }
}