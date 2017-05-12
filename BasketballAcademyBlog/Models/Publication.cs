using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ResidentialBlog.Models
{
    public class Publication
    {
        //
        // Primary Key of the Table "Publications" in database
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }
        
        public DateTime DateTime { get; set; }
        
        public string Link { get; set; }

        [ForeignKey("Author")]
        public string AuthorId { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ApplicationUser Author { get; set; }

    }
}