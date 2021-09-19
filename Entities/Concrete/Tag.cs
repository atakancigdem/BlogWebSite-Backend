using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Abstract;

namespace Entities.Concrete
{
    public class Tag : IEntity
    {
        public int TagId { get; set; }
        public string TagName { get; set; }
    }
}
