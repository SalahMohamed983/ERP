using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DominLayer.Entites;

[Table("services_with_orders_details")]
public partial class ServicesWithOrdersDetail
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("services_with_orders_id")]
    public long ServicesWithOrdersId { get; set; }

    [Column("services_with_orders_auto_serial")]
    public long ServicesWithOrdersAutoSerial { get; set; }

    [Column("order_type")]
    public bool OrderType { get; set; }

    [Column("service_id")]
    public int ServiceId { get; set; }

    [Column("notes")]
    [StringLength(500)]
    public string? Notes { get; set; }

    [Column("total", TypeName = "decimal(10, 2)")]
    public decimal Total { get; set; }

    [Column("added_by")]
    public int AddedBy { get; set; }

    [Column("updated_by")]
    public int? UpdatedBy { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [Column("com_code")]
    public int ComCode { get; set; }

    [Column("date")]
    public DateOnly Date { get; set; }

    [ForeignKey("ServicesWithOrdersId")]
    [InverseProperty("ServicesWithOrdersDetails")]
    public virtual ServicesWithOrder ServicesWithOrders { get; set; } = null!;
}
