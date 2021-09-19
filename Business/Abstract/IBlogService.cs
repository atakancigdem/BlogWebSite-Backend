using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IBlogService
    {
        IDataResult<List<Blog>> GetAll();
        IDataResult<List<Blog>> GetListByTagId(int tagId);
        IDataResult<List<BlogDetailDto>> GetBlogDetails();
        //IDataResult<List<BlogDetailDto>> GetDescendingBlogDetails(int blogId);
        IDataResult<List<BlogDetailDto>> GetBlogDetailsByBlogId(int blogId);
        IDataResult<List<BlogDetailDto>> GetBlogDetailsByTagId(int tagId);
        IDataResult<Blog> GetById(int blogId);
        IDataResult<List<Blog>> GetListByTitleName(string title);
        IResult Add(Blog blog);
        IResult Update(Blog blog);
        IResult Delete(Blog blog);
    }
}
