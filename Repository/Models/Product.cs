using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace ShgardiProductAPI.Repository.Models
{
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}

