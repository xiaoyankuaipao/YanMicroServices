using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yan.BillService.API.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class EChartPieData
    {
        /// <summary>
        /// 
        /// </summary>
        public decimal Value { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class PieDbData
    {
        /// <summary>
        /// 
        /// </summary>
        public decimal Value { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Type { get; set; }
    }

}
