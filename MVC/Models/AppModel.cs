using System.ComponentModel.DataAnnotations.Schema;

namespace MVC.Models
{
    [Table("apps")]
    public class App
    {
        public int id { get; set; }
        public string name { get; set; }
        public string www { get; set; }
        public string version { get; set; }
        public DateTime createdAt { get; set; }
    }
}

