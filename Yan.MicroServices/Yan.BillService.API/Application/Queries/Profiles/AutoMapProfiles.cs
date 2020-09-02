using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yan.BillService.API.Models;
using Yan.BillService.Domain.Aggregate;
using Yan.BillService.Domain.Entities;
using Yan.Utility;

namespace Yan.BillService.API.Application.Queries.Profiles
{
    /// <summary>
    /// 
    /// </summary>
    public class AutoMapProfiles: Profile
    {
        /// <summary>
        /// 
        /// </summary>
        public AutoMapProfiles()
        {
            CreateMap<BillItem, BillItemOutput>()
                .ForMember(c => c.BillItemTypeEnum, opts => opts.MapFrom(c => c.BillItemTypeEnum.GetDescription()));
        }
    }
}
