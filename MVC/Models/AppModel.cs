using System.ComponentModel.DataAnnotations.Schema;
using MVC.Validations;

namespace MVC.Models
{
    [Table("apps")]
    public class App
    {
        public int id { get; set; }
        public string name { get; set; }
        public string www { get; set; }
        [IsSemanticVersion()]
        public string version { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
    }
}

