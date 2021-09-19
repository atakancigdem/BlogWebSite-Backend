using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Abstract;

namespace Entities.Dtos
{
    public class BlogDetailDto : IDto
    {
        public int BlogId { get; set; }
        public int TagId { get; set; }
        public int AuthorId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string TagName { get; set; }
        public string AuthorName { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
