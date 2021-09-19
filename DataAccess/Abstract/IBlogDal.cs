using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess;
using Entities.Concrete;
using Entities.Dtos;

namespace DataAccess.Abstract
{
    public interface IBlogDal : IEntityRepository<Blog>
    {
        Blog GetBlogByBlogld(int blogId);
        List<BlogDetailDto> GetBlogDetails(Expression<Func<Blog, bool>> filter = null);
        //List<BlogDetailDto> GetBlogDescendingDetails();
    }
}
