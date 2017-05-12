using Microsoft.Owin;
using Owin;
using ResidentialBlog.Migrations;
using ResidentialBlog.Models;
using System.Data.Entity;

[assembly: OwinStartupAttribute(typeof(ResidentialBlog.Startup))]
namespace ResidentialBlog
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BlogDbContext, Configuration>());
            ConfigureAuth(app);
        }
    }
}
