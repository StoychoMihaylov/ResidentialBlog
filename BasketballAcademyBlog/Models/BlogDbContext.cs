using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ResidentialBlog.Models
{
    

    public class BlogDbContext : IdentityDbContext<ApplicationUser>
    {
        public BlogDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public virtual IDbSet<Publication> Publications { get; set; }

        public virtual DbSet<Comment> Comments { get; set; }

        public static BlogDbContext Create()
        {
            return new BlogDbContext();
        }
    }
}