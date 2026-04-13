using ApplicationLayer.Base;
using ApplicationLayer.Features.AuthAndPermissions.Permission.Commands.Models;
using ApplicationLayer.RepoInterfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationLayer.Features.AuthAndPermissions.Permission.Commands.Handlers
{
    public class UpdatePermissionHandler : IRequestHandler<UpdatePermissionCommand, Response<Unit>>
    {
        private readonly IUnitOfWork _uow;
        private readonly ResponseHandler _responseHandler;

        public UpdatePermissionHandler(IUnitOfWork uow, ResponseHandler responseHandler)
        {
            _uow = uow;
            _responseHandler = responseHandler;
        }

        public async Task<Response<Unit>> Handle(UpdatePermissionCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Permission;
            var entity = await _uow.Permissions.Query().FirstOrDefaultAsync(p => p.Id == dto.Id);
            if (entity == null) return _responseHandler.NotFound<Unit>("Permission not found.");

            if (!string.IsNullOrEmpty(dto.Code) && !string.IsNullOrEmpty(entity.Code) && !entity.Code.Equals(dto.Code, StringComparison.OrdinalIgnoreCase))
            {
                var all = await _uow.Permissions.GetAllAsync();
                if (all.Any(p => p.Code != null && p.Code.Equals(dto.Code, StringComparison.OrdinalIgnoreCase)))
                    throw new InvalidOperationException($"Permission with code '{dto.Code}' already exists.");
            }

            entity.Code = dto.Code;
            entity.Description = dto.Description;
            _uow.Permissions.Update(entity);
            await _uow.CompleteAsync();

            return _responseHandler.Success(Unit.Value);
        }
    }
}
