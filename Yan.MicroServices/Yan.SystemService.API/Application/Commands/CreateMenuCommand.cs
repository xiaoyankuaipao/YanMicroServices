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
    public class CreateMenuCommand: IRequest<HandleResultDto>
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
        public string Code { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int MenuType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ParentId { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class CreateMenuCommandHandler : IRequestHandler<CreateMenuCommand, HandleResultDto>
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly ISystemMenuRepository _systemMenuRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="systemMenuRepository"></param>
        public CreateMenuCommandHandler(ISystemMenuRepository systemMenuRepository)
        {
            this._systemMenuRepository = systemMenuRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<HandleResultDto> Handle(CreateMenuCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Id))
            {
                var menu = new SystemMenu(request.Name, request.Code, request.Address, request.Icon, request.MenuType, request.ParentId);
                await _systemMenuRepository.AddAsync(menu, cancellationToken);
            }
            else
            {
                var menu = await _systemMenuRepository.GetAsync(request.Id, cancellationToken);
                if (menu != null)
                {
                    menu.UpdateMenu(request.Name, request.Code, request.Address, request.Icon, request.MenuType, request.ParentId);
                    await _systemMenuRepository.UpdateAsync(menu, cancellationToken);
                }
            }

            await _systemMenuRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return new HandleResultDto
            {
                State = 1
            };
        }
    }
}
