using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ElevenNote.Data.Entities
{
    public class CategoryEntity
    {
        [Key]
        public int CategoryId { get; set; }
        
        [ForeignKey("Owner")]
        public int OwnerId { get; set; }
        public UserEntity Owner { get; set; }
        
        [Required]
        [MaxLength(20, ErrorMessage = "{0} cannot be longer than {1} characters long.")]
        public string CategoryName { get; set; }
        
        [MaxLength(200, ErrorMessage = "{0} cannot be over {1} characters in length.")]
        public string Description { get; set; }
        
        // public virtual List<NoteEntity> Notes { get; set; }
    }
}