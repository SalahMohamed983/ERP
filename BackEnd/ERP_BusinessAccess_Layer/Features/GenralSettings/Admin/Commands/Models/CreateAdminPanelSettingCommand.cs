using ApplicationLayer.Base;
using MediatR;
using ApplicationLayer.Features.GenralSettings.Admin.Dtos;

namespace ApplicationLayer.Features.GenralSettings.Admin.Commands.Models
{
    public class CreateAdminPanelSettingCommand : IRequest<Response<int>>
    {
        public AdminPanelSettingDto Dto { get; set; } = null!;
    }
}
