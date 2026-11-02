using System.ComponentModel.DataAnnotations;

namespace Pma_Lab03.Models
{
    public class MenuItem
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
    }
}
