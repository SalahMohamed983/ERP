using ApplicationLayer.Base;
using ApplicationLayer.Features.AuthAndPermissions.Role.Dtos;
using ApplicationLayer.Features.AuthAndPermissions.Role.Queries.Models;
using DominLayer.Entites.AuthAndPermissions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationLayer.Features.AuthAndPermissions.Role.Queries.Handlers
{
    public class GetAllRolesHandler : IRequestHandler<GetAllRolesQuery, Response<IEnumerable<RoleDto>>>
    {
        private readonly RoleManager<AspNetRole> _roleManager;
        private readonly ResponseHandler _responseHandler;

        public GetAllRolesHandler(RoleManager<AspNetRole> roleManager, ResponseHandler responseHandler)
        {
            _roleManager = roleManager;
            _responseHandler = responseHandler;
        }

        public Task<Response<IEnumerable<RoleDto>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            var roles = _roleManager.Roles.ToList();
            var dtos = roles.Select(r => new RoleDto { Id = r.Id, Name = r.Name }).ToList().AsEnumerable();
            return Task.FromResult(_responseHandler.Success(dtos));
        }
    }
}
