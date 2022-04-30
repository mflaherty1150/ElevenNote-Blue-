using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ElevenNote.Models.Category
{
    public class CategoryCreate
    {
        [Required]
        [MaxLength(20, ErrorMessage = "{0} cannot be longer than {1} characters long.")]
        public string CategoryName { get; set; }
        
        [MaxLength(200, ErrorMessage = "{0} cannot be over {1} characters in length.")]
        public string Description { get; set; }
    }
}