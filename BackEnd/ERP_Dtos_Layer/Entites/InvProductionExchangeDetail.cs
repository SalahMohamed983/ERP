using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DominLayer.Entites;

[Table("inv_production_exchange_details")]
public partial class InvProductionExchangeDetail
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("inv_production_exchange_id")]
    public long InvProductionExchangeId { get; set; }

    [Column("inv_production_exchange_auto_serial")]
    public long InvProductionExchangeAutoSerial { get; set; }

    [Column("order_type")]
    public byte OrderType { get; set; }

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

    [Column("batch_auto_serial")]
    public long? BatchAutoSerial { get; set; }

    [Column("production_date")]
    public DateOnly? ProductionDate { get; set; }

    [Column("expire_date")]
    public DateOnly? ExpireDate { get; set; }

    [Column("item_card_type")]
    public byte ItemCardType { get; set; }

    [ForeignKey("InvProductionExchangeId")]
    [InverseProperty("InvProductionExchangeDetails")]
    public virtual InvProductionExchange InvProductionExchange { get; set; } = null!;
}
