using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Concrete
{
    public class BlogManager : IBlogService
    {
        private readonly IBlogDal _blogDal;

        public BlogManager(IBlogDal blogDal)
        {
            _blogDal = blogDal;
        }

        //[SecuredOperation("Blog.List, Admin")]
        [PerformanceAspect(5)]
        [CacheAspect(duration: 10)]
        public IDataResult<List<Blog>> GetAll()
        {
            Thread.Sleep(5000);
            return new SuccessDataResult<List<Blog>>(_blogDal.GetList(), Messages.BlogListed);
        }

        [SecuredOperation("Blog.List, Admin")]
        [LogAspect(typeof(FileLogger))]
        [PerformanceAspect(5)]
        [CacheAspect(duration: 10)]
        public IDataResult<List<Blog>> GetListByTagId(int tagId)
        {
            return new SuccessDataResult<List<Blog>>(_blogDal.GetList(b => b.TagId == tagId), Messages.BlogListedByTagId);
        }

        //[SecuredOperation("Blog.Detail, Admin")]
        [PerformanceAspect(5)]
        [CacheAspect(duration: 10)]
        public IDataResult<List<BlogDetailDto>> GetBlogDetails()
        {
            Thread.Sleep(2000);
            return new SuccessDataResult<List<BlogDetailDto>>(_blogDal.GetBlogDetails(), Messages.BlogDetails);
        }

        public IDataResult<Blog> GetById(int blogId)
        {
            return new SuccessDataResult<Blog>(_blogDal.Get(b => b.BlogId == blogId));
        }

        public IDataResult<List<Blog>> GetListByTitleName(string title)
        {
            return new SuccessDataResult<List<Blog>>(_blogDal.GetList(b => b.Title == title), Messages.BlogListedByTagName);
        }

        [SecuredOperation("Blog.Add, Admin")]
        [ValidationAspect(typeof(BlogValidator), Priority = 1)]
        [CacheRemoveAspect("IBlogService.Get")]
        public IResult Add(Blog blog)
        {
            _blogDal.Add(blog);
            return new SuccessResult(Messages.BlogAdded);
        }

        [SecuredOperation("Blog.Update, Admin")]
        [ValidationAspect(typeof(BlogValidator), Priority = 1)]
        [CacheRemoveAspect("IBlogService.Get")]
        public IResult Update(Blog blog)
        {
            _blogDal.Update(blog);
            return new SuccessResult(Messages.BlogUpdated);
        }

        [SecuredOperation("Blog.Delete, Admin")]
        [CacheRemoveAspect("IBlogService.Get")]
        public IResult Delete(Blog blog)
        {
            _blogDal.Delete(blog);
            return new SuccessResult(Messages.BlogDeleted);
        }
        public IDataResult<List<BlogDetailDto>> GetBlogDetailsByBlogId(int blogId)
        {
            return new SuccessDataResult<List<BlogDetailDto>>(_blogDal.GetBlogDetails(c => c.BlogId == blogId));
        }

        public IDataResult<List<BlogDetailDto>> GetBlogDetailsByTagId(int tagId)
        {
            throw new NotImplementedException();
        }
        //public IDataResult<List<BlogDetailDto>> GetDescendingBlogDetails(int blogId)
        //{
            
        //    return new SuccessDataResult<List<BlogDetailDto>>(_blogDal.());
        //}

        [TransactionScopeAspect]
        public IResult TransactionalOperation(Blog blog)
        {
            _blogDal.Update(blog);
            _blogDal.Add(blog);
            return new SuccessResult();
        }

        
    }
}
