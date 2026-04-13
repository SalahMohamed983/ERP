using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DominLayer.Entites;

[Table("inv_stores_inventory")]
public partial class InvStoresInventory
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("store_id")]
    public int StoreId { get; set; }

    [Column("inventory_date")]
    public DateOnly InventoryDate { get; set; }

    [Column("inventory_type")]
    public bool InventoryType { get; set; }

    [Column("auto_serial")]
    public long AutoSerial { get; set; }

    [Column("is_closed")]
    public bool IsClosed { get; set; }

    [Column("total_cost_batches", TypeName = "decimal(10, 2)")]
    public decimal TotalCostBatches { get; set; }

    [Column("notes")]
    [StringLength(225)]
    public string? Notes { get; set; }

    [Column("added_by")]
    public int AddedBy { get; set; }

    [Column("date")]
    public DateOnly Date { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_by")]
    public int? UpdatedBy { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [Column("com_code")]
    public int ComCode { get; set; }

    [Column("cloased_by")]
    public int? CloasedBy { get; set; }

    [Column("closed_at")]
    public DateTime? ClosedAt { get; set; }

    [InverseProperty("InvStoresInventory")]
    public virtual ICollection<InvStoresInventoryDetail> InvStoresInventoryDetails { get; set; } = new List<InvStoresInventoryDetail>();
}
