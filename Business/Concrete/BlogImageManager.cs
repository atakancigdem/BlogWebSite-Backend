using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Helpers;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;

namespace Business.Concrete
{
    public class BlogImageManager : IBlogImageService
    {

        private readonly IBlogImageDal _blogImageDal;

        public BlogImageManager(IBlogImageDal blogImageDal)
        {
            _blogImageDal = blogImageDal;
        }

        [SecuredOperation("BlogImage.Add, Admin")]
        [ValidationAspect(typeof(BlogImageValidator), Priority = 1)]
        [CacheRemoveAspect("IBlogImageService.Get")]
        public IResult Add(IFormFile file, BlogImage blogImage)
        {
            IResult result = BusinessRules.Run(CheckImageLimitExceeded(blogImage.BlogId));
            if (result != null)
            {
                return result;
            }
            blogImage.ImagePath = FileHelper.Add(file);
            blogImage.Date = DateTime.Now;

            _blogImageDal.Add(blogImage);

            return new SuccessResult(Messages.BlogImageAdded);
        }

        [SecuredOperation("BlogImage.Delete, Admin")]
        [CacheRemoveAspect("IBlogImageService.Get")]
        public IResult Delete(BlogImage blogImage)
        {
            var oldPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\wwwroot")) + _blogImageDal.Get(b => b.Id == blogImage.Id).ImagePath;
            IResult result = BusinessRules.Run(FileHelper.Delete(oldPath));

            if (result != null)
            {
                return result;
            }

            _blogImageDal.Delete(blogImage);

            return new SuccessResult(Messages.BlogImageDeleted);
        }

        [SecuredOperation("BlogImage.List, Admin")]
        [PerformanceAspect(5)]
        [CacheAspect(duration: 10)]
        public IDataResult<List<BlogImage>> GetAll()
        {
            Thread.Sleep(5000);
            return new SuccessDataResult<List<BlogImage>>(_blogImageDal.GetList(), Messages.BlogImagesListed);
        }

        public IDataResult<BlogImage> GetById(int id)
        {
            return new SuccessDataResult<BlogImage>(_blogImageDal.Get(b => b.BlogId == id));
        }

        [SecuredOperation("BlogImage.Update, Admin")]
        [ValidationAspect(typeof(BlogImageValidator), Priority = 1)]
        [CacheRemoveAspect("IBlogImageService.Get")]
        public IResult Update(IFormFile file, BlogImage blogImage)
        {
            IResult result = BusinessRules.Run(CheckImageLimitExceeded(blogImage.BlogId));
            if (result != null)
            {
                return result;
            }

            var oldPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\wwwroot")) + _blogImageDal.Get(b => b.Id == blogImage.Id).ImagePath;

            blogImage.ImagePath = FileHelper.Update(oldPath, file);
            blogImage.Date = DateTime.Now;
            _blogImageDal.Update(blogImage);

            return new SuccessResult(Messages.BlogImageUpdated);
        }

        public IDataResult<List<BlogImage>> GetImagesByBlogId(int blogId)
        {
            IResult result = BusinessRules.Run(CheckIfBlogImageNull(blogId));
            if (result != null)
            {
                return new ErrorDataResult<List<BlogImage>>(result.Message);
            }

            return new SuccessDataResult<List<BlogImage>>(CheckIfBlogImageNull(blogId).Data);
        }

        private IResult CheckImageLimitExceeded(int blogId)
        {
            var blogImageCount = _blogImageDal.GetList(b => b.BlogId == blogId).Count;
            if (blogImageCount >= 5)
            {
                return new ErrorResult(Messages.BlogImageLimitExceeded);
            }

            return new SuccessResult();
        }
        private IDataResult<List<BlogImage>> CheckIfBlogImageNull(int id)
        {
            try
            {
                string path = @"\images\DefaultImage.jpg";
                var result = _blogImageDal.GetList(b => b.BlogId == id).Any();
                if (!result)
                {
                    List<BlogImage> blogImage = new();
                    blogImage.Add(new BlogImage { BlogId = id, ImagePath = path, Date = DateTime.Now });

                    return new SuccessDataResult<List<BlogImage>>(blogImage);
                }
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<List<BlogImage>>(exception.Message);
            }

            return new SuccessDataResult<List<BlogImage>>(_blogImageDal.GetList(b => b.BlogId == id));
        }
    }
}
