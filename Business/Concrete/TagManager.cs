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
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class TagManager : ITagService
    {
        private readonly ITagDal _tagDal;

        public TagManager(ITagDal tagDal)
        {
            _tagDal = tagDal;
        }

        //[SecuredOperation("Tag.List, Admin")]
        [PerformanceAspect(5)]
        [CacheAspect(duration: 10)]
        public IDataResult<List<Tag>> GetAll()
        {
            return new SuccessDataResult<List<Tag>>(_tagDal.GetList(), Messages.TagListed);
        }

        public IDataResult<Tag> GetById(int tagId)
        {
            return new SuccessDataResult<Tag>(_tagDal.Get(t => t.TagId == tagId), Messages.GetByTag);
        }

        public IDataResult<Tag> GetByName(string tagName)
        {
            return new SuccessDataResult<Tag>(_tagDal.Get(t => t.TagName == tagName), Messages.GetByTag);
        }

        [SecuredOperation("Tag.Add, Admin")]
        [ValidationAspect(typeof(TagValidator), Priority = 1)]
        [CacheRemoveAspect("ITagService.Get")]
        public IResult Add(Tag tag)
        {
            _tagDal.Add(tag);
            return new SuccessResult(Messages.TagAdded);
        }

        [SecuredOperation("Tag.Update, Admin")]
        [ValidationAspect(typeof(TagValidator), Priority = 1)]
        [CacheRemoveAspect("ITagService.Get")]
        public IResult Update(Tag tag)
        {
            _tagDal.Update(tag);
            return new SuccessResult(Messages.TagUpdated);
        }

        [SecuredOperation("Tag.Delete, Admin")]
        [ValidationAspect(typeof(TagValidator), Priority = 1)]
        [CacheRemoveAspect("ITagService.Get")]
        public IResult Delete(Tag tag)
        {
            _tagDal.Delete(tag); 
            return new SuccessResult(Messages.TagDeleted);
        }

        [TransactionScopeAspect]
        public IResult TransactionalOperation(Tag tag)
        {
            _tagDal.Update(tag);
            _tagDal.Add(tag);
            return new SuccessResult();
        }
    }
}
