using System.ComponentModel.DataAnnotations.Schema;

namespace MyAspireApp.WebApi.Models
{
    public class Product
    {
        public int Id { get; set; } // Primary Key
        public string Name { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
    }
}
