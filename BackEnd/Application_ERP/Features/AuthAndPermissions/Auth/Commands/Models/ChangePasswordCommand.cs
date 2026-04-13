using ApplicationLayer.Base;
using ApplicationLayer.Features.AuthAndPermissions.Auth.Dtos;
using MediatR;
using System;

namespace ApplicationLayer.Features.AuthAndPermissions.Auth.Commands.Models
{
    public class ChangePasswordCommand : IRequest<Response<bool>>
    {
        public Guid UserId { get; set; }
        public ChangePasswordDto ChangePasswordDto { get; set; } = null!;
    }
}
