using ApplicationLayer.Base;
using ApplicationLayer.Features.GenralSettings.Admin.Queries.Models;
using ApplicationLayer.RepoInterfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ERP_Business_Layer.Mapper.Genral_Settings.Admin;
using Microsoft.EntityFrameworkCore;
using ApplicationLayer.Features.GenralSettings.Admin.Dtos;

namespace ApplicationLayer.Features.GenralSettings.Admin.Queries.Handlers
{
    public class GetAdminPanelSettingHandler : IRequestHandler<GetAdminPanelSettingQuery, Response<ApplicationLayer.Features.GenralSettings.Admin.Dtos.AdminPanelSettingDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly ResponseHandler _responseHandler;

        public GetAdminPanelSettingHandler(IUnitOfWork uow, ResponseHandler responseHandler)
        {
            _uow = uow;
            _responseHandler = responseHandler;
        }

        public async Task<Response<ApplicationLayer.Features.GenralSettings.Admin.Dtos.AdminPanelSettingDto>> Handle(GetAdminPanelSettingQuery request, CancellationToken cancellationToken)
        {
            var entity = await _uow.AdminPanelSetting.Query().AsNoTracking().Select(p => new AdminPanelSettingDto{
               Id= p.Id
                , SystemName= p.SystemName
            , Photo= p.Photo 
            , Active= p.Active 
            ,
                GeneralAlert =p.GeneralAlert 
            ,Address = p.Address 
            ,Phone = p.Phone 
            ,UpdatedAt = p.UpdatedAt 
            ,ComCode = p.ComCode 
            ,Notes =p.Notes 
    }).FirstOrDefaultAsync(p => p.Id == request.Id);
            if (entity == null) return _responseHandler.NotFound<ApplicationLayer.Features.GenralSettings.Admin.Dtos.AdminPanelSettingDto>("Admin panel setting not found.");
            return _responseHandler.Success(entity);
        }
    }
}
