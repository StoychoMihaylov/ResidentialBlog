using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ResidentialBlog.Models
{
    public class Comment
    {
        public int ID { get; set; }

        public int postID { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Created date")]
        public DateTime CreatedDate { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Comment")]
        public string Body { get; set; }

        public virtual Publication Publication { get; set; }

        public bool isAuthor(string name, Comment comment)
        {
            return comment.Name.Equals(name);
        }

    }
}