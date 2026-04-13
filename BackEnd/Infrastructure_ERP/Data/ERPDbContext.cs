using DominLayer.Entites;
using DominLayer.Entites.AuthAndPermissions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureLayer.Data;

public partial class ERPContext : IdentityDbContext<ApplicationUser, AspNetRole, Guid>
{
    public ERPContext(DbContextOptions<ERPContext> options)
        : base(options) { }
    public virtual DbSet<RolePermission> RolePermissions { get; set; } = null!;
    public virtual DbSet<Permission> Permissions { get; set; } = null!;
    public virtual DbSet<RefreshToken> RefreshTokens { get; set; } = null!;

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<AccountType> AccountTypes { get; set; }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<AdminPanelSetting> AdminPanelSettings { get; set; }

    public virtual DbSet<AdminsShift> AdminsShifts { get; set; }

    public virtual DbSet<AdminsStore> AdminsStores { get; set; }

    public virtual DbSet<AdminsTreasury> AdminsTreasuries { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Delegates> Delegates { get; set; }

    public virtual DbSet<InvItemcard> InvItemcards { get; set; }

    public virtual DbSet<InvItemcardBatch> InvItemcardBatches { get; set; }

    public virtual DbSet<InvItemcardCategory> InvItemcardCategories { get; set; }

    public virtual DbSet<InvItemcardMovement> InvItemcardMovements { get; set; }

    public virtual DbSet<InvItemcardMovementsCategory> InvItemcardMovementsCategories { get; set; }

    public virtual DbSet<InvItemcardMovementsType> InvItemcardMovementsTypes { get; set; }

    public virtual DbSet<InvProductionExchange> InvProductionExchanges { get; set; }

    public virtual DbSet<InvProductionExchangeDetail> InvProductionExchangeDetails { get; set; }

    public virtual DbSet<InvProductionLine> InvProductionLines { get; set; }

    public virtual DbSet<InvProductionOrder> InvProductionOrders { get; set; }

    public virtual DbSet<InvProductionReceive> InvProductionReceives { get; set; }

    public virtual DbSet<InvProductionReceiveDetail> InvProductionReceiveDetails { get; set; }

    public virtual DbSet<InvStoresInventory> InvStoresInventories { get; set; }

    public virtual DbSet<InvStoresInventoryDetail> InvStoresInventoryDetails { get; set; }

    public virtual DbSet<InvStoresTransfer> InvStoresTransfers { get; set; }

    public virtual DbSet<InvStoresTransferDetail> InvStoresTransferDetails { get; set; }

    public virtual DbSet<InvUom> InvUoms { get; set; }

    public virtual DbSet<MovType> MovTypes { get; set; }

    public virtual DbSet<PersonalAccessToken> PersonalAccessTokens { get; set; }

    public virtual DbSet<SalesInvoice> SalesInvoices { get; set; }

    public virtual DbSet<SalesInvoicesDetail> SalesInvoicesDetails { get; set; }

    public virtual DbSet<SalesInvoicesReturn> SalesInvoicesReturns { get; set; }

    public virtual DbSet<SalesInvoicesReturnDetail> SalesInvoicesReturnDetails { get; set; }

    public virtual DbSet<SalesMatrialType> SalesMatrialTypes { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<ServicesWithOrder> ServicesWithOrders { get; set; }

    public virtual DbSet<ServicesWithOrdersDetail> ServicesWithOrdersDetails { get; set; }

    public virtual DbSet<Store> Stores { get; set; }

    public virtual DbSet<SuppliersCategory> SuppliersCategories { get; set; }

    public virtual DbSet<SuppliersWithOrder> SuppliersWithOrders { get; set; }

    public virtual DbSet<SuppliersWithOrdersDetail> SuppliersWithOrdersDetails { get; set; }

    public virtual DbSet<Suuplier> Suupliers { get; set; }

    public virtual DbSet<TreasuriesDelivery> TreasuriesDeliveries { get; set; }

    public virtual DbSet<TreasuriesTransaction> TreasuriesTransactions { get; set; }

    public virtual DbSet<Treasury> Treasuries { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=ErpSales;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<RolePermission>(entity =>
        {
            entity.HasKey(rp => new { rp.RoleId, rp.PermissionId });

            entity.HasOne(rp => rp.Role)
                   .WithMany(r => r.RolePermissions)
                   .HasForeignKey(rp => rp.RoleId)
                   .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(rp => rp.Permission)
                   .WithMany()
                   .HasForeignKey(rp => rp.PermissionId)
                   .OnDelete(DeleteBehavior.Cascade);
        });

        // ---- RefreshToken ----
        modelBuilder.Entity<RefreshToken>(entity =>
        {
           entity.HasOne(rt => rt.User)
                   .WithMany(u => u.RefreshTokens)
                   .HasForeignKey(rt => rt.UserId)
                   .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<RolePermission>(entity =>
        {
            // تعريف المفتاح الأساسي المركب
            entity.HasKey(rp => new { rp.RoleId, rp.PermissionId });

            // علاقة Role مع RolePermission
            entity.HasOne(rp => rp.Role)
                   .WithMany(r => r.RolePermissions)
                   .HasForeignKey(rp => rp.RoleId)
                   .OnDelete(DeleteBehavior.Cascade);

            // علاقة Permission مع RolePermission
            entity.HasOne(rp => rp.Permission)
                   .WithMany()  // لو ما فيش Navigation Property للـ RolePermission داخل Permission
                   .HasForeignKey(rp => rp.PermissionId)
                   .OnDelete(DeleteBehavior.Cascade);
        });


        modelBuilder.UseCollation("Arabic_CI_AS");

        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__accounts__3213E83F61A17648");

            entity.Property(e => e.Active).HasDefaultValue(true);
        });

        modelBuilder.Entity<AccountType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__account___3213E83F97943C44");
        });

        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__admins__3213E83F184D4D7F");
        });

        modelBuilder.Entity<AdminPanelSetting>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__admin_pa__3213E83FA46A2087");

            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.DefaultUnit).HasDefaultValue((byte)1);
        });

        modelBuilder.Entity<AdminsShift>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__admins_s__3213E83F9C84680D");
        });

        modelBuilder.Entity<AdminsStore>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__admins_s__3213E83F2B88BC8F");

            entity.Property(e => e.Active).HasDefaultValue(true);

            entity.HasOne(d => d.Admin).WithMany(p => p.AdminsStores)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_admins_stores_admin_id");

            entity.HasOne(d => d.Store).WithMany(p => p.AdminsStores)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_admins_stores_store_id");
        });

        modelBuilder.Entity<AdminsTreasury>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__admins_t__3213E83F3039E8F6");

            entity.Property(e => e.Active).HasDefaultValue(true);

            entity.HasOne(d => d.Admin).WithMany(p => p.AdminsTreasuries)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_admins_treasuries_admin_id");

            entity.HasOne(d => d.Treasuries).WithMany(p => p.AdminsTreasuries).HasConstraintName("FK_admins_treasuries_treasuries");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__customer__3213E83F15910068");
        });

        modelBuilder.Entity<Delegates>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__delegate__3213E83FC106541B");
        });

        modelBuilder.Entity<InvItemcard>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__inv_item__3213E83F59BDEE60");

            entity.Property(e => e.Active).HasDefaultValue(true);
        });

        modelBuilder.Entity<InvItemcardBatch>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__inv_item__3213E83F4292501B");
        });

        modelBuilder.Entity<InvItemcardCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__inv_item__3213E83F6E5DDDD8");

            entity.Property(e => e.Active).HasDefaultValue(true);
        });

        modelBuilder.Entity<InvItemcardMovement>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__inv_item__3213E83FC0932887");
        });

        modelBuilder.Entity<InvItemcardMovementsCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__inv_item__3213E83FB75097D7");
        });

        modelBuilder.Entity<InvItemcardMovementsType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__inv_item__3213E83F8AD577B9");
        });

        modelBuilder.Entity<InvProductionExchange>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__inv_prod__3213E83FCC638D8D");

            entity.Property(e => e.DiscountPercent).HasDefaultValue(0.00m);
            entity.Property(e => e.TaxPercent).HasDefaultValue(0.00m);
            entity.Property(e => e.TaxValue).HasDefaultValue(0.00m);
            entity.Property(e => e.TotalCost).HasDefaultValue(0.00m);
            entity.Property(e => e.WhatPaid).HasDefaultValue(0.00m);
            entity.Property(e => e.WhatRemain).HasDefaultValue(0.00m);
        });

        modelBuilder.Entity<InvProductionExchangeDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__inv_prod__3213E83F094CB62C");

            entity.HasOne(d => d.InvProductionExchange).WithMany(p => p.InvProductionExchangeDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_inv_production_exchange_details");
        });

        modelBuilder.Entity<InvProductionLine>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__inv_prod__3213E83F2E5635C1");
        });

        modelBuilder.Entity<InvProductionOrder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__inv_prod__3213E83FAC952FA2");
        });

        modelBuilder.Entity<InvProductionReceive>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__inv_prod__3213E83FE92EDF46");

            entity.Property(e => e.DiscountPercent).HasDefaultValue(0.00m);
            entity.Property(e => e.TaxPercent).HasDefaultValue(0.00m);
            entity.Property(e => e.TaxValue).HasDefaultValue(0.00m);
            entity.Property(e => e.TotalCost).HasDefaultValue(0.00m);
            entity.Property(e => e.WhatPaid).HasDefaultValue(0.00m);
            entity.Property(e => e.WhatRemain).HasDefaultValue(0.00m);
        });

        modelBuilder.Entity<InvProductionReceiveDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__inv_prod__3213E83F6FA5029A");

            entity.HasOne(d => d.InvProductionReceive).WithMany(p => p.InvProductionReceiveDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_inv_production_receive_details");
        });

        modelBuilder.Entity<InvStoresInventory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__inv_stor__3213E83F0614560D");
        });

        modelBuilder.Entity<InvStoresInventoryDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__inv_stor__3213E83F680AD558");

            entity.HasOne(d => d.InvStoresInventory).WithMany(p => p.InvStoresInventoryDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_inv_stores_inventory_details");
        });

        modelBuilder.Entity<InvStoresTransfer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__inv_stor__3213E83F76B7E085");
        });

        modelBuilder.Entity<InvStoresTransferDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__inv_stor__3213E83F68A41098");

            entity.Property(e => e.IsApproved).HasDefaultValue(false);
            entity.Property(e => e.IsCanceldReceive).HasDefaultValue(false);

            entity.HasOne(d => d.InvStoresTransfer).WithMany(p => p.InvStoresTransferDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_inv_stores_transfer_details");
        });

        modelBuilder.Entity<InvUom>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__inv_uoms__3213E83F6C289799");

            entity.Property(e => e.Active).HasDefaultValue(true);
        });

        modelBuilder.Entity<MovType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__mov_type__3213E83FFF689CB0");

            entity.Property(e => e.Active).HasDefaultValue(true);
        });

        modelBuilder.Entity<PersonalAccessToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__personal__3213E83F3351379E");
        });

        modelBuilder.Entity<SalesInvoice>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__sales_in__3213E83FE0BE8227");

            entity.Property(e => e.DiscountPercent).HasDefaultValue(0.00m);
            entity.Property(e => e.TaxPercent).HasDefaultValue(0.00m);
            entity.Property(e => e.TaxValue).HasDefaultValue(0.00m);
            entity.Property(e => e.TotalCost).HasDefaultValue(0.00m);
        });

        modelBuilder.Entity<SalesInvoicesDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__sales_in__3213E83FFE58D7DE");

            entity.HasOne(d => d.SalesInvoices).WithMany(p => p.SalesInvoicesDetails).HasConstraintName("FK_sales_invoices_details");
        });

        modelBuilder.Entity<SalesInvoicesReturn>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__sales_in__3213E83F4748605F");

            entity.Property(e => e.DiscountPercent).HasDefaultValue(0.00m);
            entity.Property(e => e.TaxPercent).HasDefaultValue(0.00m);
            entity.Property(e => e.TaxValue).HasDefaultValue(0.00m);
            entity.Property(e => e.TotalCost).HasDefaultValue(0.00m);
        });

        modelBuilder.Entity<SalesInvoicesReturnDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__sales_in__3213E83F3CFAE5A2");

            entity.HasOne(d => d.SalesInvoicesReturn).WithMany(p => p.SalesInvoicesReturnDetails).HasConstraintName("FK_sales_invoices_return_details");
        });

        modelBuilder.Entity<SalesMatrialType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__sales_ma__3213E83F003FF30A");

            entity.Property(e => e.Active).HasDefaultValue(true);
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__services__3213E83F6C4DE484");

            entity.Property(e => e.Active).HasDefaultValue(true);
        });

        modelBuilder.Entity<ServicesWithOrder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__services__3213E83FCC2F0101");

            entity.Property(e => e.DiscountPercent).HasDefaultValue(0.00m);
            entity.Property(e => e.TaxPercent).HasDefaultValue(0.00m);
            entity.Property(e => e.TaxValue).HasDefaultValue(0.00m);
            entity.Property(e => e.TotalCost).HasDefaultValue(0.00m);
            entity.Property(e => e.TotalServices).HasDefaultValue(0.00m);
        });

        modelBuilder.Entity<ServicesWithOrdersDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__services__3213E83F58CE4CF2");

            entity.HasOne(d => d.ServicesWithOrders).WithMany(p => p.ServicesWithOrdersDetails).HasConstraintName("FK_services_with_orders_details");
        });

        modelBuilder.Entity<Store>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__stores__3213E83FC79E3774");

            entity.Property(e => e.Active).HasDefaultValue(true);
        });

        modelBuilder.Entity<SuppliersCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__supplier__3213E83F010B3225");

            entity.Property(e => e.Active).HasDefaultValue(true);
        });

        modelBuilder.Entity<SuppliersWithOrder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__supplier__3213E83FAF991937");

            entity.Property(e => e.DiscountPercent).HasDefaultValue(0.00m);
            entity.Property(e => e.TaxPercent).HasDefaultValue(0.00m);
            entity.Property(e => e.TaxValue).HasDefaultValue(0.00m);
            entity.Property(e => e.TotalCost).HasDefaultValue(0.00m);
            entity.Property(e => e.WhatPaid).HasDefaultValue(0.00m);
            entity.Property(e => e.WhatRemain).HasDefaultValue(0.00m);
        });

        modelBuilder.Entity<SuppliersWithOrdersDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__supplier__3213E83F8E35E850");
        });

        modelBuilder.Entity<Suuplier>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__suuplier__3213E83FD3343D48");
        });

        modelBuilder.Entity<TreasuriesDelivery>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__treasuri__3213E83FE4FE8632");
        });

        modelBuilder.Entity<TreasuriesTransaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__treasuri__3213E83FD7422F4B");
        });

        modelBuilder.Entity<Treasury>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__treasuri__3213E83F20D06C66");

            entity.Property(e => e.Active).HasDefaultValue(true);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

}