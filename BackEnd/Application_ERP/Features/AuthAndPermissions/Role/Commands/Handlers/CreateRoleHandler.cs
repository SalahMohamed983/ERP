using ApplicationLayer.Base;
using ApplicationLayer.Features.AuthAndPermissions.Role.Commands.Models;
using ApplicationLayer.RepoInterfaces;
using DominLayer.Entites.AuthAndPermissions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationLayer.Features.AuthAndPermissions.Role.Commands.Handlers
{
    public class CreateRoleHandler : IRequestHandler<CreateRoleCommand, Response<Guid>>
    {
        private readonly IUnitOfWork _uow;
        private readonly RoleManager<AspNetRole> _roleManager;
        private readonly ResponseHandler _responseHandler;

        public CreateRoleHandler(IUnitOfWork uow, RoleManager<AspNetRole> roleManager, ResponseHandler responseHandler)
        {
            _uow = uow;
            _roleManager = roleManager;
            _responseHandler = responseHandler;
        }

        public async Task<Response<Guid>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Role;
            if (string.IsNullOrEmpty(dto.Name)) throw new ArgumentException("Role name is required.");

            if (await _roleManager.RoleExistsAsync(dto.Name)) throw new InvalidOperationException($"Role '{dto.Name}' already exists.");

            var role = new AspNetRole { Id = Guid.NewGuid(), Name = dto.Name, NormalizedName = dto.Name.ToUpperInvariant() };
            var result = await _roleManager.CreateAsync(role);
            if (!result.Succeeded) throw new InvalidOperationException($"Failed to create role: {string.Join(',', result.Errors.Select(e => e.Description))}");

            return _responseHandler.Created(role.Id);
        }
    }
}
