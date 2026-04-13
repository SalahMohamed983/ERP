using ApplicationLayer.Interfaces.RepoInterfaces;
using ApplicationLayer.RepoInterfaces;
using DominLayer.Entites;
using DominLayer.Entites.AuthAndPermissions;
using InfrastructureLayer.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace InfrastructureLayer.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ERPContext _context;
        public IRepository<Admin> Admin { get; private set; }
        public IRepository<Store> Store { get; private set; }

        // Added repositories for remaining tables
        public IRepository<AdminPanelSetting> AdminPanelSetting { get; private set; }
        public IRepository<InvUom> InvUom { get; private set; }
        public IRepository<InvItemcard> InvItemcard { get; private set; }
        public IRepository<InvItemcardCategory> InvItemcardCategory { get; private set; }
        public IRepository<SuppliersCategory> SuppliersCategory { get; private set; }
        public IRepository<Suuplier> Suuplier { get; private set; }
        public IRepository<Treasury> Treasury { get; private set; }
        public IRepository<TreasuriesDelivery> TreasuriesDelivery { get; private set; }
        public IRepository<InvProductionLine> InvProductionLine { get; private set; }

        public IRepository<ApplicationUser> Users { get; private set; }

        public IRepository<AspNetRole> Roles { get; private set; }

        public IRepository<Permission> Permissions { get; private set; }

        public IRepository<RolePermission> RolePermissions { get; private set; }

        public IRepository<RefreshToken> RefreshTokens { get; private set; }

        public UnitOfWork(ERPContext context) 
        {
            _context = context;
            Admin = new Repository<Admin>(_context);
            Store = new Repository<Store>(_context);
            
            RefreshTokens = new Repository<RefreshToken>(_context);
            Users = new Repository<ApplicationUser>(_context);
            Roles = new Repository<AspNetRole>(_context);
            Permissions = new Repository<Permission>(_context);
            RolePermissions = new Repository<RolePermission>(_context);

            // initialize new repositories
            AdminPanelSetting = new Repository<AdminPanelSetting>(_context);
            InvUom = new Repository<InvUom>(_context);
            InvItemcard = new Repository<InvItemcard>(_context);
            InvItemcardCategory = new Repository<InvItemcardCategory>(_context);
            SuppliersCategory = new Repository<SuppliersCategory>(_context);
            Suuplier = new Repository<Suuplier>(_context);
            Treasury = new Repository<Treasury>(_context);
            TreasuriesDelivery = new Repository<TreasuriesDelivery>(_context);
            InvProductionLine = new Repository<InvProductionLine>(_context);
        }

        public async Task<bool> CompleteAsync()
        {
            int affectedRow = await _context.SaveChangesAsync();
            return affectedRow > 0;
        }
        public void Dispose()
        {
            _context.Dispose();
        }
        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }

    }
}
