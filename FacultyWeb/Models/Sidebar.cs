using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FacultyWeb.Models
{
    public class Sidebar
    {
        public int Id { get; set; }
        public string Name { get; set; }

       // [ForeignKey("Faculty")]
        public int FacultyId { get; set; }
        public Faculty Faculty { get; set; }

        public ICollection<SubSidebar> SubSidebars { get; set; }
        public Sidebar()
        {
            SubSidebars = new List<SubSidebar>();
        }
    }
}
