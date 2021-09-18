using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yan.AdminUI2.Models
{
    public class Paged<T>
    {
        public int TotalCount { get; set; }

        public List<T> Datas { get; set; }
    }
}
