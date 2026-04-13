using ApplicationLayer.Base;
using MediatR;
using System;
using System.Collections.Generic;

namespace ApplicationLayer.Features.AuthAndPermissions.UserPermission.Queries.Models
{
    public class GetUserPermissionCodesQuery : IRequest<Response<IEnumerable<string>>>
    {
        public Guid UserId { get; set; }
    }
}
