using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DominLayer.Entites;

[Table("inv_production_order")]
public partial class InvProductionOrder
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("auto_serial")]
    public long AutoSerial { get; set; }

    [Column("production_plane")]
    public string ProductionPlane { get; set; } = null!;

    [Column("production_plan_date")]
    public DateOnly ProductionPlanDate { get; set; }

    [Column("is_approved")]
    public bool IsApproved { get; set; }

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

    [Column("approved_by")]
    public int? ApprovedBy { get; set; }

    [Column("approved_at")]
    public DateTime? ApprovedAt { get; set; }

    [Column("is_closed")]
    public bool IsClosed { get; set; }

    [Column("closed_by")]
    public int? ClosedBy { get; set; }

    [Column("closed_at")]
    public DateTime? ClosedAt { get; set; }

    [Column("date")]
    public DateOnly Date { get; set; }
}
