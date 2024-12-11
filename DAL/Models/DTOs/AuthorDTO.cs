using System;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models.DTOs
{
    public class AuthorDTO
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Biography { get; set; }
    }

    public class AuthorCreateDTO
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Biography { get; set; }
    }

    public class AuthorUpdateDTO
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Biography { get; set; }
    }
}
