using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DominLayer.Entites;

[Table("inv_itemcard_batches")]
public partial class InvItemcardBatch
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("store_id")]
    public int StoreId { get; set; }

    [Column("item_code")]
    public int ItemCode { get; set; }

    [Column("inv_uoms_id")]
    public int InvUomsId { get; set; }

    [Column("unit_cost_price", TypeName = "decimal(10, 2)")]
    public decimal UnitCostPrice { get; set; }

    [Column("quantity", TypeName = "decimal(10, 2)")]
    public decimal Quantity { get; set; }

    [Column("total_cost_price", TypeName = "decimal(10, 2)")]
    public decimal TotalCostPrice { get; set; }

    [Column("production_date")]
    public DateOnly? ProductionDate { get; set; }

    [Column("expired_date")]
    public DateOnly? ExpiredDate { get; set; }

    [Column("com_code")]
    public int ComCode { get; set; }

    [Column("auto_serial")]
    public long AutoSerial { get; set; }

    [Column("added_by")]
    public int AddedBy { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [Column("updated_by")]
    public int? UpdatedBy { get; set; }

    [Column("is_send_to_archived")]
    public bool IsSendToArchived { get; set; }
}
