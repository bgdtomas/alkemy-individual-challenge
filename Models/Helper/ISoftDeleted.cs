using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace alkemy_blog_challenge.Models.Helper
{
    public interface ISoftDeleted
    {

            bool IsDeleted { get; set; }

    }
}
