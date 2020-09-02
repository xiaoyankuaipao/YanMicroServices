using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yan.MvcClient.Clients;

namespace Yan.MvcClient.ViewModel
{
    /// <summary>
    /// 
    /// </summary>
    public class LifeIndexViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public CostStatisticsOutput StatisticsOutput { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<string> NearCosts { get; set; }
    }
}
