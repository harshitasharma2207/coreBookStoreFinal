using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace coreBookStore.Models
{
    public class Publication
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PublicationId { get; set; }
        [Required]
        public string PublicationName { get; set; }
        [Required]
        public string PublicationDescription { get; set; }
        [Required]
        public string PublicationImage { get; set; }
        public int AdminId { get; set; }
        public Admin Admin { get; set; }

        public List<Book> Books { get; set; }

    }
}
