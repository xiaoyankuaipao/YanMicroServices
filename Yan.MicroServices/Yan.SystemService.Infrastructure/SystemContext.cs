using DotNetCore.CAP;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Yan.Infrastructure.Core;

namespace Yan.SystemService.Infrastructure
{
    public class SystemContext : EFContext
    {
        public SystemContext(DbContextOptions options, IMediator mediator, ICapPublisher capBus) : base(options, mediator, capBus)
        {
        }
    }

        
}
