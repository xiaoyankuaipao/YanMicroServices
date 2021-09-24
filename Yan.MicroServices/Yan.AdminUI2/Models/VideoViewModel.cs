using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yan.AdminUI2.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class VideoViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string MainCodeStreamUrl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SubCodeStreamUrl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<VideoViewModel> Children { get; set; }
    }
}
