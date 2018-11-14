using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cavitt.net.Dtos
{
    public class ProjectImageDto
    {
        public int ImageId { get; set; }
        public int ProjectId { get; set; }
        public string Base64Image { get; set; }
    }
}
