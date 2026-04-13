using ApplicationLayer.Base;
using ApplicationLayer.Features.AuthAndPermissions.RolePermission.Commands.Models;
using ApplicationLayer.RepoInterfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationLayer.Features.AuthAndPermissions.RolePermission.Commands.Handlers
{
    public class RemoveAllPermissionsHandler : IRequestHandler<RemoveAllPermissionsCommand, Response<Unit>>
    {
        private readonly IUnitOfWork _uow;
        private readonly ResponseHandler _responseHandler;
        public RemoveAllPermissionsHandler(IUnitOfWork uow, ResponseHandler responseHandler)
        {
            _uow = uow;
            _responseHandler = responseHandler;
        }

        public async Task<Response<Unit>> Handle(RemoveAllPermissionsCommand request, CancellationToken cancellationToken)
        {
            var rolePermissions = await _uow.RolePermissions.Query().Where(rp => rp.RoleId == request.RoleId).ToListAsync(cancellationToken);
            foreach (var rp in rolePermissions) _uow.RolePermissions.Delete(rp);
            await _uow.CompleteAsync();
            return _responseHandler.Success(Unit.Value);
        }
    }
}
