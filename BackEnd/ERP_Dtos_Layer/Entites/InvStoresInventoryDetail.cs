using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DominLayer.Entites;

[Table("inv_stores_inventory_details")]
public partial class InvStoresInventoryDetail
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("inv_stores_inventory_id")]
    public long InvStoresInventoryId { get; set; }

    [Column("inv_stores_inventory_auto_serial")]
    public long InvStoresInventoryAutoSerial { get; set; }

    [Column("item_code")]
    public long ItemCode { get; set; }

    [Column("inv_uoms_id")]
    public int InvUomsId { get; set; }

    [Column("batch_auto_serial")]
    public long BatchAutoSerial { get; set; }

    [Column("old_quantity", TypeName = "decimal(10, 2)")]
    public decimal OldQuantity { get; set; }

    [Column("new_quantity", TypeName = "decimal(10, 2)")]
    public decimal NewQuantity { get; set; }

    [Column("diffrent_quantity", TypeName = "decimal(10, 2)")]
    public decimal DiffrentQuantity { get; set; }

    [Column("unit_cost_price", TypeName = "decimal(10, 2)")]
    public decimal UnitCostPrice { get; set; }

    [Column("total_cost_price", TypeName = "decimal(10, 2)")]
    public decimal TotalCostPrice { get; set; }

    [Column("production_date")]
    public DateOnly? ProductionDate { get; set; }

    [Column("expired_date")]
    public DateOnly? ExpiredDate { get; set; }

    [Column("notes")]
    [StringLength(225)]
    public string? Notes { get; set; }

    [Column("is_closed")]
    public bool IsClosed { get; set; }

    [Column("added_by")]
    public int AddedBy { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_by")]
    public int? UpdatedBy { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [Column("cloased_by")]
    public int? CloasedBy { get; set; }

    [Column("closed_at")]
    public DateTime? ClosedAt { get; set; }

    [Column("com_code")]
    public int ComCode { get; set; }

    [ForeignKey("InvStoresInventoryId")]
    [InverseProperty("InvStoresInventoryDetails")]
    public virtual InvStoresInventory InvStoresInventory { get; set; } = null!;
}
