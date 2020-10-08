using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FacultyWeb.Models
{
    public class SubHeader
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }

       // [ForeignKey("Header")]
        public int HeaderId { get; set; }
        public Header Header { get; set; }

       //// [ForeignKey("Faculty")]
       // public int FacultyId { get; set; }
       // public Faculty Faculty { get; set; }
    }
}
