using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DominLayer.Entites;

[Table("treasuries")]
public partial class Treasury
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(250)]
    public string Name { get; set; } = null!;

    [Column("is_master")]
    public bool IsMaster { get; set; }

    [Column("last_isal_exhcange")]
    public long LastIsalExhcange { get; set; }

    [Column("last_isal_collect")]
    public long LastIsalCollect { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [Column("added_by")]
    public int AddedBy { get; set; }

    [Column("updated_by")]
    public int? UpdatedBy { get; set; }

    [Column("com_code")]
    public int ComCode { get; set; }

    [Column("date")]
    public DateOnly Date { get; set; }

    [Column("active")]
    public bool Active { get; set; }

    [InverseProperty("Treasuries")]
    public virtual ICollection<AdminsTreasury> AdminsTreasuries { get; set; } = new List<AdminsTreasury>();
}
