using ApplicationLayer.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using ApplicationLayer.Features.AuthAndPermissions.Auth.Dtos;

namespace ApplicationLayer.Features.AuthAndPermissions.Auth.Commands.Models
{
    public class LoginCommand : IRequest<Response<AuthResponseDto?>>
    {
        public LoginDto LoginDto { get; set; } = null!;
        public string? IpAddress { get; set; }
        public string? UserAgent { get; set; }
    }
}
