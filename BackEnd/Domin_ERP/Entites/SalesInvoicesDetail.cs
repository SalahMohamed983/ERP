using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DominLayer.Entites;

[Table("sales_invoices_details")]
public partial class SalesInvoicesDetail
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("sales_invoices_id")]
    public long SalesInvoicesId { get; set; }

    [Column("sales_invoices_auto_serial")]
    public long SalesInvoicesAutoSerial { get; set; }

    [Column("store_id")]
    public int StoreId { get; set; }

    [Column("sales_item_type")]
    public bool SalesItemType { get; set; }

    [Column("item_code")]
    public long ItemCode { get; set; }

    [Column("uom_id")]
    public int UomId { get; set; }

    [Column("batch_auto_serial")]
    public long? BatchAutoSerial { get; set; }

    [Column("quantity", TypeName = "decimal(10, 4)")]
    public decimal Quantity { get; set; }

    [Column("unit_price", TypeName = "decimal(10, 2)")]
    public decimal UnitPrice { get; set; }

    [Column("total_price", TypeName = "decimal(10, 2)")]
    public decimal TotalPrice { get; set; }

    [Column("is_normal_orOther")]
    public bool IsNormalOrOther { get; set; }

    [Column("isparentuom")]
    public bool Isparentuom { get; set; }

    [Column("com_code")]
    public int ComCode { get; set; }

    [Column("invoice_date")]
    public DateOnly InvoiceDate { get; set; }

    [Column("added_by")]
    public int AddedBy { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_by")]
    public int? UpdatedBy { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [Column("production_date")]
    public DateOnly? ProductionDate { get; set; }

    [Column("expire_date")]
    public DateOnly? ExpireDate { get; set; }

    [Column("date")]
    public DateOnly Date { get; set; }

    [Column("itemCostPriceFromBatch", TypeName = "decimal(10, 2)")]
    public decimal ItemCostPriceFromBatch { get; set; }

    [Column("taoalitemCostPriceFromBatch", TypeName = "decimal(10, 2)")]
    public decimal TaoalitemCostPriceFromBatch { get; set; }

    [Column("item_total_earnings", TypeName = "decimal(10, 2)")]
    public decimal ItemTotalEarnings { get; set; }

    [ForeignKey("SalesInvoicesId")]
    [InverseProperty("SalesInvoicesDetails")]
    public virtual SalesInvoice SalesInvoices { get; set; } = null!;
}
