using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.DTOs
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }


    public class CategoryCreateDTO
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }



    public class CategoryUpdateDTO
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
