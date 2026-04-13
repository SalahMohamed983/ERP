using ApplicationLayer.Base;
using ApplicationLayer.Features.AuthAndPermissions.Permission.Commands.Models;
using ApplicationLayer.RepoInterfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationLayer.Features.AuthAndPermissions.Permission.Commands.Handlers
{
    public class DeletePermissionHandler : IRequestHandler<DeletePermissionCommand, Response<Unit>>
    {
        private readonly IUnitOfWork _uow;
        private readonly ResponseHandler _responseHandler;
        public DeletePermissionHandler(IUnitOfWork uow, ResponseHandler responseHandler)
        {
            _uow = uow;
            _responseHandler = responseHandler;
        }

        public async Task<Response<Unit>> Handle(DeletePermissionCommand request, CancellationToken cancellationToken)
        {
            var entity = await _uow.Permissions.Query().FirstOrDefaultAsync(p => p.Id == request.PermissionId);
            if (entity == null) return _responseHandler.NotFound<Unit>("Permission not found.");

            var isAssigned = await _uow.RolePermissions.Query().AnyAsync(rp => rp.PermissionId == request.PermissionId);
            if (isAssigned) throw new InvalidOperationException("Cannot delete permission that is assigned to roles. Remove it from all roles first.");

            _uow.Permissions.Delete(entity);
            await _uow.CompleteAsync();

            return _responseHandler.Deleted<Unit>();
        }
    }
}
