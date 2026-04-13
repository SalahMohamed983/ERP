using ApplicationLayer.Base;
using MediatR;
using ApplicationLayer.Features.GenralSettings.Admin.Dtos;

namespace ApplicationLayer.Features.GenralSettings.Admin.Commands.Models
{
    public class UpdateAdminPanelSettingCommand : IRequest<Response<Unit>>
    {
        public AdminPanelSettingDto Dto { get; set; } = null!;
    }
}
