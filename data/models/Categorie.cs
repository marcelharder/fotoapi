using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.data.models
{
    public class Category
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
    }
    
}