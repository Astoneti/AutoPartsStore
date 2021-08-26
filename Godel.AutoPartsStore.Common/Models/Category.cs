using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Godel.AutoPartsStore.DAL.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Part> Parts { get; set; }
    }
}
