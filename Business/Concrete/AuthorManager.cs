using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class AuthorManager : IAuthorService
    {
        private readonly IAuthorDal _authorDal;

        public AuthorManager(IAuthorDal authorDal)
        {
            _authorDal = authorDal;
        }


        //[SecuredOperation("Author.List, Admin")]
        [PerformanceAspect(5)]
        [CacheAspect(duration: 10)]
        public IDataResult<List<Author>> GetAll()
        {
            return new SuccessDataResult<List<Author>>(_authorDal.GetList(), Messages.AuthorList);
        }

        public IDataResult<Author> GetById(int authorId)
        {
            return new SuccessDataResult<Author>(_authorDal.Get(a => a.AuthorId == authorId));
        }

        public IDataResult<Author> GetByName(string authorName)
        {
            return new SuccessDataResult<Author>(_authorDal.Get(a => a.AuthorName == authorName));
        }

        [SecuredOperation("Author.Add, Admin")]
        [ValidationAspect(typeof(BlogValidator), Priority = 1)]
        [CacheRemoveAspect("IAuthorService.Get")]
        public IResult Add(Author author)
        {
            _authorDal.Add(author);
            return new SuccessResult(Messages.AuthorAdd);
        }

        [SecuredOperation("Author.Update, Admin")]
        [ValidationAspect(typeof(BlogValidator), Priority = 1)]
        [CacheRemoveAspect("IAuthorService.Get")]
        public IResult Update(Author author)
        {
            _authorDal.Update(author);
            return new SuccessResult(Messages.AuthorUpdate);
        }

        [SecuredOperation("Author.Delete, Admin")]
        [CacheRemoveAspect("IAuthorService.Get")]
        public IResult Delete(Author author)
        {
            _authorDal.Delete(author);
            return new SuccessResult(Messages.AuthorDelete);
        }
    }
}
