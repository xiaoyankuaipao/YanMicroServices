using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yan.SystemService.API.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class ComboxTreeDto
    {
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<ComboxTreeDto> Children { get; set; }
    }
}
