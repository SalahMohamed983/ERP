using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DominLayer.Entites;

[Table("inv_stores_transfer")]
public partial class InvStoresTransfer
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("auto_serial")]
    public long AutoSerial { get; set; }

    [Column("transfer_from_store_id")]
    public int TransferFromStoreId { get; set; }

    [Column("transfer_to_store_id")]
    public int TransferToStoreId { get; set; }

    [Column("order_date")]
    public DateOnly OrderDate { get; set; }

    [Column("is_approved")]
    public bool IsApproved { get; set; }

    [Column("com_code")]
    public int ComCode { get; set; }

    [Column("notes")]
    [StringLength(225)]
    public string? Notes { get; set; }

    [Column("items_counter", TypeName = "decimal(10, 2)")]
    public decimal ItemsCounter { get; set; }

    [Column("total_cost_items", TypeName = "decimal(10, 2)")]
    public decimal TotalCostItems { get; set; }

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

    [Column("approved_at")]
    public DateTime? ApprovedAt { get; set; }

    [InverseProperty("InvStoresTransfer")]
    public virtual ICollection<InvStoresTransferDetail> InvStoresTransferDetails { get; set; } = new List<InvStoresTransferDetail>();
}
