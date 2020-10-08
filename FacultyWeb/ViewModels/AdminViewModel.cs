using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FacultyWeb.Models;

namespace FacultyWeb.ViewModels
{
    public class AdminViewModel
    {
        public IEnumerable<Faculty> Faculties { get; set; }
        public IEnumerable<Header> Headers { get; set; }
        public IEnumerable<Sidebar> Sidebars { get; set; }
        public IEnumerable<SubHeader> SubHeaders { get; set; }
        public IEnumerable<SubSidebar> SubSidebars { get; set; }
        public IEnumerable<User> Users { get; set; }
    }
}
