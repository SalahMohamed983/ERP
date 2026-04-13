using ApplicationLayer.Base;
using ApplicationLayer.Features.AuthAndPermissions.RolePermission.Commands.Models;
using DominLayer.Entites.AuthAndPermissions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationLayer.Features.AuthAndPermissions.RolePermission.Commands.Handlers
{
    public class AssignMultiplePermissionsHandler : IRequestHandler<AssignMultiplePermissionsCommand, Response<Unit>>
    {
        private readonly ApplicationLayer.RepoInterfaces.IUnitOfWork _uow;
        private readonly ResponseHandler _responseHandler;

        public AssignMultiplePermissionsHandler(ApplicationLayer.RepoInterfaces.IUnitOfWork uow, ResponseHandler responseHandler)
        {
            _uow = uow;
            _responseHandler = responseHandler;
        }

        public async Task<Response<Unit>> Handle(AssignMultiplePermissionsCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Dto;
            var role = await _uow.Roles.GetByIdAsync(dto.RoleId);
            if (role == null) throw new InvalidOperationException($"Role with ID {dto.RoleId} not found.");

            var allPermissions = await _uow.Permissions.GetAllAsync();
            var invalidPermissions = dto.PermissionIds.Where(id => !allPermissions.Any(p => p.Id == id)).ToList();
            if (invalidPermissions.Any()) throw new InvalidOperationException($"Invalid permission IDs: {string.Join(',', invalidPermissions)}");

            var existingPermissions = await _uow.RolePermissions.Query().Where(rp => rp.RoleId == dto.RoleId).Select(rp => rp.PermissionId).ToListAsync();
            var newPermissions = dto.PermissionIds.Where(id => !existingPermissions.Contains(id)).ToList();

            foreach (var pid in newPermissions)
            {
                await _uow.RolePermissions.AddAsync(new DominLayer.Entites.AuthAndPermissions.RolePermission { RoleId = dto.RoleId, PermissionId = pid });
            }

            await _uow.CompleteAsync();
            return _responseHandler.Success(Unit.Value);
        }
    }
}
