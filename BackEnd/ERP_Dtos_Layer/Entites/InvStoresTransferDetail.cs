using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DominLayer.Entites;

[Table("inv_stores_transfer_details")]
public partial class InvStoresTransferDetail
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("inv_stores_transfer_id")]
    public long InvStoresTransferId { get; set; }

    [Column("inv_stores_transfer_auto_serial")]
    public long InvStoresTransferAutoSerial { get; set; }

    [Column("com_code")]
    public int ComCode { get; set; }

    [Column("deliverd_quantity", TypeName = "decimal(10, 2)")]
    public decimal DeliverdQuantity { get; set; }

    [Column("uom_id")]
    public int UomId { get; set; }

    [Column("isparentuom")]
    public bool Isparentuom { get; set; }

    [Column("unit_price", TypeName = "decimal(10, 2)")]
    public decimal UnitPrice { get; set; }

    [Column("total_price", TypeName = "decimal(10, 2)")]
    public decimal TotalPrice { get; set; }

    [Column("order_date")]
    public DateOnly OrderDate { get; set; }

    [Column("added_by")]
    public int AddedBy { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_by")]
    public int? UpdatedBy { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [Column("item_code")]
    public long ItemCode { get; set; }

    [Column("production_date")]
    public DateOnly? ProductionDate { get; set; }

    [Column("expire_date")]
    public DateOnly? ExpireDate { get; set; }

    [Column("item_card_type")]
    public bool ItemCardType { get; set; }

    [Column("transfer_from_batch_id")]
    public long TransferFromBatchId { get; set; }

    [Column("transfer_to_batch_id")]
    public long? TransferToBatchId { get; set; }

    [Column("is_approved")]
    public bool? IsApproved { get; set; }

    [Column("approved_by")]
    public int? ApprovedBy { get; set; }

    [Column("approved_at")]
    public DateTime? ApprovedAt { get; set; }

    [Column("is_canceld_receive")]
    public bool? IsCanceldReceive { get; set; }

    [Column("canceld_by")]
    public int? CanceldBy { get; set; }

    [Column("canceld_at")]
    public DateTime? CanceldAt { get; set; }

    [Column("canceld_cause")]
    [StringLength(300)]
    public string? CanceldCause { get; set; }

    [ForeignKey("InvStoresTransferId")]
    [InverseProperty("InvStoresTransferDetails")]
    public virtual InvStoresTransfer InvStoresTransfer { get; set; } = null!;
}
