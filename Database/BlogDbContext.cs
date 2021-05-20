using alkemy_blog_challenge.Models;
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

        public override int SaveChanges()
        {
            foreach(var entry in ChangeTracker.Entries())
            {
                var entity = entry.Entity;
                if(entry.State== EntityState.Deleted && entity is ISoftDelete)
                {
                    entry.State = EntityState.Modified;
                    entity.GetType().GetProperty("SoftDeleted").SetValue(entity, true);
                }
            }


            return base.SaveChanges();
        }
    }
}
