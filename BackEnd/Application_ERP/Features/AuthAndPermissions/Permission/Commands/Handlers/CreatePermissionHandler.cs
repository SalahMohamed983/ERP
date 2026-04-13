using ApplicationLayer.Base;
using ApplicationLayer.Features.AuthAndPermissions.Permission.Commands.Models;
using ApplicationLayer.Mapper.AuthAndPermission;
using ApplicationLayer.RepoInterfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationLayer.Features.AuthAndPermissions.Permission.Commands.Handlers
{
    public class CreatePermissionHandler : IRequestHandler<CreatePermissionCommand, Response<int>>
    {
        private readonly IUnitOfWork _uow;
        private readonly ResponseHandler _responseHandler;

        public CreatePermissionHandler(IUnitOfWork uow, ResponseHandler responseHandler)
        {
            _uow = uow;
            _responseHandler = responseHandler;
        }

        public async Task<Response<int>> Handle(CreatePermissionCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Permission;
            if (!string.IsNullOrEmpty(dto.Code))
            {
                var all = await _uow.Permissions.GetAllAsync();
                if (all.Any(p => p.Code != null && p.Code.Equals(dto.Code, StringComparison.OrdinalIgnoreCase)))
                    throw new InvalidOperationException($"Permission with code '{dto.Code}' already exists.");
            }

            var entity = PermissionMapper.Map(dto);
            await _uow.Permissions.AddAsync(entity);
            await _uow.CompleteAsync();
            return _responseHandler.Created(entity.Id);
        }
    }
}
