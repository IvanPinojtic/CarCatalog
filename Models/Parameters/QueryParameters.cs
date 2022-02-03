using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarCatalog.Models.Parameters
{
    public class QueryParameters
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 3;
    }
}
