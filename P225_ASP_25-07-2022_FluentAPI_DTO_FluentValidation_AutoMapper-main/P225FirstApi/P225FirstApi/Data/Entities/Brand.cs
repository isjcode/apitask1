using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P225FirstApi.Data.Entities
{
    public class Brand : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
