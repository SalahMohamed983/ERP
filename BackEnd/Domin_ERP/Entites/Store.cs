using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DominLayer.Entites;

[Table("stores")]
public partial class Store
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(250)]
    public string Name { get; set; } = null!;

    [Column("phones")]
    [StringLength(100)]
    public string? Phones { get; set; }

    [Column("address")]
    [StringLength(250)]
    public string? Address { get; set; }

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

    [InverseProperty("Store")]
    public virtual ICollection<AdminsStore> AdminsStores { get; set; } = new List<AdminsStore>();
}
