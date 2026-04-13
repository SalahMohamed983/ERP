using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DominLayer.Entites;

[Table("delegates")]
public partial class Delegates
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("delegate_code")]
    public long DelegateCode { get; set; }

    [Column("name")]
    [StringLength(225)]
    public string Name { get; set; } = null!;

    [Column("account_number")]
    public long AccountNumber { get; set; }

    [Column("start_balance_status")]
    public byte StartBalanceStatus { get; set; }

    [Column("start_balance", TypeName = "decimal(10, 2)")]
    public decimal StartBalance { get; set; }

    [Column("current_balance", TypeName = "decimal(10, 2)")]
    public decimal CurrentBalance { get; set; }

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

    [Column("phones")]
    [StringLength(50)]
    public string? Phones { get; set; }

    [Column("address")]
    [StringLength(250)]
    public string? Address { get; set; }

    [Column("percent_type")]
    public byte PercentType { get; set; }

    [Column("percent_collect_commission", TypeName = "decimal(10, 2)")]
    public decimal PercentCollectCommission { get; set; }

    [Column("percent_salaes_commission_kataei", TypeName = "decimal(10, 2)")]
    public decimal PercentSalaesCommissionKataei { get; set; }

    [Column("percent_salaes_commission_nosjomla", TypeName = "decimal(10, 2)")]
    public decimal PercentSalaesCommissionNosjomla { get; set; }

    [Column("percent_salaes_commission_jomla", TypeName = "decimal(10, 2)")]
    public decimal PercentSalaesCommissionJomla { get; set; }
}
