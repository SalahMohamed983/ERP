using ApplicationLayer.Base;
using ApplicationLayer.Features.AuthAndPermissions.RolePermission.Commands.Models;
using ApplicationLayer.RepoInterfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationLayer.Features.AuthAndPermissions.RolePermission.Commands.Handlers
{
    public class RemovePermissionHandler : IRequestHandler<RemovePermissionCommand, Response<Unit>>
    {
        private readonly IUnitOfWork _uow;
        private readonly ResponseHandler _responseHandler;
        public RemovePermissionHandler(IUnitOfWork uow, ResponseHandler responseHandler)
        {
            _uow = uow;
            _responseHandler = responseHandler;
        }

        public async Task<Response<Unit>> Handle(RemovePermissionCommand request, CancellationToken cancellationToken)
        {
            var rolePermission = await _uow.RolePermissions.Query().FirstOrDefaultAsync(rp => rp.RoleId == request.RoleId && rp.PermissionId == request.PermissionId);
            if (rolePermission == null) return _responseHandler.NotFound<Unit>("Permission is not assigned to this role.");

            _uow.RolePermissions.Delete(rolePermission);
            await _uow.CompleteAsync();
            return _responseHandler.Success(Unit.Value);
        }
    }
}
