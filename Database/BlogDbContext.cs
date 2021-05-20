using alkemy_blog_challenge.Models;
using alkemy_blog_challenge.Models.Helper;
using EntityFramework.DynamicFilters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace alkemy_blog_challenge.Database
{
    public class BlogDbContext : DbContext 
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options)
        {

        }

        public DbSet<Post> Post { get; set; }

        private void AddMyFilters(ref System.Data.Entity.DbModelBuilder modelBuilder)
        {
            modelBuilder.Filter("IsDeleted", (ISoftDeleted d)=> d.IsDeleted, false);
        }
    }
}
