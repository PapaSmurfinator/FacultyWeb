using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FacultyWeb.Models
{
    public class Faculty
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Image { get; set; }


        public ICollection<Header> Headers { get; set; }
        public ICollection<Sidebar> Sidebars { get; set; }
        //public ICollection<SubSidebar> SubSidebars { get; set; }
        //public ICollection<SubHeader> SubHeaders { get; set; }
        public Faculty()
        {
            Headers = new List<Header>();
            //SubHeaders = new List<SubHeader>();
            Sidebars = new List<Sidebar>();
            //SubSidebars = new List<SubSidebar>();
        }
    }
}
