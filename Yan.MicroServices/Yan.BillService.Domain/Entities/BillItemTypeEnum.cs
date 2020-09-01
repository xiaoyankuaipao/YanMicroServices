using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Yan.BillService.Domain.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public enum BillItemTypeEnum
    {
        [Description("衣")]
        衣 = 1,
        [Description("食")]
        食 = 2,
        [Description("住")]
        住 = 3,
        [Description("行 ")]
        行 = 4,
        [Description("交友")]
        交友 = 5,
        [Description("娱乐")]
        娱乐 = 6
    }
}
