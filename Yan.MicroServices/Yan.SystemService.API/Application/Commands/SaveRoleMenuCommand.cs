using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Yan.Core.Dtos;
using Yan.Infrastructure.Core.Attributes;
using Yan.SystemService.Domain.Entities;
using Yan.SystemService.Infrastructure.Repositories;

namespace Yan.SystemService.API.Application.Commands
{
    /// <summary>
    /// 
    /// </summary>
    //[UseTransaction]
    public class SaveRoleMenuCommand : IRequest<HandleResultDto>
    {
        /// <summary>
        /// 
        /// </summary>
        public string[] MenuIds { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string RoleId { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class SaveRoleMenuCommandHandler : IRequestHandler<SaveRoleMenuCommand, HandleResultDto>
    {
        /// <summary>
        /// 
        /// </summary>
        public readonly ISystemRoleRepository _systemRoleRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="systemRoleRepository"></param>
        public SaveRoleMenuCommandHandler(ISystemRoleRepository systemRoleRepository)
        {
            this._systemRoleRepository = systemRoleRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<HandleResultDto> Handle(SaveRoleMenuCommand request, CancellationToken cancellationToken)
        {
            var role = await _systemRoleRepository.GetSystemRoleWithNavById(request.RoleId, cancellationToken);
            role.UpdateRoleMenu(request.MenuIds);
            await _systemRoleRepository.UpdateAsync(role, cancellationToken);
            await _systemRoleRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return new HandleResultDto
            {
                State = 1
            };
        }
    }
}
