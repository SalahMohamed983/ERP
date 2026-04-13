using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DominLayer.Entites;

[Table("accounts")]
public partial class Account
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("name")]
    [StringLength(225)]
    public string Name { get; set; } = null!;

    [Column("account_type")]
    public int AccountType { get; set; }

    [Column("is_parent")]
    public bool IsParent { get; set; }

    [Column("parent_account_number")]
    public long? ParentAccountNumber { get; set; }

    [Column("account_number")]
    public long AccountNumber { get; set; }

    [Column("start_balance_status")]
    public byte StartBalanceStatus { get; set; }

    [Column("start_balance", TypeName = "decimal(10, 2)")]
    public decimal StartBalance { get; set; }

    [Column("current_balance", TypeName = "decimal(10, 2)")]
    public decimal CurrentBalance { get; set; }

    [Column("other_table_FK")]
    public long? OtherTableFk { get; set; }

    [Column("notes")]
    [StringLength(225)]
    public string? Notes { get; set; }

    [Column("added_by")]
    public int AddedBy { get; set; }

    [Column("updated_by")]
    public int? UpdatedBy { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [Column("active")]
    public bool Active { get; set; }

    [Column("com_code")]
    public int ComCode { get; set; }

    [Column("date")]
    public DateOnly Date { get; set; }
}
