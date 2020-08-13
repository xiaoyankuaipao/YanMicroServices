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
    public class DeleteRoleCommand:IRequest<HandleResultDto>
    {
        /// <summary>
        /// 
        /// </summary>
        public string RoleId { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, HandleResultDto>
    {
        /// <summary>
        /// 
        /// </summary>
        public ISystemRoleRepository _systemRoleRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="systemRoleRepository"></param>
        public DeleteRoleCommandHandler(ISystemRoleRepository systemRoleRepository)
        {
            _systemRoleRepository = systemRoleRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<HandleResultDto> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            await _systemRoleRepository.DeleteAsync(request.RoleId, cancellationToken);
            await _systemRoleRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return new HandleResultDto
            {
                State = 1
            };
        }
    }

}
