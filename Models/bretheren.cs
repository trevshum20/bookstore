using System;
using System.ComponentModel.DataAnnotations;

namespace Bookstore.Models
{
    public class bretheren
    {
        [Key]
        [Required]
        public int TaskID { get; set; }
        [Required]
        public string Task { get; set; }
        public DateTime DueDate { get; set; }
        [Required]
        public byte Quadrant { get; set; }
        public string Completed { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
