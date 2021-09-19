using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete;
using Entities.Dtos;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfBlogDal : EfEntityRepositoryBase<Blog, BlogContext>, IBlogDal 
    {
        public Blog GetBlogByBlogld(int blogId)
        {
            using (var context = new BlogContext())
            {
                return context.Set<Blog>().SingleOrDefault(b => b.BlogId == blogId);
            }
        }

        //public List<BlogDetailDto> GetBlogDescendingDetails()
        //{
        //    using var context = new BlogContext();
        //    {
        //        var result = from b in context.Blogs
        //                     orderby b descending
        //                     join t in context.Tags on b.TagId equals t.TagId
        //                     join a in context.Authors on b.AuthorId equals a.AuthorId
        //                     select new BlogDetailDto
        //                     {

        //                     };
        //    }

        //}

        public List<BlogDetailDto> GetBlogDetails(Expression<Func<Blog, bool>> filter = null)
        {
            using var context = new BlogContext();
            {
                var result = from b in filter == null ? context.Blogs : context.Blogs.Where(filter)
                             orderby b descending
                             join t in context.Tags on b.TagId equals t.TagId 
                    join a in context.Authors on b.AuthorId equals a.AuthorId
                    
                    select new BlogDetailDto
                    {
                        BlogId = b.BlogId,
                        TagId = t.TagId,
                        AuthorId = a.AuthorId,
                        Title = b.Title,
                        Content = b.Content,
                        AuthorName = a.AuthorName,
                        CreateTime = b.CreateTime,
                        TagName = t.TagName
                    };
                return result.ToList();
            }
        }
    }
}
