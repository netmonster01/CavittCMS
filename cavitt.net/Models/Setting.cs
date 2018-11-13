using System.ComponentModel.DataAnnotations;

namespace cavitt.net.Models
{
    public class Setting
    {
        [Key]
        public int SettingId { get; set; }
        
        public string Key  { get; set; }
       
        public string Value { get; set; }
    }
}
