using ApplicationLayer.Interfaces.RepoInterfaces;
using DominLayer.Entites;
using DominLayer.Entites.AuthAndPermissions;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace ApplicationLayer.RepoInterfaces
{
    public interface IUnitOfWork : IDisposable
    {

        IRepository<ApplicationUser> Users { get; }
        IRepository<AspNetRole> Roles { get; }
        IRepository<Permission> Permissions { get; }
        IRepository<RolePermission> RolePermissions { get; }
        IRepository<RefreshToken> RefreshTokens { get; }

        IRepository<Store> Store { get; }
        IRepository<Admin> Admin { get; }
        IRepository<InvUom> InvUom { get; }
        IRepository<AdminPanelSetting> AdminPanelSetting { get; }
        IRepository<InvItemcard> InvItemcard { get; }
        IRepository<InvItemcardCategory> InvItemcardCategory { get; }
        IRepository<SuppliersCategory> SuppliersCategory { get; }
        IRepository<Suuplier> Suuplier { get; }
        IRepository<Treasury> Treasury { get; }
        IRepository<TreasuriesDelivery> TreasuriesDelivery { get; }
        IRepository<InvProductionLine> InvProductionLine { get; }
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task<bool> CompleteAsync();
    }
}
