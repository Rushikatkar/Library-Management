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

        public bool IsBorrowed { get; set; } = false; // Indicates if the book is currently borrowed

        public ICollection<BorrowingHistory> BorrowingHistories { get; set; } // Navigation property for borrowing history
    }
}
