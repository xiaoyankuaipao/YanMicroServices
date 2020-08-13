using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Yan.Core.Dtos;
using Yan.SystemService.Infrastructure.Repositories;

namespace Yan.SystemService.API.Application.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteMenuCommand:IRequest<HandleResultDto>
    {
        /// <summary>
        /// 
        /// </summary>
        public string MenuId { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class DeleteMenuCommandHandler : IRequestHandler<DeleteMenuCommand, HandleResultDto>
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly ISystemMenuRepository _systemMenuRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="systemMenuRepository"></param>
        public DeleteMenuCommandHandler(ISystemMenuRepository systemMenuRepository)
        {
            this._systemMenuRepository = systemMenuRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<HandleResultDto> Handle(DeleteMenuCommand request, CancellationToken cancellationToken)
        {
            await _systemMenuRepository.DeleteAsync(request.MenuId, cancellationToken);
            await _systemMenuRepository.UnitOfWork.SaveEntitiesAsync();

            return new HandleResultDto
            {
                State = 1
            };
        }
    }

}
