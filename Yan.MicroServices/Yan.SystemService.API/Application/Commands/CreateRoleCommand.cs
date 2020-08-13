using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Yan.Core.Dtos;
using Yan.SystemService.Domain.Aggregate;
using Yan.SystemService.Infrastructure.Repositories;

namespace Yan.SystemService.API.Application.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateRoleCommand:IRequest<HandleResultDto>
    {
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DisplayName { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, HandleResultDto>
    {
        /// <summary>
        /// 
        /// </summary>
        public ISystemRoleRepository _systemRoleRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="systemRoleRepository"></param>
        public CreateRoleCommandHandler(ISystemRoleRepository systemRoleRepository)
        {
            _systemRoleRepository = systemRoleRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<HandleResultDto> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Id))
            {
                var role = new SystemRole(request.Name, request.DisplayName);
                await _systemRoleRepository.AddAsync(role, cancellationToken);
            }
            else
            {
                var role = await _systemRoleRepository.GetAsync(request.Id, cancellationToken);
                if (role != null)
                {
                    role.UpdateRole(request.Name, request.DisplayName);
                }
            }

            await _systemRoleRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return new HandleResultDto
            {
                State = 1
            };
        }
    }
}
