using System;
using System.ComponentModel.DataAnnotations;

namespace Bookstore.Models
{
    public class Category
    {
        [Key]
        [Required]
        public int CategoryId { get; set; }

        public string Name { get; set; }
    }
}
