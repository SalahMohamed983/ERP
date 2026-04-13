using ApplicationLayer.Base;
using ApplicationLayer.Features.GenralSettings.Admin.Commands.Models;
using ApplicationLayer.RepoInterfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ERP_Business_Layer.Mapper.Genral_Settings.Admin;

namespace ApplicationLayer.Features.GenralSettings.Admin.Commands.Handlers
{
    public class UpdateAdminPanelSettingHandler : IRequestHandler<UpdateAdminPanelSettingCommand, Response<Unit>>
    {
        private readonly IUnitOfWork _uow;
        private readonly ResponseHandler _responseHandler;

        public UpdateAdminPanelSettingHandler(IUnitOfWork uow, ResponseHandler responseHandler)
        {
            _uow = uow;
            _responseHandler = responseHandler;
        }

        public async Task<Response<Unit>> Handle(UpdateAdminPanelSettingCommand request, CancellationToken cancellationToken)
        {
            var entity = AdminPanelSettingMapper.ToEntity(request.Dto);
            _uow.AdminPanelSetting.Update(entity);
            await _uow.CompleteAsync();
            return _responseHandler.Success(Unit.Value);
        }
    }
}
