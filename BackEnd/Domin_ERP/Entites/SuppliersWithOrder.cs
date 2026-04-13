using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DominLayer.Entites;

[Table("suppliers_with_orders")]
public partial class SuppliersWithOrder
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("order_type")]
    public byte OrderType { get; set; }

    [Column("auto_serial")]
    public long AutoSerial { get; set; }

    [Column("DOC_NO")]
    [StringLength(25)]
    public string? DocNo { get; set; }

    [Column("order_date")]
    public DateOnly OrderDate { get; set; }

    [Column("suuplier_code")]
    public long SuuplierCode { get; set; }

    [Column("is_approved")]
    public bool IsApproved { get; set; }

    [Column("com_code")]
    public int ComCode { get; set; }

    [Column("notes")]
    [StringLength(225)]
    public string? Notes { get; set; }

    [Column("discount_type")]
    public bool? DiscountType { get; set; }

    [Column("discount_percent", TypeName = "decimal(10, 2)")]
    public decimal? DiscountPercent { get; set; }

    [Column("discount_value", TypeName = "decimal(10, 2)")]
    public decimal DiscountValue { get; set; }

    [Column("tax_percent", TypeName = "decimal(10, 2)")]
    public decimal? TaxPercent { get; set; }

    [Column("total_cost_items", TypeName = "decimal(10, 2)")]
    public decimal TotalCostItems { get; set; }

    [Column("tax_value", TypeName = "decimal(10, 2)")]
    public decimal? TaxValue { get; set; }

    [Column("total_befor_discount", TypeName = "decimal(10, 2)")]
    public decimal TotalBeforDiscount { get; set; }

    [Column("total_cost", TypeName = "decimal(10, 2)")]
    public decimal? TotalCost { get; set; }

    [Column("account_number")]
    public long AccountNumber { get; set; }

    [Column("money_for_account", TypeName = "decimal(10, 2)")]
    public decimal? MoneyForAccount { get; set; }

    [Column("pill_type")]
    public bool PillType { get; set; }

    [Column("what_paid", TypeName = "decimal(10, 2)")]
    public decimal? WhatPaid { get; set; }

    [Column("what_remain", TypeName = "decimal(10, 2)")]
    public decimal? WhatRemain { get; set; }

    [Column("treasuries_transactions_id")]
    public long? TreasuriesTransactionsId { get; set; }

    [Column("Supplier_balance_befor", TypeName = "decimal(10, 2)")]
    public decimal? SupplierBalanceBefor { get; set; }

    [Column("Supplier_balance_after", TypeName = "decimal(10, 2)")]
    public decimal? SupplierBalanceAfter { get; set; }

    [Column("added_by")]
    public int AddedBy { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [Column("updated_by")]
    public int? UpdatedBy { get; set; }

    [Column("store_id")]
    public long StoreId { get; set; }

    [Column("approved_by")]
    public int? ApprovedBy { get; set; }
}
