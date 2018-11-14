using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace cavitt.net.Models
{
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }
        public string Title{ get; set; }
        public string Content { get; set; }
        public string ThumbnailImage { get; set; }
        public string ThumbnailImageType { get; set; }
        public string GitHubUrl { get; set; }
        public string Keywords { get; set; }
        public bool Active { get; set; }

        public int CategoryId { get; set; }
        public ProjectCategory ProjectCategory { get; set; }

        public List<ProjectImage> Images { get; set; }
    }
}
