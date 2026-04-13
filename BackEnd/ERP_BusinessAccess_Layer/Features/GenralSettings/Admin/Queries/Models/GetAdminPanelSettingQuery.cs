using ApplicationLayer.Base;
using MediatR;
using ApplicationLayer.Features.GenralSettings.Admin.Dtos;

namespace ApplicationLayer.Features.GenralSettings.Admin.Queries.Models
{
    public class GetAdminPanelSettingQuery : IRequest<Response<AdminPanelSettingDto>>
    {
        public int Id { get; set; }
    }
}
