using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models.Entities
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        public int AuthorId { get; set; }
        public Author Author { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [Required]
        public DateTime PublishedDate { get; set; }

        [Range(0, double.MaxValue)]
        public double Price { get; set; }

        public int Stock { get; set; }

        // Foreign key for the User who borrowed the book
        public int? UserId { get; set; } // Nullable to allow books not borrowed yet

        // Navigation property for the relationship with User
        public User User { get; set; }
    }
}
