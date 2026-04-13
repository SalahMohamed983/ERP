using ApplicationLayer.Base;
using ApplicationLayer.Features.AuthAndPermissions.Permission.Dtos;
using ApplicationLayer.Features.AuthAndPermissions.Permission.Queries.Models;
using ApplicationLayer.Mapper.AuthAndPermission;
using ApplicationLayer.RepoInterfaces;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ApplicationLayer.Features.AuthAndPermissions.Permission.Queries.Handlers
{
    public class GetAllPermissionsHandler : IRequestHandler<GetAllPermissionsQuery, Response<IEnumerable<PermissionDto>>>
    {
        private readonly IUnitOfWork _uow;
        private readonly ResponseHandler _responseHandler;

        public GetAllPermissionsHandler(IUnitOfWork uow, ResponseHandler responseHandler)
        {
            _uow = uow;
            _responseHandler = responseHandler;
        }

        public async Task<Response<IEnumerable<PermissionDto>>> Handle(GetAllPermissionsQuery request, CancellationToken cancellationToken)
        {
            var entities = await _uow.Permissions.GetAllAsync();
            var dtos = PermissionMapper.Map(entities); 
            return _responseHandler.Success(dtos);
        }
    }
}
