using System;
using Core.Entities.Abstract;

namespace Entities.Concrete
{
    public class Blog : IEntity
    {
        public int BlogId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreateTime { get; set; }
        public int? TagId { get; set; }
        public int AuthorId { get; set; }
    }
}
