using ApplicationLayer.Base;
using ApplicationLayer.Common;
using ApplicationLayer.Features.AuthAndPermissions.User.Dtos;
using MediatR;

namespace ApplicationLayer.Features.AuthAndPermissions.User.Queries.Models
{
    public class GetAllUsersQuery : IRequest<Response<PagedResponseDto<UserDto>>>
    {
        public PagedRequestDto Request { get; set; } = null!;
    }
}
