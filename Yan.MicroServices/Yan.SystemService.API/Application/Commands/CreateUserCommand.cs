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
    public class CreateUserCommand:IRequest<HandleResultDto>
    {
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Nullable<int> RoleId { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, HandleResultDto>
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly ISystemUserRepository _systemUserRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="systemUserRepository"></param>
        public CreateUserCommandHandler(ISystemUserRepository systemUserRepository)
        {
            this._systemUserRepository = systemUserRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<HandleResultDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (String.IsNullOrEmpty(request.Id))
            {
                var entity = new SystemUser(request.UserName, request.Password, request.RealName, request.Email);
                await _systemUserRepository.AddAsync(entity);
            }
            else
            {
                var entity = await this._systemUserRepository.GetAsync(request.Id);
                if (entity != null)
                {
                    entity.UpdateUser(request.UserName, request.Password, request.RealName, request.Email);
                    await _systemUserRepository.UpdateAsync(entity);
                }
            }

            await _systemUserRepository.UnitOfWork.SaveEntitiesAsync();

            return new HandleResultDto
            {
                State = 1,
            };
        }
    }
}
