using ApplicationLayer.Base;
using ApplicationLayer.Features.GenralSettings.Admin.Commands.Models;
using ApplicationLayer.RepoInterfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ERP_Business_Layer.Mapper.Genral_Settings.Admin;

namespace ApplicationLayer.Features.GenralSettings.Admin.Commands.Handlers
{
    public class CreateAdminPanelSettingHandler : IRequestHandler<CreateAdminPanelSettingCommand, Response<int>>
    {
        private readonly IUnitOfWork _uow;
        private readonly ResponseHandler _responseHandler;

        public CreateAdminPanelSettingHandler(IUnitOfWork uow, ResponseHandler responseHandler)
        {
            _uow = uow;
            _responseHandler = responseHandler;
        }

        public async Task<Response<int>> Handle(CreateAdminPanelSettingCommand request, CancellationToken cancellationToken)
        {
            var entity = AdminPanelSettingMapper.ToEntity(request.Dto);
            await _uow.AdminPanelSetting.AddAsync(entity);
            await _uow.CompleteAsync();
            return _responseHandler.Created((int)entity.Id);
        }
    }
}
