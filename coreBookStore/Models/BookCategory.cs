using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace coreBookStore.Models
{
    public class BookCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookCategoryId { get; set; }
        [Required]
        public string BookCategoryName { get; set; }
        [Required]
        public string BookCategoryDescription { get; set; }
        [Required]
        public string BookCategoryImage { get; set; }

        public List<Book> Books { get; set; }
    }
}
