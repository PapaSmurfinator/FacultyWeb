using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FacultyWeb.Models
{
    public class SubSidebar
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }

       //[ForeignKey("Sidebar")]
        public int SidebarId { get; set; }
        public Sidebar Sidebar { get; set; }

        ////[ForeignKey("Faculty")]
        //public int FacultyId { get; set; }
        //public Faculty Faculty { get; set; }
    }
}
