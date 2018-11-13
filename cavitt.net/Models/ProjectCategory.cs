using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace cavitt.net.Models
{
    public class ProjectCategory
    {
        [Key]
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }
        public string Thumbnail { get; set; }
        public string CategoryDescription { get; set; }

        public List<Project> Projects { get; set; }
    }
}
