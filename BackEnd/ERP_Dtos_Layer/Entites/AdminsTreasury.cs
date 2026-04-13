using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DominLayer.Entites;

[Table("admins_treasuries")]
public partial class AdminsTreasury
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("admin_id")]
    public int AdminId { get; set; }

    [Column("treasuries_id")]
    public int TreasuriesId { get; set; }

    [Column("active")]
    public bool Active { get; set; }

    [Column("added_by")]
    public int AddedBy { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_by")]
    public int? UpdatedBy { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [Column("com_code")]
    public int ComCode { get; set; }

    [Column("date")]
    public DateOnly Date { get; set; }

    [ForeignKey("AdminId")]
    [InverseProperty("AdminsTreasuries")]
    public virtual Admin Admin { get; set; } = null!;

    [ForeignKey("TreasuriesId")]
    [InverseProperty("AdminsTreasuries")]
    public virtual Treasury Treasuries { get; set; } = null!;
}
