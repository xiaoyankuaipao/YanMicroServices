using System;
using System.Collections.Generic;
using System.Text;

namespace Yan.Core.Dtos
{
    public class ResultPage<T>
    {
        public int TotalCount { get; set; }

        public IList<T> Data { get; set; }
    }
}
