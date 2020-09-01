using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yan.MvcClient.Clients
{
    /// <summary>
    /// 
    /// </summary>
    public class CostStatisticsOutput
    {
        /// <summary>
        /// 
        /// </summary>
        public string TheYearCost { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TheMonthCost { get; set; }
    }

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
}
