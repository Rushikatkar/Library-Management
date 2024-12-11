using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.DTOs
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public string CategoryName { get; set; }
        public DateTime PublishedDate { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
    }


    public class BookCreateDTO
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        public int AuthorId { get; set; }
        public int CategoryId { get; set; }

        [Required]
        public DateTime PublishedDate { get; set; }

        [Range(0, double.MaxValue)]
        public double Price { get; set; }

        public int Stock { get; set; }
    }

    public class BookUpdateDTO
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        public int AuthorId { get; set; }
        public int CategoryId { get; set; }

        [Required]
        public DateTime PublishedDate { get; set; }

        [Range(0, double.MaxValue)]
        public double Price { get; set; }

        public int Stock { get; set; }
    }

}
