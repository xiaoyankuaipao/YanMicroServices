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
    public class DeleteUserCommand:IRequest<HandleResultDto>
    {
        /// <summary>
        /// 
        /// </summary>
        public string UserId { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, HandleResultDto>
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly ISystemUserRepository _systemUserRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="systemUserRepository"></param>
        public DeleteUserCommandHandler(ISystemUserRepository systemUserRepository)
        {
            this._systemUserRepository = systemUserRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<HandleResultDto> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            await _systemUserRepository.DeleteAsync(request.UserId, cancellationToken);
            await _systemUserRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            var result = new HandleResultDto
            {
                State = 1
            };

            return result;

        }

    }

}
