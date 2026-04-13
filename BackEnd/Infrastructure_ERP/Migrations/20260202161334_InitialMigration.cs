using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfrastructureLayer.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "account_types",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    active = table.Column<byte>(type: "tinyint", nullable: false),
                    relatediternalaccounts = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__account___3213E83F97943C44", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "accounts",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(225)", maxLength: 225, nullable: false),
                    account_type = table.Column<int>(type: "int", nullable: false),
                    is_parent = table.Column<bool>(type: "bit", nullable: false),
                    parent_account_number = table.Column<long>(type: "bigint", nullable: true),
                    account_number = table.Column<long>(type: "bigint", nullable: false),
                    start_balance_status = table.Column<byte>(type: "tinyint", nullable: false),
                    start_balance = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    current_balance = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    other_table_FK = table.Column<long>(type: "bigint", nullable: true),
                    notes = table.Column<string>(type: "nvarchar(225)", maxLength: 225, nullable: true),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    com_code = table.Column<int>(type: "int", nullable: false),
                    date = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__accounts__3213E83F61A17648", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "admin_panel_settings",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    system_name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    photo = table.Column<string>(type: "nvarchar(225)", maxLength: 225, nullable: false),
                    active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    general_alert = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    address = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    phone = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    customer_parent_account_number = table.Column<long>(type: "bigint", nullable: false),
                    suppliers_parent_account_number = table.Column<long>(type: "bigint", nullable: false),
                    delegate_parent_account_number = table.Column<long>(type: "bigint", nullable: false),
                    employees_parent_account_number = table.Column<long>(type: "bigint", nullable: false),
                    production_lines_parent_account = table.Column<long>(type: "bigint", nullable: false),
                    added_by = table.Column<int>(type: "int", nullable: true),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    com_code = table.Column<int>(type: "int", nullable: false),
                    notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    is_set_Batches_setting = table.Column<bool>(type: "bit", nullable: false),
                    Batches_setting_type = table.Column<byte>(type: "tinyint", nullable: true),
                    default_unit = table.Column<byte>(type: "tinyint", nullable: false, defaultValue: (byte)1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__admin_pa__3213E83FA46A2087", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "admins",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    permission_roles_id = table.Column<int>(type: "int", nullable: false),
                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    password = table.Column<string>(type: "nvarchar(225)", maxLength: 225, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    added_by = table.Column<int>(type: "int", nullable: true),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    active = table.Column<bool>(type: "bit", nullable: false),
                    com_code = table.Column<int>(type: "int", nullable: false),
                    date = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__admins__3213E83F184D4D7F", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "admins_shifts",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    shift_code = table.Column<long>(type: "bigint", nullable: false),
                    admin_id = table.Column<int>(type: "int", nullable: false),
                    treasuries_id = table.Column<int>(type: "int", nullable: false),
                    treasuries_balnce_in_shift_start = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    start_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    end_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    is_finished = table.Column<bool>(type: "bit", nullable: false),
                    is_delivered_and_review = table.Column<bool>(type: "bit", nullable: false),
                    delivered_to_admin_id = table.Column<int>(type: "int", nullable: true),
                    delivered_to_admin_sift_id = table.Column<long>(type: "bigint", nullable: true),
                    delivered_to_treasuries_id = table.Column<int>(type: "int", nullable: true),
                    money_should_deviled = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    what_realy_delivered = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    money_state = table.Column<bool>(type: "bit", nullable: true),
                    money_state_value = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    receive_type = table.Column<bool>(type: "bit", nullable: true),
                    review_receive_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    treasuries_transactions_id = table.Column<long>(type: "bigint", nullable: true),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    notes = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    com_code = table.Column<int>(type: "int", nullable: false),
                    date = table.Column<DateOnly>(type: "date", nullable: false),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__admins_s__3213E83F9C84680D", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TokenVersion = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "customers",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    customer_code = table.Column<long>(type: "bigint", nullable: false),
                    name = table.Column<string>(type: "nvarchar(225)", maxLength: 225, nullable: false),
                    account_number = table.Column<long>(type: "bigint", nullable: false),
                    start_balance_status = table.Column<byte>(type: "tinyint", nullable: false),
                    start_balance = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    current_balance = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    notes = table.Column<string>(type: "nvarchar(225)", maxLength: 225, nullable: true),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    active = table.Column<bool>(type: "bit", nullable: false),
                    com_code = table.Column<int>(type: "int", nullable: false),
                    date = table.Column<DateOnly>(type: "date", nullable: false),
                    address = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    phones = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__customer__3213E83F15910068", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "delegates",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    delegate_code = table.Column<long>(type: "bigint", nullable: false),
                    name = table.Column<string>(type: "nvarchar(225)", maxLength: 225, nullable: false),
                    account_number = table.Column<long>(type: "bigint", nullable: false),
                    start_balance_status = table.Column<byte>(type: "tinyint", nullable: false),
                    start_balance = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    current_balance = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    notes = table.Column<string>(type: "nvarchar(225)", maxLength: 225, nullable: true),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    active = table.Column<bool>(type: "bit", nullable: false),
                    com_code = table.Column<int>(type: "int", nullable: false),
                    date = table.Column<DateOnly>(type: "date", nullable: false),
                    phones = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    address = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    percent_type = table.Column<byte>(type: "tinyint", nullable: false),
                    percent_collect_commission = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    percent_salaes_commission_kataei = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    percent_salaes_commission_nosjomla = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    percent_salaes_commission_jomla = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__delegate__3213E83FC106541B", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "inv_itemcard",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    item_code = table.Column<long>(type: "bigint", nullable: false),
                    barcode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    name = table.Column<string>(type: "nvarchar(225)", maxLength: 225, nullable: false),
                    item_type = table.Column<byte>(type: "tinyint", nullable: false),
                    inv_itemcard_categories_id = table.Column<int>(type: "int", nullable: false),
                    parent_inv_itemcard_id = table.Column<long>(type: "bigint", nullable: true),
                    does_has_retailunit = table.Column<bool>(type: "bit", nullable: false),
                    retail_uom_id = table.Column<int>(type: "int", nullable: true),
                    uom_id = table.Column<int>(type: "int", nullable: false),
                    retail_uom_quntToParent = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    date = table.Column<DateOnly>(type: "date", nullable: false),
                    com_code = table.Column<int>(type: "int", nullable: false),
                    price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    nos_gomla_price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    gomla_price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    price_retail = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    nos_gomla_price_retail = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    gomla_price_retail = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    cost_price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    cost_price_retail = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    has_fixced_price = table.Column<bool>(type: "bit", nullable: false),
                    All_QUENTITY = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    QUENTITY = table.Column<decimal>(type: "decimal(10,3)", nullable: true),
                    QUENTITY_Retail = table.Column<decimal>(type: "decimal(10,3)", nullable: true),
                    QUENTITY_all_Retails = table.Column<decimal>(type: "decimal(10,3)", nullable: true),
                    photo = table.Column<string>(type: "nvarchar(225)", maxLength: 225, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__inv_item__3213E83F59BDEE60", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "inv_itemcard_batches",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    store_id = table.Column<int>(type: "int", nullable: false),
                    item_code = table.Column<int>(type: "int", nullable: false),
                    inv_uoms_id = table.Column<int>(type: "int", nullable: false),
                    unit_cost_price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    quantity = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    total_cost_price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    production_date = table.Column<DateOnly>(type: "date", nullable: true),
                    expired_date = table.Column<DateOnly>(type: "date", nullable: true),
                    com_code = table.Column<int>(type: "int", nullable: false),
                    auto_serial = table.Column<long>(type: "bigint", nullable: false),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    is_send_to_archived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__inv_item__3213E83F4292501B", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "inv_itemcard_categories",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    com_code = table.Column<int>(type: "int", nullable: false),
                    date = table.Column<DateOnly>(type: "date", nullable: false),
                    active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__inv_item__3213E83F6E5DDDD8", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "inv_itemcard_movements",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    inv_itemcard_movements_categories = table.Column<int>(type: "int", nullable: false),
                    item_code = table.Column<long>(type: "bigint", nullable: false),
                    store_id = table.Column<int>(type: "int", nullable: false),
                    items_movements_types = table.Column<int>(type: "int", nullable: false),
                    FK_table = table.Column<long>(type: "bigint", nullable: false),
                    FK_table_details = table.Column<long>(type: "bigint", nullable: false),
                    byan = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    quantity_befor_movement = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    quantity_after_move = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    date = table.Column<DateOnly>(type: "date", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    com_code = table.Column<int>(type: "int", nullable: false),
                    quantity_befor_move_store = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    quantity_after_move_store = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__inv_item__3213E83FC0932887", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "inv_itemcard_movements_categories",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__inv_item__3213E83FB75097D7", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "inv_itemcard_movements_types",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__inv_item__3213E83F8AD577B9", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "inv_production_exchange",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    order_type = table.Column<byte>(type: "tinyint", nullable: false),
                    auto_serial = table.Column<long>(type: "bigint", nullable: false),
                    inv_production_order_auto_serial = table.Column<long>(type: "bigint", nullable: true),
                    order_date = table.Column<DateOnly>(type: "date", nullable: false),
                    production_lines_code = table.Column<long>(type: "bigint", nullable: false),
                    is_approved = table.Column<bool>(type: "bit", nullable: false),
                    com_code = table.Column<int>(type: "int", nullable: false),
                    notes = table.Column<string>(type: "nvarchar(225)", maxLength: 225, nullable: true),
                    discount_type = table.Column<byte>(type: "tinyint", nullable: true),
                    discount_percent = table.Column<decimal>(type: "decimal(10,2)", nullable: true, defaultValue: 0.00m),
                    discount_value = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    tax_percent = table.Column<decimal>(type: "decimal(10,2)", nullable: true, defaultValue: 0.00m),
                    total_cost_items = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    tax_value = table.Column<decimal>(type: "decimal(10,2)", nullable: true, defaultValue: 0.00m),
                    total_befor_discount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    total_cost = table.Column<decimal>(type: "decimal(10,2)", nullable: true, defaultValue: 0.00m),
                    account_number = table.Column<long>(type: "bigint", nullable: false),
                    money_for_account = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    pill_type = table.Column<byte>(type: "tinyint", nullable: false),
                    what_paid = table.Column<decimal>(type: "decimal(10,2)", nullable: true, defaultValue: 0.00m),
                    what_remain = table.Column<decimal>(type: "decimal(10,2)", nullable: true, defaultValue: 0.00m),
                    treasuries_transactions_id = table.Column<long>(type: "bigint", nullable: true),
                    Supplier_balance_befor = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Supplier_balance_after = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    store_id = table.Column<long>(type: "bigint", nullable: false),
                    approved_by = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__inv_prod__3213E83FCC638D8D", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "inv_production_lines",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    production_lines_code = table.Column<long>(type: "bigint", nullable: false),
                    name = table.Column<string>(type: "nvarchar(225)", maxLength: 225, nullable: false),
                    account_number = table.Column<long>(type: "bigint", nullable: false),
                    start_balance_status = table.Column<byte>(type: "tinyint", nullable: false),
                    start_balance = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    current_balance = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    notes = table.Column<string>(type: "nvarchar(225)", maxLength: 225, nullable: true),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    active = table.Column<bool>(type: "bit", nullable: false),
                    com_code = table.Column<int>(type: "int", nullable: false),
                    date = table.Column<DateOnly>(type: "date", nullable: false),
                    address = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    phones = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__inv_prod__3213E83F2E5635C1", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "inv_production_order",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    auto_serial = table.Column<long>(type: "bigint", nullable: false),
                    production_plane = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    production_plan_date = table.Column<DateOnly>(type: "date", nullable: false),
                    is_approved = table.Column<bool>(type: "bit", nullable: false),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    com_code = table.Column<int>(type: "int", nullable: false),
                    approved_by = table.Column<int>(type: "int", nullable: true),
                    approved_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    is_closed = table.Column<bool>(type: "bit", nullable: false),
                    closed_by = table.Column<int>(type: "int", nullable: true),
                    closed_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    date = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__inv_prod__3213E83FAC952FA2", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "inv_production_receive",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    order_type = table.Column<byte>(type: "tinyint", nullable: false),
                    auto_serial = table.Column<long>(type: "bigint", nullable: false),
                    inv_production_order_auto_serial = table.Column<long>(type: "bigint", nullable: true),
                    order_date = table.Column<DateOnly>(type: "date", nullable: false),
                    production_lines_code = table.Column<long>(type: "bigint", nullable: false),
                    is_approved = table.Column<bool>(type: "bit", nullable: false),
                    com_code = table.Column<int>(type: "int", nullable: false),
                    notes = table.Column<string>(type: "nvarchar(225)", maxLength: 225, nullable: true),
                    discount_type = table.Column<byte>(type: "tinyint", nullable: true),
                    discount_percent = table.Column<decimal>(type: "decimal(10,2)", nullable: true, defaultValue: 0.00m),
                    discount_value = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    tax_percent = table.Column<decimal>(type: "decimal(10,2)", nullable: true, defaultValue: 0.00m),
                    total_cost_items = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    tax_value = table.Column<decimal>(type: "decimal(10,2)", nullable: true, defaultValue: 0.00m),
                    total_befor_discount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    total_cost = table.Column<decimal>(type: "decimal(10,2)", nullable: true, defaultValue: 0.00m),
                    account_number = table.Column<long>(type: "bigint", nullable: false),
                    money_for_account = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    pill_type = table.Column<byte>(type: "tinyint", nullable: false),
                    what_paid = table.Column<decimal>(type: "decimal(10,2)", nullable: true, defaultValue: 0.00m),
                    what_remain = table.Column<decimal>(type: "decimal(10,2)", nullable: true, defaultValue: 0.00m),
                    treasuries_transactions_id = table.Column<long>(type: "bigint", nullable: true),
                    Supplier_balance_befor = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Supplier_balance_after = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    store_id = table.Column<long>(type: "bigint", nullable: false),
                    approved_by = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__inv_prod__3213E83FE92EDF46", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "inv_stores_inventory",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    store_id = table.Column<int>(type: "int", nullable: false),
                    inventory_date = table.Column<DateOnly>(type: "date", nullable: false),
                    inventory_type = table.Column<bool>(type: "bit", nullable: false),
                    auto_serial = table.Column<long>(type: "bigint", nullable: false),
                    is_closed = table.Column<bool>(type: "bit", nullable: false),
                    total_cost_batches = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    notes = table.Column<string>(type: "nvarchar(225)", maxLength: 225, nullable: true),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    date = table.Column<DateOnly>(type: "date", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    com_code = table.Column<int>(type: "int", nullable: false),
                    cloased_by = table.Column<int>(type: "int", nullable: true),
                    closed_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__inv_stor__3213E83F0614560D", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "inv_stores_transfer",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    auto_serial = table.Column<long>(type: "bigint", nullable: false),
                    transfer_from_store_id = table.Column<int>(type: "int", nullable: false),
                    transfer_to_store_id = table.Column<int>(type: "int", nullable: false),
                    order_date = table.Column<DateOnly>(type: "date", nullable: false),
                    is_approved = table.Column<bool>(type: "bit", nullable: false),
                    com_code = table.Column<int>(type: "int", nullable: false),
                    notes = table.Column<string>(type: "nvarchar(225)", maxLength: 225, nullable: true),
                    items_counter = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    total_cost_items = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    approved_by = table.Column<int>(type: "int", nullable: true),
                    approved_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__inv_stor__3213E83F76B7E085", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "inv_uoms",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    is_master = table.Column<bool>(type: "bit", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    com_code = table.Column<int>(type: "int", nullable: false),
                    date = table.Column<DateOnly>(type: "date", nullable: false),
                    active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__inv_uoms__3213E83F6C289799", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "mov_type",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    in_screen = table.Column<byte>(type: "tinyint", nullable: false),
                    is_private_internal = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__mov_type__3213E83FFF689CB0", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "personal_access_tokens",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tokenable_type = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    tokenable_id = table.Column<long>(type: "bigint", nullable: false),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    token = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    abilities = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    last_used_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    expires_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__personal__3213E83F3351379E", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "sales_invoices",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sales_matrial_types = table.Column<int>(type: "int", nullable: true),
                    auto_serial = table.Column<long>(type: "bigint", nullable: false),
                    invoice_date = table.Column<DateOnly>(type: "date", nullable: false),
                    is_has_customer = table.Column<bool>(type: "bit", nullable: false),
                    customer_code = table.Column<long>(type: "bigint", nullable: true),
                    delegate_code = table.Column<long>(type: "bigint", nullable: true),
                    delegate_commission_percent_type = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    delegate_commission_percent = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    delegate_commission_value = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    is_approved = table.Column<bool>(type: "bit", nullable: false),
                    com_code = table.Column<int>(type: "int", nullable: false),
                    notes = table.Column<string>(type: "nvarchar(225)", maxLength: 225, nullable: true),
                    discount_type = table.Column<bool>(type: "bit", nullable: true),
                    discount_percent = table.Column<decimal>(type: "decimal(10,2)", nullable: true, defaultValue: 0.00m),
                    discount_value = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    tax_percent = table.Column<decimal>(type: "decimal(10,2)", nullable: true, defaultValue: 0.00m),
                    total_cost_items = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    tax_value = table.Column<decimal>(type: "decimal(10,2)", nullable: true, defaultValue: 0.00m),
                    total_befor_discount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    total_cost = table.Column<decimal>(type: "decimal(10,2)", nullable: true, defaultValue: 0.00m),
                    account_number = table.Column<long>(type: "bigint", nullable: true),
                    money_for_account = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    pill_type = table.Column<bool>(type: "bit", nullable: true),
                    what_paid = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    what_remain = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    treasuries_transactions_id = table.Column<long>(type: "bigint", nullable: true),
                    customer_balance_befor = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    customer_balance_after = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    approved_by = table.Column<int>(type: "int", nullable: true),
                    date = table.Column<DateOnly>(type: "date", nullable: false),
                    sales_item_type = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__sales_in__3213E83FE0BE8227", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "sales_invoices_return",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    return_type = table.Column<bool>(type: "bit", nullable: false),
                    sales_matrial_types = table.Column<int>(type: "int", nullable: true),
                    auto_serial = table.Column<long>(type: "bigint", nullable: false),
                    invoice_date = table.Column<DateOnly>(type: "date", nullable: false),
                    is_has_customer = table.Column<bool>(type: "bit", nullable: false),
                    customer_code = table.Column<long>(type: "bigint", nullable: true),
                    delegate_code = table.Column<long>(type: "bigint", nullable: true),
                    is_approved = table.Column<bool>(type: "bit", nullable: false),
                    com_code = table.Column<int>(type: "int", nullable: false),
                    notes = table.Column<string>(type: "nvarchar(225)", maxLength: 225, nullable: true),
                    discount_type = table.Column<bool>(type: "bit", nullable: true),
                    discount_percent = table.Column<decimal>(type: "decimal(10,2)", nullable: true, defaultValue: 0.00m),
                    discount_value = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    tax_percent = table.Column<decimal>(type: "decimal(10,2)", nullable: true, defaultValue: 0.00m),
                    total_cost_items = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    tax_value = table.Column<decimal>(type: "decimal(10,2)", nullable: true, defaultValue: 0.00m),
                    total_befor_discount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    total_cost = table.Column<decimal>(type: "decimal(10,2)", nullable: true, defaultValue: 0.00m),
                    account_number = table.Column<long>(type: "bigint", nullable: true),
                    money_for_account = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    pill_type = table.Column<bool>(type: "bit", nullable: true),
                    what_paid = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    what_remain = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    treasuries_transactions_id = table.Column<long>(type: "bigint", nullable: true),
                    customer_balance_befor = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    customer_balance_after = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    approved_by = table.Column<int>(type: "int", nullable: true),
                    date = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__sales_in__3213E83F4748605F", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "sales_matrial_types",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    com_code = table.Column<int>(type: "int", nullable: false),
                    date = table.Column<DateOnly>(type: "date", nullable: false),
                    active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__sales_ma__3213E83F003FF30A", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "services",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(225)", maxLength: 225, nullable: false),
                    active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    type = table.Column<bool>(type: "bit", nullable: false),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    com_code = table.Column<int>(type: "int", nullable: false),
                    date = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__services__3213E83F6C4DE484", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "services_with_orders",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    order_type = table.Column<bool>(type: "bit", nullable: false),
                    auto_serial = table.Column<long>(type: "bigint", nullable: false),
                    order_date = table.Column<DateOnly>(type: "date", nullable: false),
                    is_approved = table.Column<bool>(type: "bit", nullable: false),
                    com_code = table.Column<int>(type: "int", nullable: false),
                    notes = table.Column<string>(type: "nvarchar(225)", maxLength: 225, nullable: true),
                    total_services = table.Column<decimal>(type: "decimal(10,2)", nullable: true, defaultValue: 0.00m),
                    discount_type = table.Column<bool>(type: "bit", nullable: true),
                    discount_percent = table.Column<decimal>(type: "decimal(10,2)", nullable: true, defaultValue: 0.00m),
                    discount_value = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    tax_percent = table.Column<decimal>(type: "decimal(10,2)", nullable: true, defaultValue: 0.00m),
                    tax_value = table.Column<decimal>(type: "decimal(10,2)", nullable: true, defaultValue: 0.00m),
                    total_befor_discount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    total_cost = table.Column<decimal>(type: "decimal(10,2)", nullable: true, defaultValue: 0.00m),
                    is_account_number = table.Column<bool>(type: "bit", nullable: false),
                    entity_name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    account_number = table.Column<long>(type: "bigint", nullable: true),
                    money_for_account = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    pill_type = table.Column<bool>(type: "bit", nullable: false),
                    what_paid = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    what_remain = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    treasuries_transactions_id = table.Column<long>(type: "bigint", nullable: true),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    approved_by = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__services__3213E83FCC2F0101", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "stores",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    phones = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    address = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    com_code = table.Column<int>(type: "int", nullable: false),
                    date = table.Column<DateOnly>(type: "date", nullable: false),
                    active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__stores__3213E83FC79E3774", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "suppliers_categories",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    com_code = table.Column<int>(type: "int", nullable: false),
                    date = table.Column<DateOnly>(type: "date", nullable: false),
                    active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__supplier__3213E83F010B3225", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "suppliers_with_orders",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    order_type = table.Column<byte>(type: "tinyint", nullable: false),
                    auto_serial = table.Column<long>(type: "bigint", nullable: false),
                    DOC_NO = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    order_date = table.Column<DateOnly>(type: "date", nullable: false),
                    suuplier_code = table.Column<long>(type: "bigint", nullable: false),
                    is_approved = table.Column<bool>(type: "bit", nullable: false),
                    com_code = table.Column<int>(type: "int", nullable: false),
                    notes = table.Column<string>(type: "nvarchar(225)", maxLength: 225, nullable: true),
                    discount_type = table.Column<bool>(type: "bit", nullable: true),
                    discount_percent = table.Column<decimal>(type: "decimal(10,2)", nullable: true, defaultValue: 0.00m),
                    discount_value = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    tax_percent = table.Column<decimal>(type: "decimal(10,2)", nullable: true, defaultValue: 0.00m),
                    total_cost_items = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    tax_value = table.Column<decimal>(type: "decimal(10,2)", nullable: true, defaultValue: 0.00m),
                    total_befor_discount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    total_cost = table.Column<decimal>(type: "decimal(10,2)", nullable: true, defaultValue: 0.00m),
                    account_number = table.Column<long>(type: "bigint", nullable: false),
                    money_for_account = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    pill_type = table.Column<bool>(type: "bit", nullable: false),
                    what_paid = table.Column<decimal>(type: "decimal(10,2)", nullable: true, defaultValue: 0.00m),
                    what_remain = table.Column<decimal>(type: "decimal(10,2)", nullable: true, defaultValue: 0.00m),
                    treasuries_transactions_id = table.Column<long>(type: "bigint", nullable: true),
                    Supplier_balance_befor = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Supplier_balance_after = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    store_id = table.Column<long>(type: "bigint", nullable: false),
                    approved_by = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__supplier__3213E83FAF991937", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "suppliers_with_orders_details",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    suppliers_with_order_id = table.Column<long>(type: "bigint", nullable: false),
                    suppliers_with_orders_auto_serial = table.Column<long>(type: "bigint", nullable: false),
                    order_type = table.Column<bool>(type: "bit", nullable: false),
                    com_code = table.Column<int>(type: "int", nullable: false),
                    deliverd_quantity = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    uom_id = table.Column<int>(type: "int", nullable: false),
                    isparentuom = table.Column<bool>(type: "bit", nullable: false),
                    unit_price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    total_price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    order_date = table.Column<DateOnly>(type: "date", nullable: false),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    item_code = table.Column<long>(type: "bigint", nullable: false),
                    batch_auto_serial = table.Column<long>(type: "bigint", nullable: true),
                    production_date = table.Column<DateOnly>(type: "date", nullable: true),
                    expire_date = table.Column<DateOnly>(type: "date", nullable: true),
                    item_card_type = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__supplier__3213E83F8E35E850", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "suupliers",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    suuplier_code = table.Column<long>(type: "bigint", nullable: false),
                    suppliers_categories_id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(225)", maxLength: 225, nullable: false),
                    account_number = table.Column<long>(type: "bigint", nullable: false),
                    start_balance_status = table.Column<byte>(type: "tinyint", nullable: false),
                    start_balance = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    current_balance = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    notes = table.Column<string>(type: "nvarchar(225)", maxLength: 225, nullable: true),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    active = table.Column<bool>(type: "bit", nullable: false),
                    com_code = table.Column<int>(type: "int", nullable: false),
                    date = table.Column<DateOnly>(type: "date", nullable: false),
                    address = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    phones = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__suuplier__3213E83FD3343D48", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "treasuries",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    is_master = table.Column<bool>(type: "bit", nullable: false),
                    last_isal_exhcange = table.Column<long>(type: "bigint", nullable: false),
                    last_isal_collect = table.Column<long>(type: "bigint", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    com_code = table.Column<int>(type: "int", nullable: false),
                    date = table.Column<DateOnly>(type: "date", nullable: false),
                    active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__treasuri__3213E83F20D06C66", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "treasuries_delivery",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    treasuries_id = table.Column<int>(type: "int", nullable: false),
                    treasuries_can_delivery_id = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    com_code = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__treasuri__3213E83FE4FE8632", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "treasuries_transactions",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    auto_serial = table.Column<long>(type: "bigint", nullable: false),
                    isal_number = table.Column<long>(type: "bigint", nullable: false),
                    shift_code = table.Column<long>(type: "bigint", nullable: false),
                    money = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    treasuries_id = table.Column<int>(type: "int", nullable: false),
                    is_approved = table.Column<bool>(type: "bit", nullable: false),
                    mov_type = table.Column<int>(type: "int", nullable: false),
                    move_date = table.Column<DateOnly>(type: "date", nullable: false),
                    the_foregin_key = table.Column<long>(type: "bigint", nullable: true),
                    account_number = table.Column<long>(type: "bigint", nullable: true),
                    is_account = table.Column<bool>(type: "bit", nullable: true),
                    money_for_account = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    byan = table.Column<string>(type: "nvarchar(225)", maxLength: 225, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    com_code = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__treasuri__3213E83FD7422F4B", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TokenHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JwtId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpiresOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RevokedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReplacedByTokenId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedByIp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RevokedByIp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserAgent = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "inv_production_exchange_details",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    inv_production_exchange_id = table.Column<long>(type: "bigint", nullable: false),
                    inv_production_exchange_auto_serial = table.Column<long>(type: "bigint", nullable: false),
                    order_type = table.Column<byte>(type: "tinyint", nullable: false),
                    com_code = table.Column<int>(type: "int", nullable: false),
                    deliverd_quantity = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    uom_id = table.Column<int>(type: "int", nullable: false),
                    isparentuom = table.Column<bool>(type: "bit", nullable: false),
                    unit_price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    total_price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    order_date = table.Column<DateOnly>(type: "date", nullable: false),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    item_code = table.Column<long>(type: "bigint", nullable: false),
                    batch_auto_serial = table.Column<long>(type: "bigint", nullable: true),
                    production_date = table.Column<DateOnly>(type: "date", nullable: true),
                    expire_date = table.Column<DateOnly>(type: "date", nullable: true),
                    item_card_type = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__inv_prod__3213E83F094CB62C", x => x.id);
                    table.ForeignKey(
                        name: "FK_inv_production_exchange_details",
                        column: x => x.inv_production_exchange_id,
                        principalTable: "inv_production_exchange",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "inv_production_receive_details",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    inv_production_receive_id = table.Column<long>(type: "bigint", nullable: false),
                    inv_production_receive_auto_serial = table.Column<long>(type: "bigint", nullable: false),
                    order_type = table.Column<bool>(type: "bit", nullable: false),
                    com_code = table.Column<int>(type: "int", nullable: false),
                    deliverd_quantity = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    uom_id = table.Column<int>(type: "int", nullable: false),
                    isparentuom = table.Column<bool>(type: "bit", nullable: false),
                    unit_price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    total_price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    order_date = table.Column<DateOnly>(type: "date", nullable: false),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    item_code = table.Column<long>(type: "bigint", nullable: false),
                    batch_auto_serial = table.Column<long>(type: "bigint", nullable: true),
                    production_date = table.Column<DateOnly>(type: "date", nullable: true),
                    expire_date = table.Column<DateOnly>(type: "date", nullable: true),
                    item_card_type = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__inv_prod__3213E83F6FA5029A", x => x.id);
                    table.ForeignKey(
                        name: "FK_inv_production_receive_details",
                        column: x => x.inv_production_receive_id,
                        principalTable: "inv_production_receive",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "inv_stores_inventory_details",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    inv_stores_inventory_id = table.Column<long>(type: "bigint", nullable: false),
                    inv_stores_inventory_auto_serial = table.Column<long>(type: "bigint", nullable: false),
                    item_code = table.Column<long>(type: "bigint", nullable: false),
                    inv_uoms_id = table.Column<int>(type: "int", nullable: false),
                    batch_auto_serial = table.Column<long>(type: "bigint", nullable: false),
                    old_quantity = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    new_quantity = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    diffrent_quantity = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    unit_cost_price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    total_cost_price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    production_date = table.Column<DateOnly>(type: "date", nullable: true),
                    expired_date = table.Column<DateOnly>(type: "date", nullable: true),
                    notes = table.Column<string>(type: "nvarchar(225)", maxLength: 225, nullable: true),
                    is_closed = table.Column<bool>(type: "bit", nullable: false),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    cloased_by = table.Column<int>(type: "int", nullable: true),
                    closed_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    com_code = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__inv_stor__3213E83F680AD558", x => x.id);
                    table.ForeignKey(
                        name: "FK_inv_stores_inventory_details",
                        column: x => x.inv_stores_inventory_id,
                        principalTable: "inv_stores_inventory",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "inv_stores_transfer_details",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    inv_stores_transfer_id = table.Column<long>(type: "bigint", nullable: false),
                    inv_stores_transfer_auto_serial = table.Column<long>(type: "bigint", nullable: false),
                    com_code = table.Column<int>(type: "int", nullable: false),
                    deliverd_quantity = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    uom_id = table.Column<int>(type: "int", nullable: false),
                    isparentuom = table.Column<bool>(type: "bit", nullable: false),
                    unit_price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    total_price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    order_date = table.Column<DateOnly>(type: "date", nullable: false),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    item_code = table.Column<long>(type: "bigint", nullable: false),
                    production_date = table.Column<DateOnly>(type: "date", nullable: true),
                    expire_date = table.Column<DateOnly>(type: "date", nullable: true),
                    item_card_type = table.Column<bool>(type: "bit", nullable: false),
                    transfer_from_batch_id = table.Column<long>(type: "bigint", nullable: false),
                    transfer_to_batch_id = table.Column<long>(type: "bigint", nullable: true),
                    is_approved = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    approved_by = table.Column<int>(type: "int", nullable: true),
                    approved_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    is_canceld_receive = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    canceld_by = table.Column<int>(type: "int", nullable: true),
                    canceld_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    canceld_cause = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__inv_stor__3213E83F68A41098", x => x.id);
                    table.ForeignKey(
                        name: "FK_inv_stores_transfer_details",
                        column: x => x.inv_stores_transfer_id,
                        principalTable: "inv_stores_transfer",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "RolePermissions",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PermissionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissions", x => new { x.RoleId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_RolePermissions_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sales_invoices_details",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sales_invoices_id = table.Column<long>(type: "bigint", nullable: false),
                    sales_invoices_auto_serial = table.Column<long>(type: "bigint", nullable: false),
                    store_id = table.Column<int>(type: "int", nullable: false),
                    sales_item_type = table.Column<bool>(type: "bit", nullable: false),
                    item_code = table.Column<long>(type: "bigint", nullable: false),
                    uom_id = table.Column<int>(type: "int", nullable: false),
                    batch_auto_serial = table.Column<long>(type: "bigint", nullable: true),
                    quantity = table.Column<decimal>(type: "decimal(10,4)", nullable: false),
                    unit_price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    total_price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    is_normal_orOther = table.Column<bool>(type: "bit", nullable: false),
                    isparentuom = table.Column<bool>(type: "bit", nullable: false),
                    com_code = table.Column<int>(type: "int", nullable: false),
                    invoice_date = table.Column<DateOnly>(type: "date", nullable: false),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    production_date = table.Column<DateOnly>(type: "date", nullable: true),
                    expire_date = table.Column<DateOnly>(type: "date", nullable: true),
                    date = table.Column<DateOnly>(type: "date", nullable: false),
                    itemCostPriceFromBatch = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    taoalitemCostPriceFromBatch = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    item_total_earnings = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__sales_in__3213E83FFE58D7DE", x => x.id);
                    table.ForeignKey(
                        name: "FK_sales_invoices_details",
                        column: x => x.sales_invoices_id,
                        principalTable: "sales_invoices",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sales_invoices_return_details",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sales_invoices_return_id = table.Column<long>(type: "bigint", nullable: false),
                    sales_invoices_auto_serial = table.Column<long>(type: "bigint", nullable: false),
                    store_id = table.Column<int>(type: "int", nullable: false),
                    sales_item_type = table.Column<bool>(type: "bit", nullable: false),
                    item_code = table.Column<long>(type: "bigint", nullable: false),
                    uom_id = table.Column<int>(type: "int", nullable: false),
                    batch_auto_serial = table.Column<long>(type: "bigint", nullable: true),
                    quantity = table.Column<decimal>(type: "decimal(10,4)", nullable: false),
                    unit_cost_price = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    unit_price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    total_price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    is_normal_orOther = table.Column<bool>(type: "bit", nullable: false),
                    isparentuom = table.Column<bool>(type: "bit", nullable: false),
                    com_code = table.Column<int>(type: "int", nullable: false),
                    invoice_date = table.Column<DateOnly>(type: "date", nullable: false),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    production_date = table.Column<DateOnly>(type: "date", nullable: true),
                    expire_date = table.Column<DateOnly>(type: "date", nullable: true),
                    date = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__sales_in__3213E83F3CFAE5A2", x => x.id);
                    table.ForeignKey(
                        name: "FK_sales_invoices_return_details",
                        column: x => x.sales_invoices_return_id,
                        principalTable: "sales_invoices_return",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "services_with_orders_details",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    services_with_orders_id = table.Column<long>(type: "bigint", nullable: false),
                    services_with_orders_auto_serial = table.Column<long>(type: "bigint", nullable: false),
                    order_type = table.Column<bool>(type: "bit", nullable: false),
                    service_id = table.Column<int>(type: "int", nullable: false),
                    notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    total = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    com_code = table.Column<int>(type: "int", nullable: false),
                    date = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__services__3213E83F58CE4CF2", x => x.id);
                    table.ForeignKey(
                        name: "FK_services_with_orders_details",
                        column: x => x.services_with_orders_id,
                        principalTable: "services_with_orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "admins_stores",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    admin_id = table.Column<int>(type: "int", nullable: false),
                    store_id = table.Column<int>(type: "int", nullable: false),
                    active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    com_code = table.Column<int>(type: "int", nullable: false),
                    date = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__admins_s__3213E83F2B88BC8F", x => x.id);
                    table.ForeignKey(
                        name: "FK_admins_stores_admin_id",
                        column: x => x.admin_id,
                        principalTable: "admins",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_admins_stores_store_id",
                        column: x => x.store_id,
                        principalTable: "stores",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "admins_treasuries",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    admin_id = table.Column<int>(type: "int", nullable: false),
                    treasuries_id = table.Column<int>(type: "int", nullable: false),
                    active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    com_code = table.Column<int>(type: "int", nullable: false),
                    date = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__admins_t__3213E83F3039E8F6", x => x.id);
                    table.ForeignKey(
                        name: "FK_admins_treasuries_admin_id",
                        column: x => x.admin_id,
                        principalTable: "admins",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_admins_treasuries_treasuries",
                        column: x => x.treasuries_id,
                        principalTable: "treasuries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_admins_stores_admin_id",
                table: "admins_stores",
                column: "admin_id");

            migrationBuilder.CreateIndex(
                name: "IX_admins_stores_store_id",
                table: "admins_stores",
                column: "store_id");

            migrationBuilder.CreateIndex(
                name: "IX_admins_treasuries_admin_id",
                table: "admins_treasuries",
                column: "admin_id");

            migrationBuilder.CreateIndex(
                name: "IX_admins_treasuries_treasuries_id",
                table: "admins_treasuries",
                column: "treasuries_id");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_inv_production_exchange_details_inv_production_exchange_id",
                table: "inv_production_exchange_details",
                column: "inv_production_exchange_id");

            migrationBuilder.CreateIndex(
                name: "IX_inv_production_receive_details_inv_production_receive_id",
                table: "inv_production_receive_details",
                column: "inv_production_receive_id");

            migrationBuilder.CreateIndex(
                name: "IX_inv_stores_inventory_details_inv_stores_inventory_id",
                table: "inv_stores_inventory_details",
                column: "inv_stores_inventory_id");

            migrationBuilder.CreateIndex(
                name: "IX_inv_stores_transfer_details_inv_stores_transfer_id",
                table: "inv_stores_transfer_details",
                column: "inv_stores_transfer_id");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_PermissionId",
                table: "RolePermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_sales_invoices_details_sales_invoices_id",
                table: "sales_invoices_details",
                column: "sales_invoices_id");

            migrationBuilder.CreateIndex(
                name: "IX_sales_invoices_return_details_sales_invoices_return_id",
                table: "sales_invoices_return_details",
                column: "sales_invoices_return_id");

            migrationBuilder.CreateIndex(
                name: "IX_services_with_orders_details_services_with_orders_id",
                table: "services_with_orders_details",
                column: "services_with_orders_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "account_types");

            migrationBuilder.DropTable(
                name: "accounts");

            migrationBuilder.DropTable(
                name: "admin_panel_settings");

            migrationBuilder.DropTable(
                name: "admins_shifts");

            migrationBuilder.DropTable(
                name: "admins_stores");

            migrationBuilder.DropTable(
                name: "admins_treasuries");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "customers");

            migrationBuilder.DropTable(
                name: "delegates");

            migrationBuilder.DropTable(
                name: "inv_itemcard");

            migrationBuilder.DropTable(
                name: "inv_itemcard_batches");

            migrationBuilder.DropTable(
                name: "inv_itemcard_categories");

            migrationBuilder.DropTable(
                name: "inv_itemcard_movements");

            migrationBuilder.DropTable(
                name: "inv_itemcard_movements_categories");

            migrationBuilder.DropTable(
                name: "inv_itemcard_movements_types");

            migrationBuilder.DropTable(
                name: "inv_production_exchange_details");

            migrationBuilder.DropTable(
                name: "inv_production_lines");

            migrationBuilder.DropTable(
                name: "inv_production_order");

            migrationBuilder.DropTable(
                name: "inv_production_receive_details");

            migrationBuilder.DropTable(
                name: "inv_stores_inventory_details");

            migrationBuilder.DropTable(
                name: "inv_stores_transfer_details");

            migrationBuilder.DropTable(
                name: "inv_uoms");

            migrationBuilder.DropTable(
                name: "mov_type");

            migrationBuilder.DropTable(
                name: "personal_access_tokens");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "RolePermissions");

            migrationBuilder.DropTable(
                name: "sales_invoices_details");

            migrationBuilder.DropTable(
                name: "sales_invoices_return_details");

            migrationBuilder.DropTable(
                name: "sales_matrial_types");

            migrationBuilder.DropTable(
                name: "services");

            migrationBuilder.DropTable(
                name: "services_with_orders_details");

            migrationBuilder.DropTable(
                name: "suppliers_categories");

            migrationBuilder.DropTable(
                name: "suppliers_with_orders");

            migrationBuilder.DropTable(
                name: "suppliers_with_orders_details");

            migrationBuilder.DropTable(
                name: "suupliers");

            migrationBuilder.DropTable(
                name: "treasuries_delivery");

            migrationBuilder.DropTable(
                name: "treasuries_transactions");

            migrationBuilder.DropTable(
                name: "stores");

            migrationBuilder.DropTable(
                name: "admins");

            migrationBuilder.DropTable(
                name: "treasuries");

            migrationBuilder.DropTable(
                name: "inv_production_exchange");

            migrationBuilder.DropTable(
                name: "inv_production_receive");

            migrationBuilder.DropTable(
                name: "inv_stores_inventory");

            migrationBuilder.DropTable(
                name: "inv_stores_transfer");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "sales_invoices");

            migrationBuilder.DropTable(
                name: "sales_invoices_return");

            migrationBuilder.DropTable(
                name: "services_with_orders");
        }
    }
}
