using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElevenNote.Models.Category
{
    public class CategoryDetail
    {
        public int CategoryId { get; set; }
        public int OwnerId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set;}
    }
}