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
    public class UpdateUserRoleCommand:IRequest<HandleResultDto>
    {
        /// <summary>
        /// 
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string RoleId { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class UpdateUserRoleCommandHandler : IRequestHandler<UpdateUserRoleCommand, HandleResultDto>
    {
        /// <summary>
        /// 
        /// </summary>
        public readonly ISystemUserRepository _systemUserRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="systemUserRepository"></param>
        public UpdateUserRoleCommandHandler(ISystemUserRepository systemUserRepository)
        {
            this._systemUserRepository = systemUserRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<HandleResultDto> Handle(UpdateUserRoleCommand request, CancellationToken cancellationToken)
        {
            var user = await _systemUserRepository.GetAsync(request.UserId, cancellationToken);

            if (user != null)
            {
                user.SetUserRole(request.RoleId);
            }

            await _systemUserRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return new HandleResultDto
            {
                State = 1
            };

        }
    }

}
