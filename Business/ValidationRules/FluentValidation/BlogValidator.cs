using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class BlogValidator : AbstractValidator<Blog>
    {
        public BlogValidator()
        {
            RuleFor(b => b.Title).NotEmpty();
            RuleFor(b => b.Content).NotEmpty();
            RuleFor(b => b.TagId).NotNull();
        }
    }
}
