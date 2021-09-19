using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;

namespace Business.Abstract
{
    public interface IBlogImageService
    {
        IDataResult<List<BlogImage>> GetAll();
        IDataResult<BlogImage> GetById(int blogImageId);
        IDataResult<List<BlogImage>> GetImagesByBlogId(int blogId);
        IResult Add(IFormFile file, BlogImage blogImage);
        IResult Update(IFormFile file, BlogImage blogImage);
        IResult Delete(BlogImage blogImage);
    }
}
