using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DominLayer.Entites;

[Table("treasuries_delivery")]
public partial class TreasuriesDelivery
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("treasuries_id")]
    public int TreasuriesId { get; set; }

    [Column("treasuries_can_delivery_id")]
    public int TreasuriesCanDeliveryId { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("added_by")]
    public int AddedBy { get; set; }

    [Column("updated_by")]
    public int? UpdatedBy { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [Column("com_code")]
    public int ComCode { get; set; }
}
