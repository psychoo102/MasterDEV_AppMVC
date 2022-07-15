using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MVC.Validations;

namespace MVC.Models
{
    [Table("apps")]
    public class App
    {
        public int id { get; set; }
        [Required]
        [MinLength(3)]
        public string name { get; set; }
        [Url()]
        public string www { get; set; }
        [IsSemanticVersion()]
        public string version { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
    }
}

