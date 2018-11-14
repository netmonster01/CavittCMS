using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace cavitt.net.Models
{
    public class ProjectImage
    {
        [Key]
        public int ImageId { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public string Base64Image { get; set; }
    }
}
