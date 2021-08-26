using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Godel.AutoPartsStore.DAL.Models
{
    public class Part
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public int PartNumber { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public bool IsActive { get; set; }
        public Category Category { get; set; }
    }
}
