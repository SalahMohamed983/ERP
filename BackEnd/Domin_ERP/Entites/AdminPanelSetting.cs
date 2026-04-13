using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DominLayer.Entites;

[Table("admin_panel_settings")]
public partial class AdminPanelSetting
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("system_name")]
    [StringLength(250)]
    public string SystemName { get; set; } = null!;

    [Column("photo")]
    [StringLength(225)]
    public string Photo { get; set; } = null!;

    [Column("active")]
    public bool Active { get; set; }

    [Column("general_alert")]
    [StringLength(150)]
    public string? GeneralAlert { get; set; }

    [Column("address")]
    [StringLength(250)]
    public string Address { get; set; } = null!;

    [Column("phone")]
    [StringLength(100)]
    public string? Phone { get; set; }

    [Column("customer_parent_account_number")]
    public long CustomerParentAccountNumber { get; set; }

    [Column("suppliers_parent_account_number")]
    public long SuppliersParentAccountNumber { get; set; }

    [Column("delegate_parent_account_number")]
    public long DelegateParentAccountNumber { get; set; }

    [Column("employees_parent_account_number")]
    public long EmployeesParentAccountNumber { get; set; }

    [Column("production_lines_parent_account")]
    public long ProductionLinesParentAccount { get; set; }

    [Column("added_by")]
    public int? AddedBy { get; set; }

    [Column("updated_by")]
    public int? UpdatedBy { get; set; }

    [Column("created_at")]
    public DateTime? CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [Column("com_code")]
    public int ComCode { get; set; }

    [Column("notes")]
    public string? Notes { get; set; }

    [Column("is_set_Batches_setting")]
    public bool IsSetBatchesSetting { get; set; }

    [Column("Batches_setting_type")]
    public byte? BatchesSettingType { get; set; }

    [Column("default_unit")]
    public byte DefaultUnit { get; set; }
}
