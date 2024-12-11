using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.Entities
{
    public class BorrowingHistory
    {
        public int Id { get; set; }

        [Required]
        public int BookId { get; set; }
        public Book Book { get; set; } // Navigation property to Book

        [Required]
        public int UserId { get; set; }
        public User User { get; set; } // Navigation property to User

        [Required]
        public DateTime BorrowedDate { get; set; } // Date when the book was borrowed

        public DateTime? ReturnedDate { get; set; } // Nullable; set when the book is returned

        [Range(0, double.MaxValue)]
        public double LateFee { get; set; } = 0; // Calculated late fee for the borrowing period
    }
}
