using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yan.MvcClient.Models
{
    public class BillPageInput
    {
        /// <summary>
        /// 
        /// </summary>
        public DateTime BeginTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Size { get; set; }
    }
}
