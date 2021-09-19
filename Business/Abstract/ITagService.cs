using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface ITagService
    {
        IDataResult<List<Tag>> GetAll();
        IDataResult<Tag> GetById(int tagId);
        IDataResult<Tag> GetByName(string tagName);
        IResult Add(Tag tag);
        IResult Update(Tag tag);
        IResult Delete(Tag tag);
    }
}
