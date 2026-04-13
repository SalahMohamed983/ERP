using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DominLayer.Entites;

[Table("admins_stores")]
public partial class AdminsStore
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("admin_id")]
    public int AdminId { get; set; }

    [Column("store_id")]
    public int StoreId { get; set; }

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
    [InverseProperty("AdminsStores")]
    public virtual Admin Admin { get; set; } = null!;

    [ForeignKey("StoreId")]
    [InverseProperty("AdminsStores")]
    public virtual Store Store { get; set; } = null!;
}
