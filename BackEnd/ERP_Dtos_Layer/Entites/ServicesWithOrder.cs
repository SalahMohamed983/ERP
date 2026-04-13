using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DominLayer.Entites;

[Table("services_with_orders")]
public partial class ServicesWithOrder
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("order_type")]
    public bool OrderType { get; set; }

    [Column("auto_serial")]
    public long AutoSerial { get; set; }

    [Column("order_date")]
    public DateOnly OrderDate { get; set; }

    [Column("is_approved")]
    public bool IsApproved { get; set; }

    [Column("com_code")]
    public int ComCode { get; set; }

    [Column("notes")]
    [StringLength(225)]
    public string? Notes { get; set; }

    [Column("total_services", TypeName = "decimal(10, 2)")]
    public decimal? TotalServices { get; set; }

    [Column("discount_type")]
    public bool? DiscountType { get; set; }

    [Column("discount_percent", TypeName = "decimal(10, 2)")]
    public decimal? DiscountPercent { get; set; }

    [Column("discount_value", TypeName = "decimal(10, 2)")]
    public decimal DiscountValue { get; set; }

    [Column("tax_percent", TypeName = "decimal(10, 2)")]
    public decimal? TaxPercent { get; set; }

    [Column("tax_value", TypeName = "decimal(10, 2)")]
    public decimal? TaxValue { get; set; }

    [Column("total_befor_discount", TypeName = "decimal(10, 2)")]
    public decimal TotalBeforDiscount { get; set; }

    [Column("total_cost", TypeName = "decimal(10, 2)")]
    public decimal? TotalCost { get; set; }

    [Column("is_account_number")]
    public bool IsAccountNumber { get; set; }

    [Column("entity_name")]
    [StringLength(150)]
    public string? EntityName { get; set; }

    [Column("account_number")]
    public long? AccountNumber { get; set; }

    [Column("money_for_account", TypeName = "decimal(10, 2)")]
    public decimal? MoneyForAccount { get; set; }

    [Column("pill_type")]
    public bool PillType { get; set; }

    [Column("what_paid", TypeName = "decimal(10, 2)")]
    public decimal WhatPaid { get; set; }

    [Column("what_remain", TypeName = "decimal(10, 2)")]
    public decimal WhatRemain { get; set; }

    [Column("treasuries_transactions_id")]
    public long? TreasuriesTransactionsId { get; set; }

    [Column("added_by")]
    public int AddedBy { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [Column("updated_by")]
    public int? UpdatedBy { get; set; }

    [Column("approved_by")]
    public int? ApprovedBy { get; set; }

    [InverseProperty("ServicesWithOrders")]
    public virtual ICollection<ServicesWithOrdersDetail> ServicesWithOrdersDetails { get; set; } = new List<ServicesWithOrdersDetail>();
}
