using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DominLayer.Entites;

[Table("mov_type")]
public partial class MovType
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(100)]
    public string Name { get; set; } = null!;

    [Column("active")]
    public bool Active { get; set; }

    [Column("in_screen")]
    public byte InScreen { get; set; }

    [Column("is_private_internal")]
    public bool IsPrivateInternal { get; set; }
}
