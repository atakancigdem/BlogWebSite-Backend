using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IAuthorService
    {
        IDataResult<List<Author>> GetAll();
        IDataResult<Author> GetById(int authorId);
        IDataResult<Author> GetByName(string authorName);
        IResult Add(Author author);
        IResult Update(Author author);
        IResult Delete(Author author);
    }
}
