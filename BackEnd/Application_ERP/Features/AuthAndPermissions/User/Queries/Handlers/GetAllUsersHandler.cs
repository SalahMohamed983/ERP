using ApplicationLayer.Base;
using ApplicationLayer.Features.AuthAndPermissions.User.Dtos;
using ApplicationLayer.Features.AuthAndPermissions.User.Queries.Models;
using ApplicationLayer.RepoInterfaces;
using DominLayer.Entites.AuthAndPermissions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApplicationLayer.Mapper.AuthAndPermission;
using ApplicationLayer.Common;

namespace ApplicationLayer.Features.AuthAndPermissions.User.Queries.Handlers
{
    public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, Response<PagedResponseDto<UserDto>>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _uow;
        private readonly ResponseHandler _responseHandler;

        public GetAllUsersHandler(UserManager<ApplicationUser> userManager, IUnitOfWork uow, ResponseHandler responseHandler)
        {
            _userManager = userManager;
            _uow = uow;
            _responseHandler = responseHandler;
        }

        public async Task<Response<PagedResponseDto<UserDto>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var req = request.Request;

            var query = _uow.Users.Query();
            if (!string.IsNullOrWhiteSpace(req.SearchTerm))
            {
                var st = req.SearchTerm.Trim();
                query = query.Where(u => (u.Email != null && u.Email.Contains(st)) || (u.FullName != null && u.FullName.Contains(st)) || (u.PhoneNumber != null && u.PhoneNumber.Contains(st)));
            }

            var totalCount = await query.CountAsync();
            var users = await query.OrderByDescending(u => u.CreatedDate).Skip((req.PageNumber - 1) * req.PageSize).Take(req.PageSize).ToListAsync();

            // Use Mapperly generated mapper to map users
            var userDtos = UserMapper.Map(users).ToList();

            var responseDto = new PagedResponseDto<UserDto>
            {
                Data = userDtos,
                PageNumber = req.PageNumber,
                PageSize = req.PageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)req.PageSize)
            };

            return _responseHandler.Success(responseDto);
        }
    }
}
